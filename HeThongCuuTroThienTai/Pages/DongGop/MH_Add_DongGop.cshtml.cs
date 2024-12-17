using Entities;
using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.DongGop
{
    public class MH_Add_DongGopModel : PageModel
    {
        private readonly XuLy_ChienDich _xuLyChienDich;
        private readonly XuLy_DongGop _xuLyDongGop;
        private readonly XuLy_PhieuThu _xuLyPhieuThu;
        private readonly XuLy_ThanhToan _xuLyThanhToan;

        public MH_Add_DongGopModel(XuLy_ChienDich xuLyChienDich, XuLy_DongGop xuLyDongGop, XuLy_PhieuThu xuLyPhieuThu, XuLy_ThanhToan xuLyThanhToan)
        {
            _xuLyChienDich = xuLyChienDich;
            _xuLyDongGop = xuLyDongGop;
            _xuLyPhieuThu = xuLyPhieuThu;
            _xuLyThanhToan = xuLyThanhToan;
        }

        [BindProperty]
        public DongGopTuThien DongGop { get; set; }

        [BindProperty]
        public int? MaChienDich { get; set; }

        [BindProperty]
        public int? MaNguoiDung { get; set; }

        public List<NguoiDung> NguoiDungs { get; set; } = new List<NguoiDung>();
        public List<QuyTuThien> QuyTuThiens { get; set; } = new List<QuyTuThien>();
        public List<ChienDich> ChienDichs { get; set; } = new List<ChienDich>();

        public void OnGet(int? maChienDich)
        {
            if (maChienDich.HasValue)
            {
                MaChienDich = maChienDich;
                ChienDichs = _xuLyChienDich.LayTatCaChienDich();
            }
            else
            {
                ChienDichs = _xuLyChienDich.LayTatCaChienDichCongKhai();
            }
        }

        public IActionResult OnPost()
        {
            if (!int.TryParse(HttpContext.Session.GetString("UserId"), out int maNguoiDung))
            {
                TempData["ErrorMessage"] = "Bạn cần đăng nhập để đóng góp.";
                return RedirectToPage("/LoginLogout/DangNhap");
            }

            DongGop.MaNguoiDung = maNguoiDung;

            var chienDich = _xuLyChienDich.LayChienDichTheoId(DongGop.MaChienDich);
            if (chienDich == null)
            {
                ModelState.AddModelError(string.Empty, "Chiến dịch không hợp lệ.");
                return Page();
            }

            DongGop.MaQuyTuThien = chienDich.MaQuyTuThien;

            // Xử lý thanh toán theo hình thức người dùng chọn
            string thanhToanUrl = string.Empty;

            if (DongGop.HinhThucDongGop == "VNPay")
            {
                thanhToanUrl = _xuLyThanhToan.ThanhToan(DongGop); // Gọi phương thức xử lý thanh toán VNPay
                return Redirect(thanhToanUrl); // Chuyển hướng đến VNPay
            }
            else if (DongGop.HinhThucDongGop == "ChuyenKhoan")
            {
                thanhToanUrl = _xuLyThanhToan.ThanhToan(DongGop); // Gọi phương thức xử lý 
            }

            // Sau khi thanh toán thành công, tạo phiếu thu và đóng góp
            PhieuThu phieuThu = new PhieuThu
            {
                NgayTao = DongGop.NgayDongGop,
                SoTien = DongGop.SoTien,
                MaNguoiDung = DongGop.MaNguoiDung,
                MaChienDich = DongGop.MaChienDich,
                MaQuyTuThien = DongGop.MaQuyTuThien
            };

            try
            {
                _xuLyPhieuThu.ThemPhieuThu(phieuThu);
                DongGop.MaPhieuThu = phieuThu.MaPhieuThu;
                _xuLyDongGop.ThemDongGop(DongGop);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Lỗi: {ex.Message}");
                return Page();
            }

            ChienDichs = _xuLyChienDich.LayTatCaChienDich();

            return RedirectToPage("/DongGop/MH_DongGopThanhCong", new { maPhieuThu = phieuThu.MaPhieuThu });
        }
    }
}
