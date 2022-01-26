using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Core.DAL.Interfaces;
using Dapper;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Repositories
{
    public class CpuMetricsRepository: IRepository<CpuMetric>
    {
        private const string ConnectionString = @"Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        private readonly SQLiteConnection _connection;

        public CpuMetricsRepository()
        {
                _connection = new SQLiteConnection(ConnectionString);
                SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }
        
        public IList<CpuMetric> GetAll() =>
            _connection.Query<CpuMetric>("SELECT id, time, value FROM cpumetrics").ToList();

        
        public CpuMetric GetById(int id) =>
            _connection.QuerySingle<CpuMetric>("SELECT id, time, value FROM cpumetrics WHERE id = @id", 
                new { id = id });
        

        public void Create(CpuMetric item) =>
            _connection.Execute("INSERT INTO cpumetrics (value, time) VALUES(@value, @time)",
                new {value = item.Value, time = item.Time.TotalSeconds});
        
        public void Update(CpuMetric item) =>
            _connection.Execute("UPDATE cpumetrics SET value = @value, time = @time WHERE id = @id",
                new
                {
                    value = item.Value,
                    time = item.Time.TotalSeconds,
                    id = item.Id
                });
        
        public void Delete(int id) =>
            _connection.Execute("DELETE FROM cpumetrics WHERE id = @id",new {id = id});


        public IList<CpuMetric> Select(double fromTime, double toTime) =>
            _connection.Query<CpuMetric>(
                "SELECT id, value, time FROM cpumetrics WHERE time > @fromTime AND time < @toTime",
                new
                {
                    fromTime = fromTime,
                    toTime = toTime
                })
                .ToList(); }
}