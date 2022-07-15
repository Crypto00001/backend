using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Pandora.Application.Contract;
using Pandora.Application.Scheduler;
using Pandora.Application.Scheduler.Jobs;
using Pandora.Application.Scraper;
using Pandora.Application.Service;
using Pandora.Domain.Repository;
using Pandora.Infrastructure.Context;
using Pandora.Infrastructure.Implementation;
using Pandora.Jwt;
using Pandora.Middleware;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Pandora
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("PandoraCnn");
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pandora", Version = "v1" }); });
            services.AddDbContext<EFDbContext>(
                options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Transient);
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddTransient<CheckTransactionConfirmJob>();
            services.AddTransient<CheckPaymentConfirmationScheduler>();

            services.AddScoped<UpdateUserPlanAfterPlanDurationJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(UpdateUserPlanAfterPlanDurationJob),
                cronExpression: "0 0 0 * * ?"));

            services.AddSingleton<UpdatePriceJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(UpdatePriceJob),
                cronExpression: "0 0/15 * * * ? *"));

            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IDashboardService, DashboardService>();
            services.AddTransient<IReferralService, ReferralService>();
            services.AddTransient<IPlanService, PlanService>();
            services.AddTransient<IWalletService, WalletService>();
            services.AddTransient<IUserPlanService, UserPlanService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IWithdrawalService, WithdrawalService>();

            services.AddTransient<UserRepository, EfUserRepository>();
            services.AddTransient<DashboardRepository, EfDashboardRepository>();
            services.AddTransient<ReferralRepository, EfReferralRepository>();
            services.AddTransient<PlanRepository, EfPlanRepository>();
            services.AddTransient<WalletRepository, EfWalletRepository>();
            services.AddTransient<UserPlanRepository, EfUserPlanRepository>();
            services.AddTransient<PaymentRepository, EfPaymentRepository>();
            services.AddTransient<WithdrawalRepository, EfWithdrawalRepository>();

            services.AddHostedService<QuartzHostedService>();
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pandora v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            UpdatePriceScraper.Start();
        }
    }
}
