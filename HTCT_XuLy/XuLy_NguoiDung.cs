using Entities;
using HTCT_Entities;
using HTCT_LuuTru;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace HTCT_XuLy
{
    public class XuLy_NguoiDung
    {
        private readonly LuuTru_NguoiDung _luuTruNguoiDung;

        public XuLy_NguoiDung(LuuTru_NguoiDung luuTruNguoiDung)
        {
            _luuTruNguoiDung = luuTruNguoiDung;
        }
        // Lưu thông tin người dùng tạm thời và gửi OTP
        public void DangKyNguoiDungTam(NguoiDung nguoiDung, ISession session)
        {
            var otp = new Random().Next(1000, 9999).ToString();
            nguoiDung.OTP = otp;
            nguoiDung.OTPExpires = DateTime.Now.AddMinutes(3);

            // Lưu thông tin người dùng vào session
            var jsonNguoiDung = JsonConvert.SerializeObject(nguoiDung);
            session.SetString("NguoiDungTam", jsonNguoiDung);

            // Gửi OTP qua SMS
            SmsSender smsSender = new SmsSender();
            smsSender.SendOtp(nguoiDung.SDT, otp);
        }

        // Xác thực OTP
        public bool XacThucOTP(ISession session, string otpNhap)
        {
            // Lấy người dùng từ session
            var jsonNguoiDung = session.GetString("NguoiDungTam");
            if (string.IsNullOrEmpty(jsonNguoiDung))
            {
                throw new Exception("Không tìm thấy thông tin người dùng trong phiên làm việc.");
            }

            var nguoiDung = JsonConvert.DeserializeObject<NguoiDung>(jsonNguoiDung);

            // Kiểm tra OTP
            if (nguoiDung.OTP == otpNhap && nguoiDung.OTPExpires > DateTime.Now)
            {
                // Xóa OTP và lưu vào CSDL
                nguoiDung.OTP = null;
                nguoiDung.OTPExpires = null;

                _luuTruNguoiDung.AddNguoiDung(nguoiDung); // Lưu vào CSDL
                session.Remove("NguoiDungTam"); // Xóa thông tin trong session
                return true;
            }

            return false;
        }
        // Lấy người dùng theo ID
        public NguoiDung LayNguoiDungById(int manguoiDung)
        {
            return _luuTruNguoiDung.GetNguoiDungById(manguoiDung);
        }

        //Lấy người dùng theo email và pass
        public NguoiDung GetNguoiDungByEmail(string email)
        {
            return _luuTruNguoiDung.GetNguoiDungByEmail(email);
        }

        public NguoiDung DangNhap(string email, string matKhau)
        {            
            var nguoiDung = _luuTruNguoiDung.GetNguoiDungByEmailAndPassword(email, matKhau);

            if (nguoiDung != null)
            {
                return nguoiDung;
            }

            return null;
        }

        public string LayTenNguoiDung(int maNguoiDung)
        {
            var nguoiDung = _luuTruNguoiDung.GetNguoiDungById(maNguoiDung);
            return nguoiDung?.HoTen ?? "Không rõ";
        }
    }
}

