using HangFire.Jobs;
using HangFire.Jobs.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HangFireSampleApp.Controllers
{
    public class ScheduleJobController : Controller
    {
        private IJobs IJobs;
        public ScheduleJobController()
        {
            this.IJobs = new HangFireJobsHandler();
        }
        // GET: ScheduleJob
        public ActionResult Index()
        {
            //this.IJobs.AddRecurringJob("SubmitOrderToErp");
            this.IJobs.ScheduleAndForgetJob();
            return View();
        }
    }
}