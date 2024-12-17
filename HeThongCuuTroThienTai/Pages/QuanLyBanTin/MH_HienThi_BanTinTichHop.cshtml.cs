using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.QuanLyBanTin
{
    public class MH_HienThi_BanTinTichHopModel : PageModel
    {
        private readonly XuLy_BanTinTichHop _xuLyBanTinTichHop;

        public BanTinTichHop BanTinTichHop { get; set; }

        public MH_HienThi_BanTinTichHopModel(XuLy_BanTinTichHop xuLyBanTinTichHop)
        {
            _xuLyBanTinTichHop = xuLyBanTinTichHop;
        }

        public void OnGet(int maTinTichHop)
        {
            BanTinTichHop = _xuLyBanTinTichHop.GetBanTinTichHopById(maTinTichHop);
        }
    }
}
