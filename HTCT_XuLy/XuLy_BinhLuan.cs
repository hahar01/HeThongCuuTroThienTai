using HTCT_Entities;
using HTCT_LuuTru;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_XuLy
{
    public class XuLy_BinhLuan
    {
        private readonly LuuTru_BanTin _luuTruBanTin;
        private readonly LuuTru_BinhLuan _luuTruBinhLuan;

        public XuLy_BinhLuan(LuuTru_BanTin luuTruBanTin, LuuTru_BinhLuan luuTruBinhLuan)
        {
            _luuTruBanTin = luuTruBanTin;
            _luuTruBinhLuan = luuTruBinhLuan;
        }

        // Lấy danh sách bình luận theo bài viết
        public List<BinhLuan> LayBinhLuanTheoBanTin(int maBanTin)
        {
            return _luuTruBinhLuan.GetBinhLuansByBanTin(maBanTin);
        }

        // Thêm bình luận mới
        public bool ThemBinhLuan(int maBanTin, string noiDung, string nguoiBinhLuan)
        {
            if (string.IsNullOrWhiteSpace(noiDung) || string.IsNullOrWhiteSpace(nguoiBinhLuan))
            {
                return false; 
            }
            var binhLuan = new BinhLuan
            {
                MaBanTin = maBanTin,
                NoiDung = noiDung,
                NguoiBinhLuan = nguoiBinhLuan,
                ThoiGian = DateTime.Now,
                IsDeleted = false
            };

            _luuTruBinhLuan.AddBinhLuan(binhLuan);
            return true;
        }

        // Xóa bình luận
        public bool XoaBinhLuan(int maBinhLuan)
        {
            var binhLuan = _luuTruBinhLuan.GetBinhLuansByBanTin(maBinhLuan);
            if (binhLuan != null)
            {
                _luuTruBinhLuan.DeleteBinhLuan(maBinhLuan);
                return true;
            }
            return false;
        }

        // Sửa bình luận
        public bool SuaBinhLuan(int maBinhLuan, string noiDung)
        {
            if (string.IsNullOrWhiteSpace(noiDung))
            {
                return false; // Nội dung không hợp lệ
            }

            _luuTruBinhLuan.UpdateBinhLuan(maBinhLuan, noiDung);
            return true;
        }
        // Lấy thông tin bản tin
        public BanTinAdmin LayBanTin(int maBanTin)
        {
            return _luuTruBanTin.GetBanTinAdminById(maBanTin);
        }

        
    }
}
