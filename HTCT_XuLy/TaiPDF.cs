using HTCT_Entities;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HTCT_XuLy
{
    public class TaiPDF
    {
        private XuLy_NguoiDung _xuLyNguoiDung;

        public TaiPDF(XuLy_NguoiDung xuLyNguoiDung)
        {
            _xuLyNguoiDung = xuLyNguoiDung;
        }
        public byte[] CreatePdf(List<PhieuThu> danhSachPhieuThu)
        {
            using (var stream = new MemoryStream())
            {
                // Tạo tài liệu PDF
                PdfSharp.Pdf.PdfDocument document = new PdfSharp.Pdf.PdfDocument();  // Sử dụng PdfSharp PdfDocument
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Arial", 12);
                XFont headerFont = new XFont("Arial", 12); 

                // Thêm tiêu đề
                gfx.DrawString("Báo Cáo Đóng Góp", headerFont, XBrushes.Black, new XPoint(100, 100));

                // Tạo tiêu đề cột
                int startX = 100;  // Vị trí bắt đầu cho các cột
                int columnWidth = 100;  // Chiều rộng của mỗi cột
                int rowHeight = 20;  // Chiều cao của mỗi dòng
                int y = 130;  // Vị trí bắt đầu của dòng đầu tiên

                // Vẽ tiêu đề cột
                gfx.DrawString("Mã Phiếu Thu", headerFont, XBrushes.Black, new XPoint(startX, y));
                gfx.DrawString("Người Đóng Góp", headerFont, XBrushes.Black, new XPoint(startX + columnWidth, y));
                gfx.DrawString("Số Tiền", headerFont, XBrushes.Black, new XPoint(startX + 2 * columnWidth, y));
                gfx.DrawString("Ngày Đóng Góp", headerFont, XBrushes.Black, new XPoint(startX + 3 * columnWidth, y));

                y += rowHeight;  // Di chuyển xuống dưới sau khi vẽ tiêu đề

                // Vẽ dữ liệu trong bảng
                foreach (var phieu in danhSachPhieuThu)
                {
                    gfx.DrawString(phieu.MaPhieuThu.ToString(), font, XBrushes.Black, new XPoint(startX, y));
                    gfx.DrawString(phieu.MaNguoiDung.ToString(), font, XBrushes.Black, new XPoint(startX + columnWidth, y));
                    gfx.DrawString(phieu.SoTien.ToString("C"), font, XBrushes.Black, new XPoint(startX + 2 * columnWidth, y));
                    gfx.DrawString(phieu.NgayTao.ToString("dd/MM/yyyy"), font, XBrushes.Black, new XPoint(startX + 3 * columnWidth, y));

                    y += rowHeight;  // Di chuyển xuống dưới sau mỗi dòng
                }

                // Lưu tài liệu vào stream
                document.Save(stream);
                return stream.ToArray();
            }
        }

    }
}
