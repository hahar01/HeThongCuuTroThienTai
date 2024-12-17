using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HTCT_XuLy;

namespace HeThongCuuTroThienTai.Pages
{
    public class MH_BanDoModel : PageModel
    {
        public List<(double Latitude, double Longitude, string Title)> FloodLocations { get; set; } = new List<(double Latitude, double Longitude, string Title)>();

        public JsonResult OnGetRssApi()
        {
            var rssHandler = new RSSBanDo();
            var floodLocations = rssHandler.GetFloodLocationsFromRSS("https://nchmf.gov.vn/Kttv/rss/thoi-tiet-dat-lien-24h-2.rss");
            return new JsonResult(floodLocations);
        }
    }
}
