using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace TranslationTool
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("File name:");
            string file = /*@"messages.he.xlf";*/Console.ReadLine();
            XDocument doc = XDocument.Load(file);
            XNamespace df = doc.Root.Name.Namespace;
            XNamespace defaultNameSpace = "urn:oasis:names:tc:xliff:document:1.2";
            foreach (XElement transUnitNode in doc.Descendants(df + "trans-unit"))
            {
                XElement sourceNode = transUnitNode.Element(df + "source");
                string rawValue = sourceNode.Value;

                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri("https://rapidapi.p.rapidapi.com/language/translate/v2"),
                        Headers =
                        {
                            {"x-rapidapi-host", "google-translate1.p.rapidapi.com"},
                            {"x-rapidapi-key", "26094918d7msh47ae88301329141p1e4c87jsn592a8e9d4d1a"},
                        },
                        Content = new FormUrlEncodedContent(new Dictionary<string, string>
                        {
                            {"q", rawValue},
                            {"source", "en"},
                            {"target", "he"},
                        }),
                    };
                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                        var body = await response.Content.ReadAsStringAsync();
                        var translation = JObject.Parse(body)["data"]["translations"][0]["translatedText"].Value<string>();
                        using (StreamWriter sw = new StreamWriter("translated.txt", true, Encoding.Unicode))
                        {
                            await sw.WriteLineAsync(translation);
                        }
                        Console.WriteLine(translation);
                    }
                }

                await Task.Delay(5000);
                sourceNode.AddAfterSelf(new XElement(defaultNameSpace + "target", rawValue));
            }

            doc.Save(@"messages.he.xlf");
        }
    }
}
