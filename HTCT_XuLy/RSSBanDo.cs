using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HTCT_XuLy
{
    public class RSSBanDo
    {
        public List<(double Latitude, double Longitude, string Title)> GetFloodLocationsFromRSS(string rssUrl)
        {
            var locations = new List<(double Latitude, double Longitude, string Title)>();

            try
            {
                XDocument rssFeed = XDocument.Load(rssUrl);
                var items = rssFeed.Descendants("item");

                foreach (var item in items)
                {
                    var title = item.Element("title")?.Value;
                    var description = item.Element("description")?.Value;

                    // Xử lý tọa độ từ description nếu chứa thông tin tọa độ
                    if (description != null && description.Contains("Tọa độ:"))
                    {
                        var coords = description.Split("Tọa độ:")[1].Split(',');
                        if (coords.Length == 2 &&
                            double.TryParse(coords[0].Trim(), out double lat) &&
                            double.TryParse(coords[1].Trim(), out double lon))
                        {
                            locations.Add((lat, lon, title));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi đọc RSS: " + ex.Message);
            }

            return locations;
        }
    }
}
