using HTCT_Entities;
using HTCT_LuuTru;
using HTCT_XuLy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.BaoCao
{
    public class MH_BaoCaoModel : PageModel
    {
        public readonly XuLy_ChienDich _xuLyChienDich;
        public readonly XuLy_QuyTuThien _xuLyQuyTuThien;
        private readonly XuLy_PhieuThu _xuLyPhieuThu;
        public readonly XuLy_NguoiDung _xuLyNguoiDung;
        private readonly TaiPDF _taiPDF;

        public MH_BaoCaoModel(XuLy_ChienDich xuLyChienDich, XuLy_QuyTuThien xuLyQuyTuThien, XuLy_PhieuThu xuLyPhieuThu, XuLy_NguoiDung xuLyNguoiDung, TaiPDF taiPDF)
        {
            _xuLyChienDich = xuLyChienDich;
            _xuLyQuyTuThien = xuLyQuyTuThien;
            _xuLyPhieuThu = xuLyPhieuThu;
            _xuLyNguoiDung = xuLyNguoiDung;
            _taiPDF = taiPDF;
        }

        public ChienDich ChienDich { get; set; }
        public List<PhieuThu> DanhSachPhieuThu { get; set; }

        public string TenNguoiDung { get; set; }
        public string TenChienDich { get; set; }
        public string TenQuyTuThien { get; set; }
        public decimal TongSoTien { get; set; }
        public void OnGet(int maChienDich)
        {
            ChienDich = _xuLyChienDich.LayChienDichTheoId(maChienDich);
            // Lấy danh sách phiếu thu từ cơ sở dữ liệu
            DanhSachPhieuThu = _xuLyPhieuThu.LayDanhSachPhieuThu(maChienDich); 

            foreach (var phieu in DanhSachPhieuThu)
            {
                var nguoiDung = _xuLyNguoiDung.LayNguoiDungById(phieu.MaNguoiDung);
                TenNguoiDung = nguoiDung?.HoTen ?? "Không rõ";

                // Lấy tên chiến dịch
                var chienDich = _xuLyChienDich.LayChienDichTheoId(phieu.MaChienDich);
                TenChienDich = chienDich?.TenChienDich ?? "Không rõ";

                // Lấy tên quỹ từ thiện
                var quyTuThien = _xuLyQuyTuThien.LayQuyTuThienById(phieu.MaQuyTuThien);
                TenQuyTuThien = quyTuThien?.TenQuyTuThien ?? "Không rõ";
            }

            TongSoTien = DanhSachPhieuThu.Sum(p => p.SoTien);
        }
        public IActionResult OnPostDownloadPdf(int maChienDich)
        {
            var danhSachPhieuThu = _xuLyPhieuThu.LayDanhSachPhieuThu(maChienDich);
            var pdfContent = _taiPDF.CreatePdf(danhSachPhieuThu);
            return new FileContentResult(pdfContent, "application/pdf")
            {
                FileDownloadName = "BaoCaoDongGop.pdf"
            };
        }
    }
}
