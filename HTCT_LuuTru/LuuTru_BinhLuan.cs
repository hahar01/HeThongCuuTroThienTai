using HTCT_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_LuuTru
{
    public class LuuTru_BinhLuan
    {
        private readonly HTContext _context;

        public LuuTru_BinhLuan(HTContext context)
        {
            _context = context;
        }

        // Lấy danh sách bình luận theo bài viết
        public List<BinhLuan> GetBinhLuansByBanTin(int maBanTin)
        {
            return _context.BinhLuan
               .Where(bl => bl.MaBanTin == maBanTin && !bl.IsDeleted)
                .OrderByDescending(bl => bl.ThoiGian)
                .Select(bl => new BinhLuan
                {
                    MaBinhLuan = bl.MaBinhLuan,
                    NoiDung = bl.NoiDung ?? string.Empty, 
                    NguoiBinhLuan = bl.NguoiBinhLuan ?? "Ẩn danh", 
                    ThoiGian = bl.ThoiGian,
                    MaBanTin = bl.MaBanTin,
                    IsDeleted = bl.IsDeleted
                })
                .ToList();
        }

        // Thêm bình luận mới
        public void AddBinhLuan(BinhLuan binhLuan)
        {
            _context.BinhLuan.Add(binhLuan);
            _context.SaveChanges();
        }

        // Xóa mềm bình luận (đánh dấu IsDeleted)
        public void DeleteBinhLuan(int maBinhLuan)
        {
            var binhLuan = _context.BinhLuan.FirstOrDefault(bl => bl.MaBinhLuan == maBinhLuan);
            if (binhLuan != null)
            {
                binhLuan.IsDeleted = true;
                binhLuan.ThoiGian = DateTime.Now;
                _context.SaveChanges();
            }
        }

        // Cập nhật nội dung bình luận
        public void UpdateBinhLuan(int maBinhLuan, string noiDung)
        {
            var binhLuan = _context.BinhLuan.FirstOrDefault(bl => bl.MaBinhLuan == maBinhLuan);
            if (binhLuan != null && !binhLuan.IsDeleted)
            {
                binhLuan.NoiDung = noiDung;
                _context.SaveChanges();
            }
        }

    }
}
