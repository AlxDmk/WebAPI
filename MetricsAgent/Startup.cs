using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL;
using System.Data.SQLite;
using AutoMapper;
using Core.DAL.Interfaces;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repositories;
using MetricsAgent.Mappers;

namespace MetricsAgent
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
            ConfigureSqlLiteConnection(services);
            services.AddScoped<IRepository<CpuMetric>, CpuMetricsRepository>();
            services.AddScoped<IRepository<DotNetMetric>, DotNetMetricsRepository>();
            services.AddScoped<IRepository<HddMetric>, HddMetricsRepository>();
            services.AddScoped<IRepository<NetworkMetric>, NetworkMetricsRepository>();
            services.AddScoped<IRepository<RamMetric>, RamMetricsRepository>();

            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new
                MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);


        }

        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            const string connectionString = @"Data Source= metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            PrepareSchema(connection);
        }
        
        private void PrepareSchema(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                // command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
                // command.ExecuteNonQuery();
        
                command.CommandText = @"CREATE TABLE IF NOT EXISTS  cpumetrics(id INTEGER PRIMARY KEY , value INT, time BIGINT)";
                command.ExecuteNonQuery();
                
                // command.CommandText = "DROP TABLE IF EXISTS dotnetmetrics";
                // command.ExecuteNonQuery();
        
                command.CommandText = @"CREATE TABLE IF NOT EXISTS dotnetmetrics(id INTEGER PRIMARY KEY , value INT, time INT)";
                command.ExecuteNonQuery();
                
                command.CommandText = @"CREATE TABLE IF NOT EXISTS hddmetrics(id INTEGER PRIMARY KEY , value INT, time INT)";
                command.ExecuteNonQuery();
                
                command.CommandText = @"CREATE TABLE IF NOT EXISTS networkmetrics(id INTEGER PRIMARY KEY , value INT, time INT)";
                command.ExecuteNonQuery();
                
                command.CommandText = @"CREATE TABLE IF NOT EXISTS rammetrics(id INTEGER PRIMARY KEY , value INT, time INT)";
                command.ExecuteNonQuery();
            }
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
        }
    }
}
