using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages
{
    public class DangKy_NguoiThuHuongModel : PageModel
    {
        private readonly XuLy_NguoiThuHuong _xuLyNguoiThuHuong;

        public DangKy_NguoiThuHuongModel(XuLy_NguoiThuHuong xuLyNguoiThuHuong)
        {
            _xuLyNguoiThuHuong = xuLyNguoiThuHuong;
        }

        [BindProperty]
        public NguoiThuHuong NguoiThuHuong { get; set; }

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
            _xuLyNguoiThuHuong.DangKyNguoiThuHuongTam(NguoiThuHuong, GiayPhep, HttpContext.Session);
            return RedirectToPage("/XacThucOTP/XacThucOTP_NguoiThuHuong");

        }
    }
}
