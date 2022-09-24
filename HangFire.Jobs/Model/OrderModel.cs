using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HangFire.Jobs.Models
{
    public class OrderModel
    {
        public OrderModel()
        {
            BillingAddress = new AddressModel();
            ShippingAddress = new AddressModel();
            OrderLines = new List<OrderLineItemModel>();
            ReturnOrderLines = new List<OrderLineItemModel>();
        }
        public string WebOrderNumber { get; set; }

        public string ErpOrderNumber { get; set; }
        public AddressModel BillingAddress { get; set; }
        public AddressModel ShippingAddress { get; set; }
        public decimal OrderSubTotal { get; set; }
        public decimal ShippingTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal OrderTotal { get; set; }
        public string CarrierName { get; set; }

        public List<OrderLineItemModel> OrderLines { get; set; }
        public List<OrderLineItemModel> ReturnOrderLines { get; set; }
    }
}