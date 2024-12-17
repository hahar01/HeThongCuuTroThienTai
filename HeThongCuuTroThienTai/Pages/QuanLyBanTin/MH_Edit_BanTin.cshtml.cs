using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.QuanLyBanTin
{
    
    public class MH_HienThi_BanTinModel : PageModel
    {
        private readonly XuLy_BanTin _xuLyBanTin;

        [BindProperty]
        public BanTinAdmin BanTinAdmin { get; set; }

        public MH_HienThi_BanTinModel(XuLy_BanTin xuLyBanTin)
        {
            _xuLyBanTin = xuLyBanTin;
        }

        public IActionResult OnGet(int maBanTin)
        {
            BanTinAdmin = _xuLyBanTin.LayBanTinAdminTheoId(maBanTin);
            if (BanTinAdmin == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _xuLyBanTin.CapNhatBanTinAdmin(BanTinAdmin);
            return RedirectToPage("/QuanLyBanTin/MH_DanhSach_BanTinAdmin");
        }
    }
}
