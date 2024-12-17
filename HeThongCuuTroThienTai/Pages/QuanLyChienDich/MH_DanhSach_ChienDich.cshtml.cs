using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages
{
    public class MH_DanhSach_ChienDichModel : PageModel
    {
        private readonly XuLy_ChienDich _xuLyChienDich;

        private readonly XuLy_QuyTuThien _xuLyQuyTuThien; 

        public List<ChienDich> ChienDichs { get; set; }
        public QuyTuThien QuyTuThien { get; set; } 

        public MH_DanhSach_ChienDichModel(XuLy_ChienDich xuLyChienDich, XuLy_QuyTuThien xuLyQuyTuThien)
        {
            _xuLyChienDich = xuLyChienDich;
            _xuLyQuyTuThien = xuLyQuyTuThien;
        }
        public void OnGet()
        {
            var maQuyTuThien = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(maQuyTuThien))
            {
                ModelState.AddModelError(string.Empty, "Bạn chưa đăng nhập.");
                ChienDichs = new List<ChienDich>();
                return;
            }
            QuyTuThien = _xuLyQuyTuThien.LayQuyTuThienById(int.Parse(maQuyTuThien));

            if (QuyTuThien == null || QuyTuThien.TrangThai != "Đã Duyệt")
            {
                TempData["ErrorMessage"] = "Quỹ từ thiện chưa được duyệt. Bạn không thể thêm chiến dịch.";
                ChienDichs = new List<ChienDich>();
                return;
            }
            ChienDichs = _xuLyChienDich.LayChienDichTheoQuyTuThien(int.Parse(maQuyTuThien));           
        }
        public IActionResult OnPostCongKhaiChiendich(int maChienDich)
        {
            try
            {
                _xuLyChienDich.CongKhaiChienDich(maChienDich);
             
                TempData["SuccessMessage"] = "Chiến dịch đã được công khai.";
                return RedirectToPage("/QuanLyChienDich/MH_DanhSach_ChienDich");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToPage("/QuanLyChienDich/MH_DanhSach_ChienDich", new { maChienDich });
            }
        }
    }
}
