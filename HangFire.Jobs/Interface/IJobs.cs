using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangFire.Jobs.Interface
{
    public interface IJobs
    {
        void AddRecurringJob(string jobId);
        void ScheduleAndForgetJob();
    }
}
