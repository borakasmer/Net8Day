using System.ComponentModel.DataAnnotations;

namespace Net8Features.Models
{
    //JSON TO CLASS => https://json2csharp.com
    //DUMMY JSON => https://dummyjson.com/products
    //.Net Core Library https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-8.0
    public class Product
    {
        public int id { get; set; }
        [DeniedValues("Bora","Cetvel","Kalem", ErrorMessage ="Gecersiz Urun")]
        public string title { get; set; }
        [Length(50, 100)]
        public string description { get; set; }
        public int price { get; set; }
        [Range(1, 20, MinimumIsExclusive = true, MaximumIsExclusive = true)]
        public double discountPercentage { get; set; }
        public double rating { get; set; }
        public int stock { get; set; }
        public string brand { get; set; }
        public string category { get; set; }
        public string thumbnail { get; set; }
        public List<string> images { get; set; }
    }

    public class ResultModel
    {
        public List<Product> products { get; set; }
        public int total { get; set; }
        public int skip { get; set; }
        [AllowedValues(20,30,50,ErrorMessage ="Boyle Limit Olmaz Olsun :)")]
        public int limit { get; set; }
    }
}
