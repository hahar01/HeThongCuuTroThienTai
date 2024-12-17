using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_Entities
{
    public class Admin
    {
        public int MaAdmin { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }        
        public string MatKhau { get; set; }
        public string SDT { get; set; }
        public string VaiTro { get; set; } = "Admin";
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
    }
}
