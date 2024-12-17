using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_Entities
{
    public class BanTinAdmin
    {
        public int MaBanTin { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public bool IsHidden { get; set; }
        public DateTime NgayTao { get; set; }
        public bool IsPublic { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
        public int MaAdmin { get; set; }


        public ICollection<BinhLuan> BinhLuans { get; set; }
    }
}
