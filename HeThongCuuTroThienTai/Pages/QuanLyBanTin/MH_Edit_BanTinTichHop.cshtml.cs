using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.QuanLyBanTin
{
   
    public class MH_Edit_BanTinTichHopModel : PageModel
    {
        private readonly XuLy_BanTinTichHop _xuLyBanTinTichHop;

        [BindProperty]
        public BanTinTichHop BanTinTichHop { get; set; }

        public MH_Edit_BanTinTichHopModel(XuLy_BanTinTichHop xuLyBanTinTichHop)
        {
            _xuLyBanTinTichHop = xuLyBanTinTichHop;
        }

        public void OnGet(int maTinTichHop)
        {
            BanTinTichHop = _xuLyBanTinTichHop.LayBanTinTichHopById(maTinTichHop);
        }

        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _xuLyBanTinTichHop.CapNhatBanTinTichHop(BanTinTichHop);
            return RedirectToPage("/QuanLyBanTin/MH_DanhSach_BanTinTichHop");
        }
    }
}
