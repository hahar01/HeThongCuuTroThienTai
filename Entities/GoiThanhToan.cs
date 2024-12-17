using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_Entities
{
    public class GoiThanhToan
    {
        public int MaGiaoDich { get; set; }
        public decimal SoTien { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayGiaoDich { get; set; }
        public int MaNguoiDung { get; set; }
        public int MaChienDich { get; set; }
        public int MaQuyTuThien { get; set; }
    }
}
