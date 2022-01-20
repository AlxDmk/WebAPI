using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Models
{
    public class WeatherForecastRepository :IWeatherForecastRepositary
    {
       
        private readonly  List<WeatherForecast> _forecasts = new List<WeatherForecast>() ;

        public WeatherForecastRepository()
        {

            for (int i = 0; i<= 10; i++)
            {
                _forecasts.Add(new WeatherForecast()
                    {
                    Date = DateTime.Today.AddDays(i),
                    TemperatureC = i
                    
                    });
            }
        }                    
        
        public IEnumerable<WeatherForecast> Get() => _forecasts;

        public void Create(DateTime date, int temp)
        {
            int index = _forecasts.FindIndex(x => x.Date == date);
            if (index == -1)
            {
                _forecasts.Add
                    (
                    new WeatherForecast()
                    {
                        Date = date,
                        TemperatureC = temp
                    }
                );
            }
            else
            {
               Update(date, temp);
            }           
        }

        public WeatherForecast Read(DateTime date) => _forecasts.Find(x => x.Date == date);

        public bool Update(DateTime date, int temp)
        {
            int index = _forecasts.FindIndex(x => x.Date == date);

            if (index < 0)
            {
                return false;
            }
            _forecasts.RemoveAt(index);
            _forecasts.Add(new WeatherForecast()
            {
                Date = date,
                TemperatureC = temp
            });
                       
            return true;
        }
        
        public void Delete(DateTime date) => _forecasts.RemoveAll(p => p.Date == date);

        public void Delete(DateTime fromDate, DateTime toDate) => _forecasts.RemoveAll(p => p.Date >= fromDate && p.Date <= toDate);

        public IEnumerable<WeatherForecast> Select(DateTime fromDate, DateTime toDate) { 

            IEnumerable<WeatherForecast> selectedList = new List<WeatherForecast> ();
            selectedList = _forecasts.Where(x => x.Date >= fromDate && x.Date <= toDate).ToList();

            return selectedList;

            }
        public IEnumerator GetEnumerator() => _forecasts.GetEnumerator();
        
    }
}
