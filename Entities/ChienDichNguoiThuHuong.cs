using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_Entities
{
    public class ChienDichNguoiThuHuong
    {
        public int MaChienDich { get; set; }
        public int MaNguoiThuHuong { get; set; }    

        public ChienDich ChienDich { get; set; }
        public NguoiThuHuong NguoiThuHuong { get; set; }
    }
}
