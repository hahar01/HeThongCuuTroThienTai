using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.LoginLogout
{
    public class DangXuatModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Xóa toàn bộ session của người dùng
            HttpContext.Session.Clear();
            return RedirectToPage("/LoginLogout/DangNhap");
        }
    }
}
