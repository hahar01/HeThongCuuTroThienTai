using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_Entities
{
    public class DongGopTuThien
    {
        public int MaDongGop { get; set; }
        public DateTime NgayDongGop { get; set; }
        public decimal SoTien { get; set; }
        public string HinhThucDongGop { get; set; }
        public int MaNguoiDung { get; set; }
        public int MaChienDich { get; set; }
        public int MaQuyTuThien { get; set; }
        public int? MaPhieuThu { get; set; }

        public DongGopTuThien()
        {
            // Thiết lập NgayDongGop là ngày hiện tại
            NgayDongGop = DateTime.Now;
        }
    }
}
