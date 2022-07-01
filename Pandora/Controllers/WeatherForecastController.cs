using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pandora.Application.Scheduler.Jobs;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ISchedulerFactory _schedulerFactory;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ISchedulerFactory schedulerFactory)
        {
            _logger = logger;
            _schedulerFactory = schedulerFactory;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> GetAsync()
        {
            // IScheduler scheduler = await _schedulerFactory.GetScheduler();
            //
            // IJobDetail job = JobBuilder.Create<CheckTransactionConfirmJob>()
            //     .UsingJobData("userId", Guid.NewGuid())
            //     .UsingJobData("transactionId", new Random().Next(0, 1))
            //     .Build();
            //
            // // Trigger the job to run now, and then repeat every 10 seconds
            // ITrigger trigger = TriggerBuilder.Create()
            //     .StartNow()
            //     .WithSimpleSchedule(x => x
            //         .WithIntervalInSeconds(10)
            //         .RepeatForever())
            //     .Build();
            //
            // // Tell quartz to schedule the job using our trigger
            // await scheduler.ScheduleJob(job, trigger);
            // // some sleep to show what's happening
            // await Task.Delay(TimeSpan.FromSeconds(10));


            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet]
        [Route("finish")]
        public async Task<IEnumerable<WeatherForecast>> GetAsyncfinish()
        {
            IScheduler scheduler = await _schedulerFactory.GetScheduler();

            // Tell quartz to schedule the job using our trigger
            await scheduler.DeleteJob(new JobKey("job1", "group1"));
            // some sleep to show what's happening
            await Task.Delay(TimeSpan.FromSeconds(10));
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

        }
    }
}
