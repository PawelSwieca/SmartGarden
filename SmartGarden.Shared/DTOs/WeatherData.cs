using System;
using System.Collections.Generic;
using System.Text;

namespace SmartGarden.Shared.DTOs
{
    public class WeatherData
    {
        public CurrentWeather current { get; set; }
        public HourlyWeather hourly { get; set; }

        public class CurrentWeather
        {
            public DateTime time { get; set; }
            public double temperature_2m { get; set; }
            public double relative_humidity_2m { get; set; }
            public double rain { get; set; }
        }

        public class HourlyWeather
        {
            public List<DateTime> time { get; set; }
            public List<double> temperature_2m { get; set; }
            public List<double> relative_humidity_2m { get; set; }
            public List<double> precipitation { get; set; }
            public List<double> precipitation_probability { get; set; }
        }
    }
}