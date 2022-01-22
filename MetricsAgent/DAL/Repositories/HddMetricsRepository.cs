using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;

namespace MetricsAgent.DAL.Repositories
{
    public class HddMetricsRepository : IRepository<HddMetric>
    {
        private const string ConnectionString = @"Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        private readonly SQLiteConnection _connection;

        public HddMetricsRepository()
        {
            _connection = new SQLiteConnection(ConnectionString);
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }

        public IList<HddMetric> GetAll() => 
            
            _connection.Query<HddMetric>("SELECT id, time, value FROM hddmetrics").ToList();

        
        public HddMetric GetById(int id) =>
            
            _connection.QuerySingle<HddMetric>("SELECT id, time, value FROM hddmetrics WHERE id = @id", 
                new { id = id });
        

        public void Create(HddMetric item) =>
        
            _connection.Execute("INSERT INTO hddmetrics (value, time) VALUES (@value, @time)",
                new {value = item.Value, time = item.Time.TotalSeconds});
        

        public void Update(HddMetric item) =>
      
            _connection.Execute("UPDATE hddmetrics SET value = @value, time = @time WHERE id = @id",
                new {value = item.Value, time = item.Time.TotalSeconds, id = item.Id});
        
        public void Delete(int id) =>
            
            _connection.Execute("DELETE FROM hddmetrics WHERE id = @id",new {id = id});

    }
}