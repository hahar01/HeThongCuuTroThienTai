using HTCT_Entities;
using HTCT_LuuTru;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_XuLy
{
    public class XuLy_PhieuThu
    {
        private readonly LuuTru_PhieuThu _luuTruPhieuThu;

        public XuLy_PhieuThu(LuuTru_PhieuThu luuTruPhieuThu)
        {
            _luuTruPhieuThu = luuTruPhieuThu;
        }
        // Thêm phiếu thu
        public void ThemPhieuThu(PhieuThu phieuThu)
        {
            _luuTruPhieuThu.AddPhieuThu(phieuThu);
        }

        // Lấy phiếu thu theo ID
        public PhieuThu LayPhieuThu(int maPhieuThu)
        {
            return _luuTruPhieuThu.GetPhieuThuById(maPhieuThu);
        }

        // Lấy phiếu thu theo ID người dùng
        public List<PhieuThu> LayDanhSachPhieuThuTheoNguoiDung(int maNguoiDung)
        {
            return _luuTruPhieuThu.GetDanhSachPhieuThuTheoNguoiDung(maNguoiDung);
        }

        // Lấy tất cả phiếu thu theo mã chiến dịch
        public List<PhieuThu> LayDanhSachPhieuThu(int maChienDich)
        {
            return _luuTruPhieuThu.GetPhieuThuTheoChienDich(maChienDich);
        }
       
    }
}
