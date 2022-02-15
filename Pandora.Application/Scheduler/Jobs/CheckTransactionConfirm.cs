using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Application.Scheduler.Jobs
{
    public class CheckTransactionConfirmJob : IJob
    {
        private readonly IServiceProvider _provider;
        public CheckTransactionConfirmJob(IServiceProvider provider)
        {
            _provider = provider;
        }

        public Task Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;

            JobDataMap dataMap = context.MergedJobDataMap;

            Guid userId = dataMap.GetGuid("userId");
            string transactionId = dataMap.GetInt("transactionId").ToString();
            Task<IScheduler> scheduler = ((ISchedulerFactory)_provider.GetService(typeof(ISchedulerFactory))).GetScheduler();

            Console.Write($"{DateTime.Now} [Test Test]" + userId + " " + transactionId + Environment.NewLine);
            context.Scheduler.DeleteJob(key);
            return Task.CompletedTask;
        }

    }
}
