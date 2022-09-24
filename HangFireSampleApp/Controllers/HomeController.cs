using Hangfire;
using HangFire.Jobs;
using HangFireSampleApp.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HangFireSampleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly BackgroundJobClient _jobs = new BackgroundJobClient();
        public ActionResult Index()
        {
            //RecurringJob.AddOrUpdate(() => Console.WriteLine("Recurring!"), Cron.Daily);
            //_jobs.Enqueue<SubmitOrder>(x => x.CreateOrder("Web1001010"));
            //OracleOrderService oracleOrderManagement = new OracleOrderService();
            //oracleOrderManagement.CreateOrder();
            return View();
        }

        public ActionResult About()
        {

            RecurringJob.AddOrUpdate<SubmitOrder>("Submit Order To Erp", x => x.SubmitFailedOrder(), Cron.MinuteInterval(5));
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}