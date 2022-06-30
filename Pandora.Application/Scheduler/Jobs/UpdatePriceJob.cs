using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Application.Scheduler.Jobs
{
    public class UpdatePriceJob : IJob
    {

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                return Scraper.UpdatePriceScraper.UpdatePrices();
            }
            catch (Exception)
            {
                return Task.CompletedTask;
            }
        }

    }
}
