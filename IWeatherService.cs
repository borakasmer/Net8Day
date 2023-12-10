using Net8Features.Models;

namespace Net8Features
{
    public interface IWeatherService
    {
        public void GetMyName();
        public void GetMyName(string name);
        public Task<List<ShortCodPlayers>> GetAllCodPlayers();
    }
}
