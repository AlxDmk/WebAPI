using System.Data.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Core.DAL.Interfaces;
using FluentMigrator.Runner;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repositories;
using MetricsAgent.Jobs;
using MetricsAgent.Mappers;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace MetricsAgent
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private const string ConnectionString = @"Data Source= metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //ConfigureSqlLiteConnection(services);
            services.AddSingleton<IRepository<CpuMetric>, CpuMetricsRepository>();
            services.AddSingleton<IRepository<DotNetMetric>, DotNetMetricsRepository>();
            services.AddSingleton<IRepository<HddMetric>, HddMetricsRepository>();
            services.AddSingleton<IRepository<NetworkMetric>, NetworkMetricsRepository>();
            services.AddSingleton<IRepository<RamMetric>, RamMetricsRepository>();

            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(
                new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(ConnectionString)
                    .ScanIn(typeof(Startup).Assembly).For.Migrations()
                ).AddLogging(lb => lb
                    .AddFluentMigratorConsole());

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            
            services.AddSingleton<CpuMetricJob>();
            services.AddSingleton(new JobSchedule(
                typeof(CpuMetricJob), "0/5 * * * * ?"));
            
            services.AddSingleton<RamMetricJob>();
            services.AddSingleton(new JobSchedule(
                typeof(RamMetricJob), "0/5 * * * * ?"));
            
            services.AddSingleton<HddMetricJob>();
            services.AddSingleton(new JobSchedule(
                typeof(HddMetricJob), "0/5 * * * * ?"));
            
            services.AddSingleton<DotNetMetricJob>();
            services.AddSingleton(new JobSchedule(
                typeof(DotNetMetricJob), "0/5 * * * * ?"));
            
            services.AddSingleton<NetworkMetricJob>();
            services.AddSingleton(new JobSchedule(
                typeof(NetworkMetricJob), "0/5 * * * * ?"));

            services.AddHostedService<QuartzHostedService>();

        }

        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            migrationRunner.MigrateUp();
        }
    }
}
