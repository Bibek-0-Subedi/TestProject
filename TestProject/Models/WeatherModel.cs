using System;

namespace TestProject.Models
{
    public class WeatherModel
    {
        public DateTime date { get; set; }
        public int temperatureC { get; set; }
        public int temperatureF { get; set; }
        public string summary { get; set; }


    }
    public class Root
    {
        public WeatherModel weather { get; set; }
    }
}
