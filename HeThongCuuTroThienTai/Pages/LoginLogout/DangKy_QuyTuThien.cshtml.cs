using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.LoginLogout
{
    public class DangKy_QuyTuThienModel : PageModel
    {
        private readonly XuLy_QuyTuThien _xuLyQuyTuThien;

        public DangKy_QuyTuThienModel(XuLy_QuyTuThien xuLyQuyTuThien)
        {
            _xuLyQuyTuThien = xuLyQuyTuThien;
        }

        [BindProperty]
        public QuyTuThien QuyTuThien { get; set; }

        [BindProperty]
        public GiayPhep GiayPhep { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(GiayPhep.CoQuanCap) || string.IsNullOrEmpty(GiayPhep.SoQuyetDinh) || GiayPhep.NgayQuyetDinh == null)
            {
                TempData["ErrorMessage"] = "Thông tin giấy phép chưa đầy đủ!";
                return Page(); // Trả về lại trang nếu thiếu thông tin giấy phép
            }
            _xuLyQuyTuThien.DangKyQuyTuThienTam(QuyTuThien, GiayPhep, HttpContext.Session);
            return RedirectToPage("/XacThucOTP/XacThucOTP_QuyTuThien");

        }
    }
}
