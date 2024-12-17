using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_Entities
{
    public class NguoiThuHuong
    {
        public int MaNguoiThuHuong { get; set; }  
        public string TenNguoiThuHuong { get; set; }  
        public string DiaChi { get; set; }  
        public string SDT { get; set; }  
        public string Email { get; set; }  
        public string? Website { get; set; }  
        public string MatKhau { get; set; }  
        public string VaiTro { get; set; } = "NguoiThuHuong";  
        public string? OTP { get; set; }  
        public DateTime? OTPExpires { get; set; }  
        public int? MaGiayPhep { get; set; }         
        public bool IsApproved { get; set; } = false;  
        public string TrangThai { get; set; } = "Pending";  
        public bool IsDeleted { get; set; } = false; 
        public DateTime? DeletedDate { get; set; }  
        public string? DeletedBy { get; set; }

        public GiayPhep GiayPhep { get; set; }
        public ICollection<ChienDichNguoiThuHuong> ChienDichNguoiThuHuongs { get; set; } = new List<ChienDichNguoiThuHuong>();
        public ICollection<PhanHoiDanhGia> PhanHoiDanhGias { get; set; } = new List<PhanHoiDanhGia>();
    }
}
