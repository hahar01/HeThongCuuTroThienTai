using Entities;
using HTCT_Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_LuuTru
{
    public class LuuTru_NguoiThuHuong
    {
        private readonly HTContext _context;

        public LuuTru_NguoiThuHuong(HTContext context)
        {
            _context = context;
        }
        // Thêm người thụ hưởng
        public void AddNguoiThuHuong(NguoiThuHuong nguoiThuHuong)
        {
            _context.NguoiThuHuong.Add(nguoiThuHuong);
            _context.SaveChanges();
        }

        // Lấy người dùng theo email
        public NguoiThuHuong GetNguoiThuHuongByEmail(string email)
        {
            return _context.NguoiThuHuong.FirstOrDefault(nt => nt.Email == email);
        }

        // Cập nhật người dùng
        public void UpdateNguoiThuHuong(NguoiThuHuong nguoiThuHuong)
        {
            _context.NguoiThuHuong.Update(nguoiThuHuong);
            _context.SaveChanges();
        }

        // Lấy danh sách tất cả người dùng
        public List<NguoiThuHuong> GetAllNguoiThuHuongs()
        {
            return _context.NguoiThuHuong.Include(q => q.GiayPhep).ToList();
        }

        // Lấy người dùng theo ID
        public NguoiThuHuong GetNguoiThuHuongById(int maNguoiThuHuong)
        {
            return _context.NguoiThuHuong
                .Include(q => q.GiayPhep)
                .FirstOrDefault(nd => nd.MaNguoiThuHuong == maNguoiThuHuong);
        }

        // Xóa mềm người dùng
        public void SoftDeleteNguoiThuHuong(int MaDonVi)
        {
            var NguoiThuHuong = GetNguoiThuHuongById(MaDonVi);
            if (NguoiThuHuong != null)
            {
                NguoiThuHuong.IsDeleted = true;
                NguoiThuHuong.DeletedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }
    }
}
