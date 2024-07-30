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

        private static readonly HttpClient client;

        static ApiService()
        {
            // will take request to the server
            client = new HttpClient
            {
                // will get request from given URL
                BaseAddress = new Uri("http://api.weatherstack.com/")
            };
        }

        // function for getting given lat and long to get appropriate weather
        public static async Task<Root> GetWeather(double latitute, double longitude)
        {
            // will take request to the server
            var httpClient = new HttpClient();
            //string apiKeyCode = "9787719d93919bf24ba94a8bbfece6a2";
            //string UriRequest = $"current?access_key={apiKeyCode}&query={latitute},{longitude}";

            // will get request from given URL
            var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=metric&appid=9787719d93919bf24ba94a8bbfece6a2", latitute, longitude));
            //var response = await client.GetStringAsync(UriRequest);

            // maps this json response variable to the Root class recreated earlier --> Root class = parent class
            return JsonConvert.DeserializeObject<Root>(response);
        }



        // function for getting appropriate weather by city name
        public static async Task<Root> GetWeatherByCity(string city)
        {
            // will take request to the server
            var httpClient = new HttpClient();
            //string apiKeyCode = "9787719d93919bf24ba94a8bbfece6a2";
            //string UriRequest = $"current?access_key={apiKeyCode}&query={city}";


            // will get request from given URL
            var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast?q={0}&units=metric&appid=9787719d93919bf24ba94a8bbfece6a2", city));
            //var response = await client.GetStringAsync(UriRequest);

            // maps this json response variable to the Root class recreated earlier --> Root class = parent class
            return JsonConvert.DeserializeObject<Root>(response);
        }
    }

   
}
