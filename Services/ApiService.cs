using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    // changed access modifier from internal to public
    public static class ApiService
    {
        // function for getting given lat and long to get appropriate weather
        public static async Task<Root> GetWeather(double latitute, double longitude)
        {
            // will take request to the server
            var httpClient = new HttpClient();

            // will get request from given URL
            var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=metric&appid=9787719d93919bf24ba94a8bbfece6a2", latitute, longitude));

            // maps this json response variable to the Root class recreated earlier --> Root class = parent class
            return JsonConvert.DeserializeObject<Root>(response);
        }



        // function for getting appropriate weather by city name
        public static async Task<Root> GetWeatherByCity(string city)
        {
            // will take request to the server
            var httpClient = new HttpClient();

            // will get request from given URL
            var response = await httpClient.GetStringAsync(string.Format("api.openweathermap.org/data/2.5/forecast?q={0}&units=metric&appid=9787719d93919bf24ba94a8bbfece6a2", city));

            // maps this json response variable to the Root class recreated earlier --> Root class = parent class
            return JsonConvert.DeserializeObject<Root>(response);
        }
    }

   
}
