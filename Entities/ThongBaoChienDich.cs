using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_Entities
{
    public class ThongBaoChienDich
    {
        public int MaThongBaoChienDich { get; set; }
        public string TenThongBaoChienDich { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayTao { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
        public int MaChienDich { get; set; }     

    }
}
