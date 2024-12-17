using Entities;
using HTCT_Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_LuuTru
{
    public class LuuTru_NguoiDung
    {
        private readonly HTContext _context;

        public LuuTru_NguoiDung(HTContext context)
        {
            _context = context;
        }
        //Thêm người dùng
        public void AddNguoiDung(NguoiDung nguoiDung)
        {
            _context.NguoiDung.Add(nguoiDung);
            _context.SaveChanges();
        }
        //Lấy ngươi dùng theo email
        public NguoiDung GetNguoiDungByEmail(string email)
        {
            return _context.NguoiDung.FirstOrDefault(nd => nd.Email == email);
        }
        //Cập nhật ngươi dùng
        public void UpdateNguoiDung(NguoiDung nguoiDung)
        {
            _context.NguoiDung.Update(nguoiDung);
            _context.SaveChanges();
        }

        // Phương thức lấy danh sách tất cả người dùng
        public List<NguoiDung> GetAllNguoiDungs()
        {
            return _context.NguoiDung.ToList();
        }

        // Lấy người dùng theo ID
        public NguoiDung GetNguoiDungById(int maNguoiDung)
        {
            return _context.NguoiDung.FirstOrDefault(nd => nd.MaNguoiDung == maNguoiDung);
        }

        // Xóa mềm người dùng
        public void SoftDeleteNguoiDung(int MaDonVi)
        {
            var NguoiDung = GetNguoiDungById(MaDonVi);
            if (NguoiDung != null)
            {
                NguoiDung.IsDeleted = true;
                NguoiDung.DeletedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        //Lấy người dùng theo email và pass
        public NguoiDung GetNguoiDungByEmailAndPassword(string email, string password)
        {
            try
            {
                // Tìm người dùng theo email
                var nguoiDung = _context.NguoiDung.FirstOrDefault(nd => nd.Email == email);

                if (nguoiDung == null)
                {
                    return null;
                }

                // So sánh mật khẩu trực tiếp (bỏ qua bước băm mật khẩu)
                if (password == nguoiDung.MatKhau)
                {
                    return nguoiDung;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        

    }
}
