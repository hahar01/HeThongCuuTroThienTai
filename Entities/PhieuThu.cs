using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_Entities
{
    public class PhieuThu
    {
        public int MaPhieuThu { get; set; }
        public DateTime NgayTao { get; set; }
        public decimal SoTien { get; set; }
        public int MaNguoiDung { get; set; }
        public int MaChienDich { get; set; }
        public int MaQuyTuThien { get; set; }

        public ChienDich ChienDich { get; set; } 
        public QuyTuThien QuyTuThien { get; set; } 
        public NguoiDung NguoiDung { get; set; }
    }
}
