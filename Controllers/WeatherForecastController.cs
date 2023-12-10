using DAL8.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Net8Features.Models;
using System;
using System.Collections.Frozen;
using CW = System.Diagnostics.Debug;
namespace Net8Features.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};
        private static readonly string[] Summaries = ["Freezing",
            "Bracing",
            "Chilly",
            "Cool",
            "Mild",
            "Warm",
            "Balmy",
            "Hot",
            "Sweltering",
            "Scorching"];
        int[][] twoD = [[1, 1, 2], [4, 5, 6], [7, 8]];
        Dictionary<string, int> dict = [];

        FrozenDictionary<string, int> dicktFrozen;
        FrozenSet<string> frozenSummary = Summaries.ToFrozenSet();

        public readonly IOptionsSnapshot<WeatherServiceOptions> _config;
        public WeatherForecastController(IOptionsSnapshot<WeatherServiceOptions> config)
        {
            _config = config;

            //LinqQuery ToFrozenSet
            var groupResult = Summaries.GroupBy(word => word.Length switch
            {
                <= 5 => word + "(<5)",
                > 5 and < 8 => word + "(>5)",
                _ => word + "(>8)",
            }).Select((g, id) => new { Name = g.Key, Id = id }).ToFrozenSet();

            dicktFrozen = Summaries.ToList().Select((key, id) => (key, id)).ToFrozenDictionary(e => e.key, e => e.id);
        }
        [HttpGet("Shuffle")]
        public IOrderedEnumerable<KeyValuePair<string, int>> GetShuffleList()
        {
            var serviceName = _config.Value.ServiceName;
            //Shuffle Example
            int[] numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
            Random.Shared.Shuffle(numbers);
            return numbers.ToList().Select((key, id) => (key, id))
                .ToFrozenDictionary(e => $"{e.key}. number =>", e => e.id)
                .OrderBy(x => int.Parse(x.Key.Replace(". number =>", "")));
        }
        [HttpGet(Name = "GetWeatherForecast")]
        //public List<int> Get([FromKeyedServices(_config.Value.ServiceName)] IWeatherService service)      
        public List<int> Get([FromKeyedServices("weather2")] IWeatherService service, int id)
        {
            //WeatherService service = new WeatherService();
            service.GetMyName();
            List<int> list = new();
            List<string> weekDays = ["Pazartesi", "Sali", "Carsamba", "Persembe", "Cuma"];
            //var randomSummaries = Random.Shared.GetItems(Summaries, 3);
            var randomSummaries = GetRandom(new List<string>(), 5);
            //foreach (var item in Summaries)
            foreach (var item in randomSummaries)
            {
                dicktFrozen.TryGetValue(item, out int itemID);
                list.Add(itemID);
            }
            return list;
        }
        [HttpGet("GetRomanNumber")]
        public int Get(string number)
        {
            using (HttpClient client = new())
            {
                var products = client.GetFromJsonAsync<Net8Features.Models.Product>("https://dummyjson.com/products");
            }
            return GetRomanNumberValue(number);
        }
        [HttpGet("GetAllProducts")]
        public async Task<Net8Features.Models.ResultModel> GetAllProducts()
        {
            using (HttpClient client = new())
            {
                var products = await client.GetFromJsonAsync<Net8Features.Models.ResultModel>("https://dummyjson.com/products/");

                //Global Using Example
                var dicktProduct = products.products.Select((key, id) => (key, id)).ToFrozenDictionary(e => e.key, e => e.id);
                ShoppingData data = new()
                {
                    Type = Models.ServiceType.weather,
                    Basket = dicktProduct
                };
                //--------------
                return products ?? new Models.ResultModel();
            }
            //          
        }       
        [HttpGet("GetProductByID")]
        public async Task<Net8Features.Models.Product> GetProductByID(int? number)
        {
            using (HttpClient client = new())
            {
                var products = await client.GetFromJsonAsync<Net8Features.Models.Product>($"https://dummyjson.com/products/{number}");
                return products ?? new Models.Product();
            }
        }
        [HttpGet("GetUserByID")]
        public User GetUserByID(int id)
        {
            using (var dbContext = new NorthwindContext())
            {
                var result = dbContext.GetUserByID(id);
                return result;
            }
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public List<string> GetRandom(List<string> list, int totalCount)
        {
            var addRandomItems = list.Count() < totalCount ? Random.Shared.GetItems(Summaries.Except(list).
                ToArray(), (totalCount - list.Count())).Distinct().ToList() : list;
            list.AddRange(addRandomItems);
            if (list.Count() < totalCount) GetRandom(list, totalCount);
            return list;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public int GetRomanNumberValue(string number)
        {
            return number switch
            {
                "X" => 10,
                "VIII" => 8,
                "III" => 3,
                "L" => 50,
                _ => 0
            };
        }
    }

    /*public static class Interception
    {
        [InterceptsLocation("""C:\Users\Bora Kasmer\source\repos\GenericAttributeBlog\Net8Features\Controllers\WeatherForecastController.cs""", line: 32, character: 22)]
        public static void InterceptionName()
        {
            Console.WriteLine("Start Interception For Controller");
        }
    }*/
}
