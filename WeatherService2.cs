using Con = System.Console;
using Env = System.Environment;
using GoogleMapPoint = (double latitude, double longitude, string name);
using CW = System.Diagnostics.Debug;
using Net8Features.Models;

namespace Net8Features
{
    public class WeatherService2 : IWeatherService
    {
        public void GetMyName() { Con.WriteLine($"Hello Bora Kasmer. Os Versin:{Env.OSVersion}"); GetMyName("Bill Gates"); }
        public void GetMyName(string name)
        {
            GoogleMapPoint mySchool = (38.8951, -77.0364, "ITU");
            Con.WriteLine($"Hello {name}");
            RedisCore redis = new();
            var redisValue = redis.GetValue();
            Con.WriteLine(redisValue);
            RedisCore redis2 = new(94, "Bill Gates", null, 60);
            var redisValue2 = redis2.GetValue();
            Con.WriteLine(redisValue2);
            CW.WriteLine("WEATHER222 SERVICE");
        }

        public async Task<List<ShortCodPlayers>> GetAllCodPlayers()
        {
            using (HttpClient client = new())
            {
                //https://json2csharp.com
                var players = client.GetFromJsonAsAsyncEnumerable<Net8Features.Models.CodPlayers>("https://microsoftedge.github.io/Demos/json-dummy-data/64KB.json");
                var shortPlayerList = new List<ShortCodPlayers>();
                await foreach (var player in players)
                {
                    shortPlayerList.Add(new ShortCodPlayers() { name = player.name, version = player.version });
                }

                return shortPlayerList.OrderBy(x=>x.version).ToList();
            }
        }

        public class RedisPersonKeyGenerator(ref readonly int id, string name)
        {
            //id=5;
            int num = id;
            //public string RedisKey => $"person:{id.ToString()}:{name}";
            public string RedisKey => $"person:{num.ToString()}:{name}";
        }
        public class RedisCore(int id, string name, object val, int expireMinute) : RedisPersonKeyGenerator(id, name)
        {
            public RedisCore() : this(78, "bora", null, 30) { } // default RedisCore 

            public int Id => id;
            public object Value => val;
            public TimeSpan ExpireTime = TimeSpan.FromMinutes(expireMinute);

            public string GetValue()
            {
                return $"RedisClient: Key={RedisKey}, Value:{Value}, ExpireTime:{ExpireTime}";
            }
        }
    }
}
