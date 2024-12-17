using Entities;
using HTCT_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_LuuTru
{
    public class LuuTru_Admin
    {
        private readonly HTContext _context;

        public LuuTru_Admin(HTContext context)
        {
            _context = context;
        }
        // Phương thức lấy danh sách tất cả người dùng theo email
        public Admin GetAdminByEmail(string email)
        {
            return _context.Admin.FirstOrDefault(nt => nt.Email == email);
        }

        // Phương thức lấy danh sách tất cả người dùng
        public List<Admin> GetAllAdmins()
        {
            return _context.Admin.ToList();
        }

        // Phương thức tìm người dùng theo ID
        public Admin GetAdminById(int MaAdmin)
        {
            return _context.Admin.FirstOrDefault(ad => ad.MaAdmin == MaAdmin);
        }

        // Phương thức xóa mềm người dùng
        public void SoftDeleteAdmin(int MaAdmin)
        {
            var Admin = GetAdminById(MaAdmin);
            if (Admin != null)
            {
                Admin.IsDeleted = true;
                Admin.DeletedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }


    }
}
