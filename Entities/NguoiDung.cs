using HTCT_Entities;

namespace Entities
{
    public class NguoiDung
    {
        public int MaNguoiDung { get; set; }  
        public string HoTen { get; set; }    
        public string DiaChi { get; set; }   
        public string SDT { get; set; }    
        public string Email { get; set; }  
        public string MatKhau { get; set; }    
        public string VaiTro { get; set; } = "NguoiDung";
        public string? OTP { get; set; }
        public DateTime? OTPExpires { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }  
        public string? DeletedBy { get; set; }  

        public ICollection<PhanHoiDanhGia> PhanHoiDanhGias { get; set; }
    }
}
