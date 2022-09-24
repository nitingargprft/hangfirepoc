using HangFire.Jobs.Interface;
using HangFire.Jobs.Models;
using Integration.MSMQ;
using Integration.MSMQ.Helper;
using Integration.MSMQ.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangFire.Jobs
{
    public class OracleOrderService : IOrderManagement
    {
        private IMessageQueueHandler iMessageQueueHandler;
        public OracleOrderService()
        {
            this.iMessageQueueHandler = new MessageQueueHandler();
        }
        public void SubmitOrderToErp()
        {
            MessageQueueClient messageQueueClient = this.iMessageQueueHandler.ConnectToQueue("prft-12602", "private$\\znodetkcorders");
            if (messageQueueClient != null && messageQueueClient.MessageQueue != null)
            {
                OrderModel orderModel = this.GetOrderModel();
                string messageLable = string.Format("WebOrder {0}", orderModel.WebOrderNumber);
                string json = JsonConvert.SerializeObject(orderModel);

                byte[] bytes = Encoding.ASCII.GetBytes(json);

                this.iMessageQueueHandler.SendMessageToQueue(messageQueueClient, json, messageLable, Enums.MessageFormatter.BinaryFormatter);
            }
        }

        private string GenrateRamdonNumber()
        {
            Random r = new Random();
            int sequenceNumber = r.Next();
            string webOrderNumber = string.Format("Web{0}", sequenceNumber.ToString());
            return webOrderNumber;
        }

        public OrderModel GetOrderModel()
        {
            OrderModel orderModel = new OrderModel();
            orderModel.BillingAddress = new AddressModel();
            orderModel.BillingAddress.Address1 = "311 E Chicago St";
            orderModel.BillingAddress.Address2 = "Suite 520";
            orderModel.BillingAddress.CityName = "Milwaukee";
            orderModel.BillingAddress.CountryName = "United State";
            orderModel.BillingAddress.DisplayName = "Perficient MKE Office";
            orderModel.BillingAddress.FirstName = "Nitin";
            orderModel.BillingAddress.LastName = "Garg";
            orderModel.BillingAddress.PhoneNumber = "414-885-7516";
            orderModel.BillingAddress.PostalCode = "53202";
            orderModel.BillingAddress.StateCode = "WI";
            orderModel.BillingAddress.StateName = "Wisconsin";
            orderModel.ShippingAddress = orderModel.BillingAddress;
            orderModel.OrderSubTotal = 2.45M;
            orderModel.ShippingTotal = 10M;
            orderModel.Tax = 10M;
            orderModel.OrderTotal = 12.45M;
            orderModel.OrderLines = GetOrderLineItemModels();
            orderModel.WebOrderNumber = this.GenrateRamdonNumber();
            return orderModel;
        }

        public List<OrderLineItemModel> GetOrderLineItemModels()
        {

            List<OrderLineItemModel> orderLineItemModels = new List<OrderLineItemModel>();
            OrderLineItemModel orderLineItemModel = new OrderLineItemModel()
            {
                Description = "Hawaiian Punch 0.95 oz. Lemon Berry Squeeze Drink Mix (8 pk.)",
                Price = 2.45M,
                ProductName = "80008029",
                Quantity = 1,
                Sku = "80008029"
            };
            orderLineItemModels.Add(orderLineItemModel);
            return orderLineItemModels;
        }

        public void SendOrderToOracle()
        {
            Console.WriteLine("Fire-and-forget!");
        }
    }
}
