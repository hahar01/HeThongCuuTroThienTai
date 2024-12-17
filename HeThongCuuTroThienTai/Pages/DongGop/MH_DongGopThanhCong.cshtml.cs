using Entities;
using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.DongGop
{
    public class MH_DongGopThanhCongModel : PageModel
    {
        private readonly XuLy_PhieuThu _xuLyPhieuThu;

        private readonly XuLy_NguoiDung _xuLyNguoiDung;
        private readonly XuLy_ChienDich _xuLyChienDich;
        private readonly XuLy_QuyTuThien _xuLyQuyTuThien;

        public MH_DongGopThanhCongModel(XuLy_PhieuThu xuLyPhieuThu, XuLy_NguoiDung xuLyNguoiDung, XuLy_ChienDich xuLyChienDich,
            XuLy_QuyTuThien xuLyQuyTuThien)
        {
            _xuLyPhieuThu = xuLyPhieuThu;
            _xuLyNguoiDung = xuLyNguoiDung;
            _xuLyChienDich = xuLyChienDich;
            _xuLyQuyTuThien = xuLyQuyTuThien;
        }

        [BindProperty]
        public PhieuThu PhieuThu { get; set; }

        public string TenNguoiDung { get; set; }
        public string TenChienDich { get; set; }
        public string TenQuyTuThien { get; set; }

        [BindProperty]
        public bool IsProcessing { get; set; } = false;

        public IActionResult OnGet(int maPhieuThu)
        {
            IsProcessing = true;

            // Lấy thông tin phiếu thu
            PhieuThu = _xuLyPhieuThu.LayPhieuThu(maPhieuThu);

            if (PhieuThu == null)
            {
                return RedirectToPage("/Index");
            }

            // Lấy tên người dùng
            var nguoiDung = _xuLyNguoiDung.LayNguoiDungById(PhieuThu.MaNguoiDung);
            TenNguoiDung = nguoiDung?.HoTen ?? "Không rõ";

            // Lấy tên chiến dịch
            var chienDich = _xuLyChienDich.LayChienDichTheoId(PhieuThu.MaChienDich);
            TenChienDich = chienDich?.TenChienDich ?? "Không rõ";

            // Lấy tên quỹ từ thiện
            var quyTuThien = _xuLyQuyTuThien.LayQuyTuThienById(PhieuThu.MaQuyTuThien);
            TenQuyTuThien = quyTuThien?.TenQuyTuThien ?? "Không rõ";

            IsProcessing = false;

            return Page();

        }

    }
}
