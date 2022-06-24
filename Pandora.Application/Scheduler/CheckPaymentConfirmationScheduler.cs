using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace Pandora.Application.Scheduler
{
    public class CheckPaymentConfirmationScheduler
    {
        private IScheduler _schedulerInstance;
        private readonly object _syncRoot = new();


        public IScheduler GetScheduler()
        {
            if (_schedulerInstance == null)
            {
                lock (_syncRoot)
                {
                    if (_schedulerInstance == null)
                    {
                        _schedulerInstance = Task.Run(GetSchedulerAsync).GetAwaiter().GetResult();
                    }
                }
            }

            return _schedulerInstance;
        }


        private async Task<IScheduler> GetSchedulerAsync()
        {
            var schedulerFactory = new StdSchedulerFactory();

            try
            {
                return await schedulerFactory.GetScheduler();
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }
    }
}