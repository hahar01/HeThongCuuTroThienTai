using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages
{    
    public class MHThem_QuanLyChienDichModel : PageModel
    {
        private readonly XuLy_ChienDich _xuLyChienDich;

        [BindProperty]
        public ChienDich ChienDich { get; set; }

        public MHThem_QuanLyChienDichModel(XuLy_ChienDich xuLyChienDich)
        {
            _xuLyChienDich = xuLyChienDich;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {

            var maQuyTuThien = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(maQuyTuThien))
            {
                ModelState.AddModelError(string.Empty, "Bạn chưa đăng nhập hoặc không phải là Quỹ Từ Thiện.");
                return Page();
            }

            string errorMessage;
            ChienDich.MaQuyTuThien = int.Parse(maQuyTuThien);
   
            _xuLyChienDich.ThemChienDich(ChienDich, out errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                TempData["ErrorMessage"] = errorMessage;
                return Page();
            }

            TempData["SuccessMessage"] = "Chiến dịch đã được thêm thành công!";
            return RedirectToPage("MH_DanhSach_ChienDich");
        }
    }
}
