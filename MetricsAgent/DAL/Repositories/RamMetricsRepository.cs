using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Core.DAL.Interfaces;
using Dapper;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Repositories
{
    public class RamMetricsRepository : IRepository<RamMetric>
    {
        private const string ConnectionString = @"Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        private readonly SQLiteConnection _connection;

        public RamMetricsRepository()
        {
            _connection = new SQLiteConnection(ConnectionString);
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }
        
        
        public IList<RamMetric> GetAll() => 
            _connection.Query<RamMetric>("SELECT id, time, value FROM rammetrics").ToList();

        
        public RamMetric GetById(int id) =>
            
            _connection.QuerySingle<RamMetric>("SELECT id, time, value FROM rammetrics WHERE id = @id", 
                new { id = id });
        
        public void Create(RamMetric item) =>
        
            _connection.Execute("INSERT INTO rammetrics (value, time) VALUES(@value, @time)",
                new {value = item.Value, time = item.Time.TotalSeconds});
        

        public void Update(RamMetric item) =>
      
            _connection.Execute("UPDATE rammetrics SET value = @value, time = @time WHERE id = @id",
                new {value = item.Value, time = item.Time.TotalSeconds, id = item.Id});
        
        public void Delete(int id) =>  _connection.Execute("DELETE FROM rammetrics WHERE id = @id",new {id = id});
    }
}