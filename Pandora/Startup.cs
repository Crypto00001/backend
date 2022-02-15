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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pandora", Version = "v1" });
            });
            services.AddDbContext<EFDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PandoraCnn")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Transient);
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IReferralService, ReferralService>();
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<IUserPlanService, UserPlanService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IWithdrawalService, WithdrawalService>();

            services.AddScoped<UserRepository, EfUserRepository>();
            services.AddScoped<DashboardRepository, EfDashboardRepository>();
            services.AddScoped<ReferralRepository, EfReferralRepository>();
            services.AddScoped<PlanRepository, EfPlanRepository>();
            services.AddScoped<WalletRepository, EfWalletRepository>();
            services.AddScoped<UserPlanRepository, EfUserPlanRepository>();
            services.AddScoped<PaymentRepository, EfPaymentRepository>();
            services.AddScoped<WithdrawalRepository, EfWithdrawalRepository>();

            services.AddHostedService<QuartzHostedService>();
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddScoped<UpdateUserPlanDailyJob>();
            services.AddSingleton<UpdatePriceJob>();
            services.AddSingleton<CheckTransactionConfirmJob>();

            services.AddSingleton(new JobSchedule(
                jobType: typeof(UpdateUserPlanDailyJob),
                cronExpression: "0 0 0 * * ?"));

            services.AddSingleton(new JobSchedule(
                jobType: typeof(UpdatePriceJob),
                cronExpression: "0 0/15 * * * ?")); 
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
            
            ScrapeManager.Start();
        }
    }
}
