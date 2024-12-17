using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.QuanLyBanTin
{
    public class MH_HienThi_BanTinAdminModel : PageModel
    {
        private readonly XuLy_BanTin _xuLyBanTin;

        public BanTinAdmin BanTinAdmin { get; set; }

        public MH_HienThi_BanTinAdminModel(XuLy_BanTin xuLyBanTin)
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
    }
}
