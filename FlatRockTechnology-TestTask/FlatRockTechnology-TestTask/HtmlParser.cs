using System.Text;
using FlatRockTechnology_TestTask;
using HtmlAgilityPack;

public class HtmlParser
{
    public static HtmlDocument LoadHtmlDocument()
    {
        HtmlDocument doc = new HtmlDocument();
        var html = ReadText(Constants.FilePath);
        doc.LoadHtml(html);

        return doc;
    }
    private static string ReadText(string filePath)
    {
        return File.ReadAllText(filePath);

    }
    public List<Product> ParseHtml()
    {
        List<Product> productsList = new List<Product>();
        var doc = LoadHtmlDocument();
        var itemDivs = doc.DocumentNode.SelectNodes(Constants.MainItemTag);


        if (itemDivs != null)
        {
            foreach (var itemDiv in itemDivs)
            {
                var productNameNode = itemDiv.SelectSingleNode(Constants.ProductNameTag);
                var priceNode = itemDiv.SelectSingleNode(Constants.PriceTag);
                var ratingAttribute = itemDiv.GetAttributeValue(Constants.RatingTagAttribute, " ");

                if (productNameNode != null && priceNode != null)
                {
                    string productName = HtmlEntity.DeEntitize(productNameNode.InnerText);
                    decimal price = ExtractPrice(priceNode.InnerText);
                    double rating = NormalizeRating(ratingAttribute);

                    Product product = new Product
                    {
                        Name = productName,
                        Price = price,
                        Rating = rating
                    };
                    productsList.Add(product);
                }
            }
        }
        return productsList;
    }

    private decimal ExtractPrice(string priceText)
    {
        StringBuilder numericPriceText = new StringBuilder();
        bool foundDecimalPoint = false;

        foreach (char p in priceText)
        {
            if (char.IsDigit(p))
            {
                numericPriceText.Append(p);
            }
            else if (p == '.' && !foundDecimalPoint)
            {
                numericPriceText.Append(p);
                foundDecimalPoint = true;
            }
        }
        numericPriceText = numericPriceText.Replace(",", "");
        decimal price;
        if (decimal.TryParse(numericPriceText.ToString(), out price))
        {
            price = Math.Round(price, 2);
            return price;
        }
        return 0;
    }
    private double NormalizeRating(string ratingAttribute)
    {
        double rating = 0;
        if (!string.IsNullOrEmpty(ratingAttribute))
        {
            double.TryParse(ratingAttribute, out rating);
            if (rating > 5)
            {
                rating = 5;
            }
        }
        return rating;
    }
}
