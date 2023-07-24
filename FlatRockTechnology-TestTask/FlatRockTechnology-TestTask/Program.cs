using Newtonsoft.Json;

namespace FlatRockTechnology_TestTask
{
    public class Program
    {
        static void Main(string[] args)
        {
            var objectParser = new HtmlParser();
            var productsList = objectParser.ParseHtml();

            string jsonResult = JsonConvert.SerializeObject(productsList, Formatting.Indented);
            Console.WriteLine(jsonResult);
        }
    }
}
