using Newtonsoft.Json;

namespace FlatRockTechnology_TestTask
{
    public class Product
    {
        [JsonProperty("productName")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("rating")]
        public double Rating { get; set; }
    }
}
