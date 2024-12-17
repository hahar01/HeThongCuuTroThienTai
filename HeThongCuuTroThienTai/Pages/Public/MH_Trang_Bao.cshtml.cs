using HTCT_Entities;
using HTCT_LuuTru;
using HTCT_XuLy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.Public
{
    public class MH_Trang_BaoModel : PageModel
    {
        private readonly XuLy_BanTin _xulyBanTin;
        private readonly XuLy_BanTinTichHop _xulyBanTinTichHop;      
        

        public MH_Trang_BaoModel(XuLy_BanTin xulyBanTin, XuLy_BanTinTichHop xulyBanTinTichHop)
        {
            _xulyBanTin = xulyBanTin;
            _xulyBanTinTichHop = xulyBanTinTichHop;
        }

        public List<BanTinAdmin> BanTinAdmin { get; set; } = new List<BanTinAdmin>();
        public BanTinAdmin BanTinNoiBat => BanTinAdmin.OrderByDescending(b => b.NgayTao).FirstOrDefault();

        public List<BanTinTichHop> BanTinTichHopList { get; set; }

        public void OnGet()
        {
            BanTinAdmin = _xulyBanTin.LayTatCaBanTinCongKhai();
            BanTinTichHopList = _xulyBanTinTichHop.LayTatCaBanTinTichHop();
            if (BanTinAdmin == null || !BanTinAdmin.Any())
            {
                TempData["ErrorMessage"] = "Không có Bản tin công khai.";
                BanTinAdmin = new List<BanTinAdmin>();  
            }
        }
    }
}
