using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Core.DAL.Interfaces;
using Dapper;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Repositories
{
    public class DotNetMetricsRepository: IRepository<DotNetMetric>
    {
        private const string ConnectionString = @"Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        private readonly SQLiteConnection _connection;

        public DotNetMetricsRepository()
        {
            _connection = new SQLiteConnection(ConnectionString);
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }
        
        public IList<DotNetMetric> GetAll() =>
            _connection.Query<DotNetMetric>("SELECT id, time, value FROM dotnetmetrics").ToList();

        
        public DotNetMetric GetById(int id) =>
            _connection.QuerySingle<DotNetMetric>("SELECT id, time, value FROM dotnetmetrics WHERE id = @id", 
                new { id = id });
        
        public void Create(DotNetMetric item) =>
            _connection.Execute("INSERT INTO dotnetmetrics (value, time) VALUES(@value, @time)",
                new {value = item.Value, time = item.Time.TotalSeconds});
        
        public void Update(DotNetMetric item) =>
            _connection.Execute("UPDATE dotnetmetrics SET value = @value, time = @time WHERE id = @id",
                new
                {
                    value = item.Value,
                    time = item.Time.TotalSeconds,
                    id = item.Id
                });
        
        public void Delete(int id) =>
            _connection.Execute("DELETE FROM dotnetmetrics WHERE id = @id",new {id = id});

        public IList<DotNetMetric> Select(double fromTime, double toTime) =>
            _connection.Query<DotNetMetric>(
                    "SELECT id, value, time FROM dotnetmetrics WHERE time > @fromTime AND time < @toTime",
                    new
                    {
                        fromTime = fromTime,
                        toTime = toTime
                    })
                    .ToList();
    }
}