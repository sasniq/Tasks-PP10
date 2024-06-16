using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main()
    {
        string city = "London"; // Фиксированный город
        string apiKey = "1b5d712b43810ebcc9c7f7c020c8e9b9";
        string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                JObject weatherData = JObject.Parse(responseData);

                string weatherDescription = (string)weatherData["weather"][0]["description"];
                double temperature = (double)weatherData["main"]["temp"];
                double feelsLike = (double)weatherData["main"]["feels_like"];
                double tempMin = (double)weatherData["main"]["temp_min"];
                double tempMax = (double)weatherData["main"]["temp_max"];
                int humidity = (int)weatherData["main"]["humidity"];
                double windSpeed = (double)weatherData["wind"]["speed"];

                Console.WriteLine($"Описание погоды: {weatherDescription}");
                Console.WriteLine($"Температура: {temperature}°C");
                Console.WriteLine($"Ощущается как: {feelsLike}°C");
                Console.WriteLine($"Минимальная температура: {tempMin}°C");
                Console.WriteLine($"Максимальная температура: {tempMax}°C");
                Console.WriteLine($"Влажность: {humidity}%");
                Console.WriteLine($"Скорость ветра: {windSpeed} м/с");
            }
            else
            {
                Console.WriteLine("Ошибка при получении данных о погоде.");
            }
        }

        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadLine();
    }
}