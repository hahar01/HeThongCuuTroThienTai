using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_Entities
{
    public class ChienDich
    {
        public int MaChienDich { get; set; }
        public string TenChienDich { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string? TrangThai { get; set; }
        public string SoTaiKhoan { get; set; } 
        public string TenNganHang { get; set; }
        public string ChiNhanh { get; set; }
        public bool IsPublic { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; } 
        public string? DeletedBy { get; set; }
        public int MaQuyTuThien { get; set; }
        public int? MaNguoiThuHuong { get; set; }
        

        public QuyTuThien QuyTuThien { get; set; }
        public ICollection<ChienDichNguoiThuHuong> ChienDichNguoiThuHuongs { get; set; }
        public ICollection<PhanHoiDanhGia> PhanHoiDanhGias { get; set; }

    }
}
