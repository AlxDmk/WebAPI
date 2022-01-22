using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;

namespace MetricsAgent.DAL.Repositories
{
    public class NetworkMetricsRepository : IRepository<NetworkMetric>
    {
        private readonly SQLiteConnection _connection;
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        

        public NetworkMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
            _connection = new SQLiteConnection(ConnectionString);
        }
        public IList<NetworkMetric> GetAll() => 
            
            _connection.Query<NetworkMetric>("SELECT id, time, value FROM networkmetrics").ToList();

        
        public NetworkMetric GetById(int id) =>
            
            _connection.QuerySingle<NetworkMetric>("SELECT id, time, value FROM networkmetrics WHERE id = @id", 
                new { id = id });
        

        public void Create(NetworkMetric item) =>
        
            _connection.Execute("INSERT INTO networkmetrics (value, time) VALUES (@value, @time)",
                new {value = item.Value, time = item.Time.TotalSeconds});
        

        public void Update(NetworkMetric item) =>
      
            _connection.Execute("UPDATE networkmetrics SET value = @value, time = @time WHERE id = @id",
                new {value = item.Value, time = item.Time.TotalSeconds, id = item.Id});
        
        public void Delete(int id) =>
            
            _connection.Execute("DELETE FROM networkmetrics WHERE id = @id",new {id = id});

    }
}