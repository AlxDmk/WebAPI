using System;
using System.Collections;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public interface IWeatherForecastRepositary: IEnumerable
    {
        IEnumerable <WeatherForecast> Get();

        void Create(DateTime date, int temp);

        WeatherForecast Read(DateTime date);

        bool Update(DateTime date, int temp);

        void Delete (DateTime date);

        void Delete (DateTime fromDate, DateTime toDate);

        IEnumerable<WeatherForecast> Select(DateTime fromDate, DateTime toDate);
    }
}
