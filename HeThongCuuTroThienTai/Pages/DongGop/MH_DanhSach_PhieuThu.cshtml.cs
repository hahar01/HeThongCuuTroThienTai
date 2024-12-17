using Entities;
using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.DongGop
{
    public class MH_DanhSach_PhieuThuModel : PageModel
    {
        private readonly XuLy_PhieuThu _xuLyPhieuThu;
        public readonly XuLy_NguoiDung _xuLyNguoiDung;
        public readonly XuLy_ChienDich _xuLyChienDich;
        public readonly XuLy_QuyTuThien _xuLyQuyTuThien;

        public MH_DanhSach_PhieuThuModel(XuLy_PhieuThu xuLyPhieuThu, XuLy_NguoiDung xuLyNguoiDung,
                                         XuLy_ChienDich xuLyChienDich, XuLy_QuyTuThien xuLyQuyTuThien)
        {
            _xuLyPhieuThu = xuLyPhieuThu;
            _xuLyNguoiDung = xuLyNguoiDung;
            _xuLyChienDich = xuLyChienDich;
            _xuLyQuyTuThien = xuLyQuyTuThien;
        }

        public List<PhieuThu> DanhSachPhieuThu { get; set; } 

        public void OnGet()
        {
            var maNguoiDung = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(maNguoiDung))
            {
                ModelState.AddModelError(string.Empty, "Bạn chưa đăng nhập hoặc không phải là Admin.");
                DanhSachPhieuThu = new List<PhieuThu>();
                return;
            }

            DanhSachPhieuThu = _xuLyPhieuThu.LayDanhSachPhieuThuTheoNguoiDung(int.Parse(maNguoiDung));

        }
    }
}
