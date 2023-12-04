// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System.Text.Json.Serialization;
using ExtractProducts;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

void DoCategoryPage(string file, HtmlNode document, StreamWriter writer)
{
    var main = document.QuerySelector("main#content");
    if (main == null)
    {
        main = document.QuerySelector("div.page");
    }
    var sections = main.QuerySelectorAll("section").ToArray();
    var titleSection = sections[0];
    var titleElement = titleSection.QuerySelector("h1");
    var categoryTitle = titleElement.InnerText.Trim();
    var categoryDescription = titleSection.QuerySelector(".elementor-widget-text-editor").InnerText.Trim();
    Console.WriteLine(categoryTitle + " " + categoryDescription);
    var gallery = main.QuerySelector(".elementor-widget-premium-img-gallery");
    var dataSettingsAttr = gallery.Attributes["data-settings"].DeEntitizeValue;
    var content = JsonConvert.DeserializeObject<PremiumGalleryImgContentCollection>(dataSettingsAttr);
    foreach (var img in content.PremiumGalleryImgContentPremiumGalleryImgContent)
    {
        var categories = img.PremiumGalleryImgCategory.Split(",", StringSplitOptions.TrimEntries);
        var name = img.PremiumGalleryImgName;
        var imageUrl = img.PremiumGalleryImg.Url;
        var imgLink = img.PremiumGalleryImgLink.Url;
        var categoriesString = string.Join(";", categories);
        writer.WriteLine($"{Csv(categoryTitle)},{Csv(categoryDescription)},{Csv(name)},{Csv(categoriesString)},{imageUrl},{imgLink}");
    }
}

void DoProductPage(string file, string fileName, string currentUrl, HtmlNode document, StreamWriter writer)
{
    var main = document.QuerySelector("main#content");
    if (main == null)
    {
        main = document.QuerySelector("div.page");
    }
    var sections = main.QuerySelectorAll("section").ToArray();
    var titleElement = sections[0].QuerySelector("h1");
    var title = titleElement.InnerText.Trim();

    for (int i = 1; i < sections.Length; i++)
    {
        var section = sections[i];
        var productTitleElement = section.QuerySelector("h2");
        if (productTitleElement == null) break;
        var product_title = productTitleElement.InnerText.Trim();
        if (product_title.StartsWith("Manufacturer"))
        {
            product_title = title;
        }
        Console.WriteLine(product_title);
        var image_src = section.QuerySelector(".elementor-widget-image img").Attributes["src"].Value;
        Console.WriteLine(image_src);

        var descriptionContainer = section.QuerySelector("div.elementor-widget-heading + div.elementor-widget-text-editor");
        if (descriptionContainer == null)
        {
            descriptionContainer = section.QuerySelector("div.elementor-widget-heading + div.elementor-widget-theme-post-content");
        }

        var description_text = descriptionContainer?.InnerText?.Trim() ?? "Cannot find description";
        Console.WriteLine("Description: " + description_text);

        bool uxbridge = false;
        bool grafton = false;
        bool sharon = false;
        var buttons = section.QuerySelectorAll("a.elementor-button");
        foreach (var button in buttons)
        {
            if (button.InnerText.Contains("Uxbridge"))
            {
                uxbridge = true;
            }

            if (button.InnerText.Contains("Grafton"))
            {
                grafton = true;
            }

            if (button.InnerText.Contains("Sharon"))
            {
                sharon = true;
            }
        }

        var video = section.QuerySelector("div.elementor-widget-heading + div.elementor-widget-video");
        string videoTitle = "";
        string videoUrl = "";
        if (video != null)
        {
            var heading = video.PreviousSibling;
            videoTitle = heading.InnerText.Trim();
            var dataSettings = video.Attributes["data-settings"].DeEntitizeValue;
            var data = JsonConvert.DeserializeObject<JObject>(dataSettings);
            videoUrl = data.Value<string>("youtube_url");
            //Console.WriteLine("Video Title: " + videoTitle);
            //Console.WriteLine("Video Url: " + videoUrl);
        }
        writer.WriteLine($"{currentUrl},{Csv(title)},{Csv(product_title)},{image_src},{Csv(description_text)},{videoTitle},{videoUrl},{uxbridge},{grafton},{sharon}");
    }
}

string Csv(string input)
{
    return '"' + input.Replace(",", "\\,") + '"';
}
var urlFileMap = new Dictionary<string, string>();
var lines = File.ReadAllLines(@"c:\dev\koopman\url-page-files.csv").Skip(1).Select(x => x.Split(','));
foreach (var line in lines)
{
    if (line.Contains("Not found.")) continue;
    var siteUrl = line[0];
    var fileNameUrl = new Uri(line[1]);
    var fileName = fileNameUrl.Segments.Last();
    if (!urlFileMap.ContainsKey(fileName))
    {
        urlFileMap.Add(fileName, siteUrl);
    }
}

var categoryPages = new[]
{
    "/rentals/contractor-rentals/",
    "/rentals/landscaping-rentals/",
    "/rentals/party-and-event-rentals/",
    "/rentals/power-rentals/",
}.ToHashSet();

var products = new StreamWriter(@"c:\dev\koopman\product-list.csv");
products.WriteLine("url,category,product_name,image_url,description,video_title,video_url,uxbridge,grafton,sharon");

var categoryWriter = new StreamWriter(@"c:\dev\koopman\product-category-list.csv");
categoryWriter.WriteLine("category,category_description,name,sub-categories,image_url,image_link");
foreach (var file in Directory.EnumerateFiles(@"c:\dev\koopman", "html*.txt.html"))
{
    var fileName = Path.GetFileName(file);
    string currentUrl = urlFileMap[fileName];
    var doc = new HtmlDocument();
    doc.Load2(file);
    //HtmlNode divNode = doc.DocumentNode.SelectSingleNode("//div");
    //Console.WriteLine(doc.DocumentNode.OuterHtml.Substring(0, 100));
    var document = doc.DocumentNode;
    var categoryPage = categoryPages.Contains(currentUrl);
    if (categoryPage)
    {
        Console.WriteLine(file);
        DoCategoryPage(file, document, categoryWriter);
    }
    if (currentUrl == "/rentals") continue; // skip
    if (currentUrl == "/rentals/") continue; // skip
    if (currentUrl == "/rentals/index.html") continue; // skip
    DoProductPage(file, fileName, currentUrl, document, products);
}

products.Dispose();
categoryWriter.Dispose();