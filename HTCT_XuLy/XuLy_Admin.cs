using Entities;
using HTCT_Entities;
using HTCT_LuuTru;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_XuLy
{
    public class XuLy_Admin
    {
        private readonly LuuTru_Admin _luuTruAdmin;
        private readonly LuuTru_GiayPhep _luuTruGiayPhep;

        public XuLy_Admin(LuuTru_Admin luuTruAdmin, LuuTru_GiayPhep luuTruGiayPhep)
        {
            _luuTruAdmin = luuTruAdmin;
            _luuTruGiayPhep = luuTruGiayPhep;
        }

        // Phương thức lấy danh sách tất cả người dùng theo email
        public Admin LayAdminByEmail(string email)
        {
            return _luuTruAdmin.GetAdminByEmail(email);
        }

        // Phương thức lấy danh sách tất cả người dùng
        public Admin LayAdminById(int maAdmin)
        {
            return _luuTruAdmin.GetAdminById(maAdmin);
        }
    }
}
