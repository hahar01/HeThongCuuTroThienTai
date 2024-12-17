using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.XetDuyetGiayPhep
{
    public class GiayPhep_NguoiThuHuongModel : PageModel
    {
        private readonly XuLy_GiayPhep _xuLyGiayPhep;
        private readonly XuLy_NguoiThuHuong _xuLyNguoiThuHuong;

        public GiayPhep_NguoiThuHuongModel(XuLy_GiayPhep xuLyGiayPhep, XuLy_NguoiThuHuong xuLyNguoiThuHuong)
        {
            _xuLyGiayPhep = xuLyGiayPhep;
            _xuLyNguoiThuHuong = xuLyNguoiThuHuong;
        }

        public List<NguoiThuHuong> NguoiThuHuongList { get; set; }

        public IActionResult OnGet()
        {
            var isAdmin = HttpContext.Session.GetString("UserRole") == "Admin";  // Lấy quyền từ session

            if (!isAdmin)
            {
                // Nếu không phải admin, chuyển hướng về trang khác
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập trang này.";
                return RedirectToPage("/LoginLogout/DangNhap");
            }

            // Lấy danh sách quỹ từ thiện từ cơ sở dữ liệu
            NguoiThuHuongList = _xuLyNguoiThuHuong.LayAllNguoiThuHuong();
            return Page();
        }
    }
}
