using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.XetDuyetGiayPhep
{
    public class MH_XemGiayPhep_QuyTuThienModel : PageModel
    {
        private readonly XuLy_QuyTuThien _xuLyQuyTuThien;
        private readonly XuLy_GiayPhep _xuLyGiayPhep;

        public QuyTuThien QuyTuThien { get; set; }

        public MH_XemGiayPhep_QuyTuThienModel(XuLy_QuyTuThien xuLyQuyTuThien, XuLy_GiayPhep xuLyGiayPhep)
        {
            _xuLyQuyTuThien = xuLyQuyTuThien;
            _xuLyGiayPhep = xuLyGiayPhep;
        }

        public void OnGet(int maQuyTuThien)
        {
            var isAdmin = HttpContext.Session.GetString("UserRole") == "Admin";  // Lấy quyền từ session

            if (!isAdmin)
            {
                // Nếu không phải admin, chuyển hướng về trang khác 
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập trang này.";
                RedirectToPage("/LoginLogout/DangNhap"); 
            }
            
            // Lấy thông tin Quỹ Từ Thiện từ cơ sở dữ liệu
            QuyTuThien = _xuLyQuyTuThien.LayQuyTuThienById(maQuyTuThien);

            if (QuyTuThien == null)
            {
                TempData["ErrorMessage"] = "Quỹ từ thiện không tồn tại.";        
                RedirectToPage("/XetDuyetGiayPhep/GiayPhep_QuyTuThien");
            }

            // Đảm bảo Giấy Phép đã được lấy cùng với Quỹ Từ Thiện
            if (QuyTuThien.GiayPhep == null)
            {
                TempData["ErrorMessage"] = "Giấy phép của quỹ từ thiện chưa được cấp.";         
                RedirectToPage("/XetDuyetGiayPhep/GiayPhep_QuyTuThien");
            }
        }

        public IActionResult OnPostDuyetGiayPhep(int maQuyTuThien)
        {          
            
            try
            {
                // Duyệt giấy phép
                _xuLyGiayPhep.DuyetGiayPhepQuyTuThien(maQuyTuThien);

                // Thêm thông báo và redirect về trang danh sách
                TempData["SuccessMessage"] = "Giấy phép quỹ từ thiện đã được duyệt thành công.";
                return RedirectToPage("/XetDuyetGiayPhep/GiayPhep_QuyTuThien");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToPage("/XetDuyetGiayPhep/GiayPhep_QuyTuThien");
            }
        }
        public IActionResult OnPostTuChoiDuyetGiayPhep(int maQuyTuThien)
        {
            try
            {
                // Từ chối duyệt giấy phép
                _xuLyGiayPhep.TuChoiDuyetGiayPhepQuyTuThien(maQuyTuThien);

                // Thêm thông báo và redirect về trang danh sách
                TempData["SuccessMessage"] = "Giấy phép quỹ từ thiện đã bị từ chối duyệt.";
                return RedirectToPage("/XetDuyetGiayPhep/GiayPhep_QuyTuThien");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToPage("/XetDuyetGiayPhep/GiayPhep_QuyTuThien");
            }
        }
    }
}
