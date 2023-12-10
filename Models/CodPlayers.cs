namespace Net8Features.Models
{
    public class CodPlayers
    {
        public string name { get; set; }
        public string language { get; set; }
        public string id { get; set; }
        public string bio { get; set; }
        public double version { get; set; }
    }

    public class ShortCodPlayers
    {
        public string name { get; set; }        
        public double version { get; set; }
    }
}
