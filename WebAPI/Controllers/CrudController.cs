using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using WebAPI.Models;
using System.Collections.Generic;


namespace WebAPI.Controllers
{
    [Route("api/crud")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        static readonly IWeatherForecastRepositary repository = new WeatherForecastRepository();

        [HttpGet("get")]
        public IEnumerable<WeatherForecast> Get() => repository.Get();
        

        //Возможность сохранить температуру в указанное время

        [HttpPost("add")]
        public IActionResult Add([FromForm] DateTime date, [FromForm] int temp) 
        { 
           repository.Create(date, temp);
           return Ok(repository);
        }


        //Возможность отредактировать показатель температуры в указанное время

        [HttpPut("update")]
        public IActionResult Update([FromForm] DateTime date, [FromForm] int temp) {

            repository.Update(date, temp);
            return Ok(repository);

        } 


        //Возможность удалить показатель температуры в указанный промежуток времени

        [HttpDelete ("delete")]
        public IActionResult Delete ([FromForm]DateTime fromDate, [FromForm] DateTime toDate)
        {
            repository.Delete(fromDate, toDate);
            return Ok(repository);
        }


        //Возможность прочитать список показателей температуры за указанный промежуток времени

        [HttpGet("select")]
        public IEnumerable<WeatherForecast> Select([FromForm] DateTime fromDate, [FromForm] DateTime toDate) => repository.Select(fromDate, toDate);

    }
}
