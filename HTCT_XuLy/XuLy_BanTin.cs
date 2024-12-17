using HTCT_Entities;
using HTCT_LuuTru;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_XuLy
{
    public class XuLy_BanTin
    {
        private readonly LuuTru_BanTin _luuTruBanTin;

        public XuLy_BanTin(LuuTru_BanTin luuTruBanTin)
        {
            _luuTruBanTin = luuTruBanTin;
        }

        // Thêm bản tin do Admin tạo
        public void ThemBanTinAdmin(BanTinAdmin banTinAdmin)
        {
            if (banTinAdmin == null || string.IsNullOrEmpty(banTinAdmin.NoiDung))
            {
                throw new ArgumentException("Thông tin bản tin không đầy đủ");
            }

            _luuTruBanTin.AddBanTinAdmin(banTinAdmin);
        }        

        // Lấy tất cả bản tin do Admin tạo
        public List<BanTinAdmin> LayTatCaBanTinAdmin()
        {
            return _luuTruBanTin.GetAllBanTinAdmin();
        }

        // Lấy tất cả bản tin theo ID bản tin     
        public BanTinAdmin LayBanTinAdminTheoId(int maBanTin)
        {
            return _luuTruBanTin.GetBanTinAdminById(maBanTin);
        }

        // Xóa bản tin
        public void XoaMemBanTinAdmin(int maBanTin)
        {
            var banTinAdmin = LayBanTinAdminTheoId(maBanTin);
            if (banTinAdmin == null)
            {
                throw new ArgumentException("Không tìm thấy bản tin.");
            }

            _luuTruBanTin.SoftDeleteBanTinAdmin(maBanTin);
        }

        // Cập nhật bản tin
        public void CapNhatBanTinAdmin(BanTinAdmin banTinAdmin)
        {
            if (banTinAdmin == null || banTinAdmin.MaBanTin <= 0)
            {
                throw new ArgumentException("Thông tin bản tin không hợp lệ.");
            }

            _luuTruBanTin.UpdateBanTinAdmin(banTinAdmin);
        }

        // Lấy tất bản tin theo mã admin
        public List<BanTinAdmin> LayBanTinAdminTheoMaAdmin(int maAdmin)
        {
            return _luuTruBanTin.GetBanTinAdminByMaAdmin(maAdmin);
        }

        // Công khai bản tin
        public void CongKhaiBanTin(int maBanTin)
        {
            // Lấy bản tin theo ID
            var banTin = _luuTruBanTin.GetBanTinAdminById(maBanTin);

            if (banTin == null)
            {
                throw new Exception("Bản tin không tồn tại.");
            }
            if (banTin.IsPublic)
            {
                throw new Exception("Bản tin đã được công khai.");
            }

            // Đánh dấu chiến dịch là công khai
            banTin.IsPublic = true;
            _luuTruBanTin.UpdateBanTinAdmin(banTin);
        }

        // Lấy tất bản tin công khai
        public List<BanTinAdmin> LayTatCaBanTinCongKhai()
        {
            return _luuTruBanTin.GetAllBanTinCongKhai();
        }
    }
}
