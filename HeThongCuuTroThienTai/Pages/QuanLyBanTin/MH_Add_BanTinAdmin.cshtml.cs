using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.QuanLyBanTin
{
    public class MH_Add_BanTinAdminModel : PageModel
    {
        private readonly XuLy_BanTin _xuLyBanTin;

        public MH_Add_BanTinAdminModel(XuLy_BanTin xuLyBanTin)
        {
            _xuLyBanTin = xuLyBanTin;
        }

        [BindProperty]
        public BanTinAdmin BanTinAdmin { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            var maAdmin = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(maAdmin))
            {
                ModelState.AddModelError(string.Empty, "Bạn chưa đăng nhập hoặc không phải là Admin.");
                return Page();
            }

            BanTinAdmin.NgayTao = DateTime.Now;
            _xuLyBanTin.ThemBanTinAdmin(BanTinAdmin);

            return RedirectToPage("/QuanLyBanTin/MH_DanhSach_BanTinAdmin");
        }
    }
}
