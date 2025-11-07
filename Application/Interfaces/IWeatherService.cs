using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IWeatherService
    {
        Task<string> GetWeatherAsync(string city);
    }
}