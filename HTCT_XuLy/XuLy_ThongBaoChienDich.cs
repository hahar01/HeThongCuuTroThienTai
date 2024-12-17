using HTCT_Entities;
using HTCT_LuuTru;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_XuLy
{
    public class XuLy_ThongBaoChienDich
    {
        private readonly LuuTru_ThongBaoChienDich _luuTruThongBaoChienDich;

        public XuLy_ThongBaoChienDich(LuuTru_ThongBaoChienDich luuTruThongBaoChienDich)
        {
            _luuTruThongBaoChienDich = luuTruThongBaoChienDich;
        }

        // Thêm chiến dịch
        public void ThemThongBaoChienDich(ThongBaoChienDich thongBaoChienDich)
        {
            if (thongBaoChienDich == null)
            {
                throw new ArgumentNullException(nameof(thongBaoChienDich), "Chiến dịch không được null");
            }
           
            if (string.IsNullOrEmpty(thongBaoChienDich.TenThongBaoChienDich) || string.IsNullOrEmpty(thongBaoChienDich.MoTa))
            {
                throw new ArgumentException("Thông tin chiến dịch không đầy đủ");
            }
            thongBaoChienDich.NgayTao = DateTime.Now;
            
            _luuTruThongBaoChienDich.AddThongBaoChienDich(thongBaoChienDich);
        }

        // Lấy tất cả chiến dịch
        public List<ThongBaoChienDich> LayTatCaThongBaoChienDich()
        {
            return _luuTruThongBaoChienDich.GetAllThongBaoChienDichs();
        }

        // Lấy chiến dịch theo ID
        public ThongBaoChienDich TimThongBaoChienDichTheoId(int maThongBaoChienDich)
        {
            if (maThongBaoChienDich <= 0)
            {
                throw new ArgumentException("Mã chiến dịch không hợp lệ");
            }

            return _luuTruThongBaoChienDich.GetThongBaoChienDichById(maThongBaoChienDich);
        }

        // Cập nhật thông tin chiến dịch
        public void CapNhatThongBaoChienDich(ThongBaoChienDich thongBaoChienDich)
        {
            if (thongBaoChienDich == null || thongBaoChienDich.MaThongBaoChienDich <= 0)
            {
                throw new ArgumentException("Thông tin chiến dịch không hợp lệ");
            }

            _luuTruThongBaoChienDich.UpdateThongBaoChienDich(thongBaoChienDich);
        }

        // Lấy tất cả thông báo của một chiến dịch theo MaChienDich
        public List<ThongBaoChienDich> TimThongBaoChienDichByMaChienDich(int maChienDich)
        {
            return _luuTruThongBaoChienDich.GetThongBaoChienDichByMaChienDich(maChienDich);
        }
    }
}
