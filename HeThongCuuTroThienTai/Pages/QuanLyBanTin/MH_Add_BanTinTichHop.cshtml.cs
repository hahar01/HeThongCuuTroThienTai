using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.QuanLyBanTin
{
   
    public class MH_Add_BanTinTichHopModel : PageModel
    {
        private readonly XuLy_BanTinTichHop _xuLyBanTinTichHop;

        [BindProperty]
        public BanTinTichHop BanTinTichHop { get; set; }

        public MH_Add_BanTinTichHopModel(XuLy_BanTinTichHop xuLyBanTinTichHop)
        {
            _xuLyBanTinTichHop = xuLyBanTinTichHop;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var maAdmin = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(maAdmin))
            {
                ModelState.AddModelError(string.Empty, "Bạn chưa đăng nhập hoặc không phải là Admin.");
                return Page();
            }


            BanTinTichHop.MaAdmin = int.Parse(maAdmin);

            BanTinTichHop.NgayTichHop = DateTime.Now;
            _xuLyBanTinTichHop.ThemBanTinTichHop(BanTinTichHop);

            return RedirectToPage("/QuanLyBanTin/MH_DanhSach_BanTinTichHop");
        }
    }
}
