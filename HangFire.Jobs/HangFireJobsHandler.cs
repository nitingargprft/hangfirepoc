using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using HangFire.Jobs.Interface;
using HangFire.Jobs.JobProcessor;

namespace HangFire.Jobs
{
    public class HangFireJobsHandler : IJobs
    {
        public void AddRecurringJob(string jobId)
        {
            if (jobId.Equals("SubmitOrderToErp"))
            {
                ScheduleOrderSubmitJob(jobId);
            }
        }

        private static void ScheduleOrderSubmitJob(string jobId)
        {
            RecurringJob.AddOrUpdate<OrderManagementProcessor>(jobId, x => x.Execute(), Cron.Minutely());
        }

        public void ScheduleAndForgetJob()
        {
             BackgroundJob.Enqueue<OrderManagementProcessor>(x => x.SendOrderToOracle());
        }
    }
}
