using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.QuanLyBanTin
{   
    public class MH_DanhSach_BanTinTichHopModel : PageModel
    {
        private readonly XuLy_BanTinTichHop _xuLyBanTinTichHop;

        public List<BanTinTichHop> BanTinTichHopList { get; set; }

        public MH_DanhSach_BanTinTichHopModel(XuLy_BanTinTichHop xuLyBanTinTichHop)
        {
            _xuLyBanTinTichHop = xuLyBanTinTichHop;
        }

        public void OnGet()
        {
            var maAdmin = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(maAdmin))
            {
                ModelState.AddModelError(string.Empty, "Bạn chưa đăng nhập hoặc không phải là Admin.");
                return;
            }
            // Lấy danh sách các bản tin tích hợp từ lớp xử lý
            BanTinTichHopList = _xuLyBanTinTichHop.LayTatCaBanTinTichHop();
        }
    }
}
