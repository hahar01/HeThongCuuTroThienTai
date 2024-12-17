using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.XetDuyetGiayPhep
{
    public class MH_XemGiayPhep_NguoiThuHuongModel : PageModel
    {
        private readonly XuLy_NguoiThuHuong _xuLyNguoiThuHuong;
        private readonly XuLy_GiayPhep _xuLyGiayPhep;

        public NguoiThuHuong NguoiThuHuong { get; set; }

        public MH_XemGiayPhep_NguoiThuHuongModel(XuLy_NguoiThuHuong xuLyNguoiThuHuong, XuLy_GiayPhep xuLyGiayPhep)
        {
            _xuLyNguoiThuHuong = xuLyNguoiThuHuong;
            _xuLyGiayPhep = xuLyGiayPhep;
        }

        public void OnGet(int maNguoiThuHuong)
        {

            var isAdmin = HttpContext.Session.GetString("UserRole") == "Admin";  // Lấy quyền từ session

            if (!isAdmin)
            {
                // Nếu không phải admin, chuyển hướng về trang khác 
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập trang này.";
                RedirectToPage("/LoginLogout/DangNhap");  
            }
            // Lấy thông tin từ cơ sở dữ liệu
            NguoiThuHuong = _xuLyNguoiThuHuong.GetNguoiThuHuongById(maNguoiThuHuong);

            if (NguoiThuHuong == null)
            {
                TempData["ErrorMessage"] = "Quỹ từ thiện không tồn tại.";
                // Redirect về trang danh sách nếu không tìm thấy
                RedirectToPage("/XetDuyetGiayPhep/GiayPhep_NguoiThuHuong");
            }

            // Đảm bảo Giấy Phép đã được lấy cùng
            if (NguoiThuHuong.GiayPhep == null)
            {
                TempData["ErrorMessage"] = "Giấy phép của quỹ từ thiện chưa được cấp.";            
                RedirectToPage("/XetDuyetGiayPhep/GiayPhep_NguoiThuHuong");
            }
        }

        public IActionResult OnPostDuyetGiayPhep(int maNguoiThuHuong)
        {
            try
            {
                // Duyệt giấy phép
                _xuLyGiayPhep.DuyetGiayPhepNguoiThuHuong(maNguoiThuHuong);

                // Thêm thông báo và redirect về trang danh sách
                TempData["SuccessMessage"] = "Giấy phép quỹ từ thiện đã được duyệt thành công.";
                return RedirectToPage("/XetDuyetGiayPhep/GiayPhep_NguoiThuHuong");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToPage("/XetDuyetGiayPhep/GiayPhep_NguoiThuHuong");
            }
        }
        public IActionResult OnPostTuChoiDuyetGiayPhep(int maNguoiThuHuong)
        {
            try
            {
                // Từ chối duyệt giấy phép
                _xuLyGiayPhep.TuChoiDuyetGiayPhepNguoiThuHuong(maNguoiThuHuong);

                // Thêm thông báo và redirect về trang danh sách
                TempData["SuccessMessage"] = "Giấy phép Người thụ hưởng đã bị từ chối duyệt.";
                return RedirectToPage("/XetDuyetGiayPhep/GiayPhep_NguoiThuHuong");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToPage("/XetDuyetGiayPhep/GiayPhep_NguoiThuHuong");
            }
        }
    }
}
