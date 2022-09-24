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


namespace HangFire.Jobs.JobProcessor
{
    public class OrderManagementProcessor
    {
        private IOrderManagement IOrderManagement;

        public OrderManagementProcessor()
        {
            this.IOrderManagement = new OracleOrderService();
        }

        public void Execute()
        {
            this.IOrderManagement.SubmitOrderToErp();
        }

        public void SendOrderToOracle() {
            this.IOrderManagement.SendOrderToOracle();
        }

    }
}
