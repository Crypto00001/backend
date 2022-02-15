using Pandora.Application.Contract;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Application.Scheduler.Jobs
{
    public class UpdateUserPlanDailyJob : IJob
    {
        private readonly IUserPlanService _userPlanService;
        public UpdateUserPlanDailyJob(IUserPlanService userPlanService)
        {
            _userPlanService = userPlanService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                return _userPlanService.UpdatePlansScheduler();
            }
            catch (Exception)
            {
                return Task.CompletedTask;
            }
        }

    }
}
