using Application.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace Application.Service
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetWeatherAsync(string city)
        {
            var (lat, lon) = city.ToLower() switch
            {
                "rosario"    => (-32.95, -60.65),
                "santa fe"   => (-31.63, -60.7),
                "buenos aires" => (-34.61, -58.38),
                "ushuaia"    => (-54.80, -68.30),
                "salta"      => (-24.79, -65.41),
                "zavalla"    => (-33.03, -60.88),
                "roldan"     => (-32.83, -60.93),
                "cordoba"    => (-31.42, -64.19),
                // Puedes agregar más ciudades aquí
                _ => (0.0, 0.0) // Coordenadas inválidas
            };

            if (lat == 0.0 && lon == 0.0)
                return "{\"error\": \"City not supported\"}";

            var client = _httpClientFactory.CreateClient("OpenMeteo");
            var url = $"forecast?latitude={lat}&longitude={lon}&current_weather=true";
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return "{\"error\": \"Weather API error\"}";

            return await response.Content.ReadAsStringAsync();
        }
    }
}