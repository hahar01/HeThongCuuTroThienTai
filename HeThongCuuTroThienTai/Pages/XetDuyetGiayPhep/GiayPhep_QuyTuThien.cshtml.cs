using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.XetDuyetGiayPhep
{
    public class GiayPhep_QuyTuThienModel : PageModel
    {
        private readonly XuLy_GiayPhep _xuLyGiayPhep;
        private readonly XuLy_QuyTuThien _xuLyQuyTuThien;

        public GiayPhep_QuyTuThienModel(XuLy_GiayPhep xuLyGiayPhep, XuLy_QuyTuThien xuLyQuyTuThien)
        {
            _xuLyGiayPhep = xuLyGiayPhep;
            _xuLyQuyTuThien = xuLyQuyTuThien; 
        }

        public List<QuyTuThien> QuyTuThienList { get; set; }

        public IActionResult OnGet()
        {
            var isAdmin = HttpContext.Session.GetString("UserRole") == "Admin";  // Lấy quyền từ session

            if (!isAdmin)
            {
                // Nếu không phải admin, chuyển hướng về trang khác
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập trang này.";
                return RedirectToPage("/LoginLogout/DangNhap");
            }

            // Nếu là admin, tiếp tục lấy danh sách quỹ từ thiện
            QuyTuThienList = _xuLyQuyTuThien.LayAllQuyTuThiens();
            return Page();
        }

    }
}
