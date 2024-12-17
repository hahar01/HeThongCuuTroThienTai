using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_Entities
{
    public class PhanHoiDanhGia
    {
        public int MaPhanHoi { get; set; }
        public string PhanHoi { get; set; }
        public int DanhGia { get; set; }
        public int MaChienDich { get; set; }
        public int MaQuyTuThien { get; set; }
        public int MaNguoiDung { get; set; }
        public int MaNguoiThuHuong { get; set; }

        public ChienDich ChienDich { get; set; }
        public NguoiDung NguoiDung { get; set; }
        public NguoiThuHuong NguoiThuHuong { get; set; }
    }
}
