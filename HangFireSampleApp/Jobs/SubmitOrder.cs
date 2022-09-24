using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace HangFireSampleApp.Jobs
{
    public class SubmitOrder : IDisposable
    {
        public void CreateOrder(string webOrderNumber)
        {
            Debug.WriteLine("!!Create Order From Erp" + webOrderNumber);
        }

        public void SubmitFailedOrder()
        {
            Debug.WriteLine("!!Processing Failed Order!!");
        }
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}