using Entities;
using HTCT_Entities;
using HTCT_LuuTru;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_XuLy
{
    public class XuLy_NguoiThuHuong
    {
        private readonly LuuTru_NguoiThuHuong _luuTruNguoiThuHuong;
        private readonly LuuTru_GiayPhep _luuTruGiayPhep;

        public XuLy_NguoiThuHuong(LuuTru_NguoiThuHuong luuTruNguoiThuHuong, LuuTru_GiayPhep luuTruGiayPhep)
        {
            _luuTruNguoiThuHuong = luuTruNguoiThuHuong;
            _luuTruGiayPhep = luuTruGiayPhep;
        }
        //Đăng ký
        public void DangKyNguoiThuHuongTam(NguoiThuHuong nguoiThuHuong, GiayPhep giayPhep, ISession session)
        {
            // Tạo OTP
            var otp = new Random().Next(1000, 9999).ToString();
            nguoiThuHuong.OTP = otp;
            nguoiThuHuong.OTPExpires = DateTime.Now.AddMinutes(3);

            // Lưu giấy phép tạm thời vào session
            var jsonGiayPhep = JsonConvert.SerializeObject(giayPhep);
            session.SetString("GiayPhepTam", jsonGiayPhep);

            // Lưu thông tin tạm thời vào session
            var jsonNguoiThuHuong = JsonConvert.SerializeObject(nguoiThuHuong);
            session.SetString("NguoiThuHuongTam", jsonNguoiThuHuong);

            // Gửi OTP qua SMS
            SmsSender smsSender = new SmsSender();
            smsSender.SendOtp(nguoiThuHuong.SDT, otp);
        }

        // Xác thực OTP
        public bool XacThucOTP(ISession session, string otpNhap)
        {
            var jsonNguoiThuHuong = session.GetString("NguoiThuHuongTam");
            var jsonGiayPhep = session.GetString("GiayPhepTam");

            if (string.IsNullOrEmpty(jsonNguoiThuHuong) || string.IsNullOrEmpty(jsonGiayPhep))
            {
                throw new Exception("Không tìm thấy thông tin Người thụ hưởng hoặc giấy phép.");
            }

            var nguoiThuHuong = JsonConvert.DeserializeObject<NguoiThuHuong>(jsonNguoiThuHuong);
            var giayPhep = JsonConvert.DeserializeObject<GiayPhep>(jsonGiayPhep);

            // Kiểm tra OTP
            if (nguoiThuHuong.OTP == otpNhap && nguoiThuHuong.OTPExpires > DateTime.Now)
            {
                // Xác thực thành công, xóa OTP và trạng thái tạm thời
                nguoiThuHuong.OTP = null;
                nguoiThuHuong.OTPExpires = null;
                nguoiThuHuong.TrangThai = "PendingApproval";  // Cập nhật trạng thái quỹ từ thiện

                // Kiểm tra giấy phép có tồn tại chưa trước khi thêm mới
                var existingGiayPhep = _luuTruGiayPhep.GetGiayPhepBySoQuyetDinh(giayPhep.SoQuyetDinh);
                if (existingGiayPhep == null)
                {
                    // Giấy phép chưa tồn tại, thêm mới
                    _luuTruGiayPhep.AddGiayPhep(giayPhep);
                    nguoiThuHuong.MaGiayPhep = giayPhep.MaGiayPhep;  // Lưu mã giấy phép vào QuyTuThien
                }
                else
                {
                    // Giấy phép đã tồn tại, thông báo lỗi
                    throw new Exception("Giấy phép với số quyết định này đã tồn tại.");
                }

                // Lưu quỹ từ thiện vào CSDL
                _luuTruNguoiThuHuong.AddNguoiThuHuong(nguoiThuHuong);

                // Xóa thông tin tạm thời trong session chỉ sau khi đã lưu vào cơ sở dữ liệu
                session.Remove("NguoiThuHuongTam");
                session.Remove("GiayPhepTam");

                return true;
            }
            else
            {
                // OTP không hợp lệ hoặc đã hết hạn
                return false;
            }
        }

        // Cập nhật người dùng
        public void CapNhatNguoiThuHuong(NguoiThuHuong nguoiThuHuong)
        {
            _luuTruNguoiThuHuong.UpdateNguoiThuHuong(nguoiThuHuong);
        }

        // Lấy danh sách tất cả người dùng
        public List<NguoiThuHuong> LayAllNguoiThuHuong()
        {
            return _luuTruNguoiThuHuong.GetAllNguoiThuHuongs();
        }

        // Lấy người dùng theo email
        public NguoiThuHuong GetNguoiThuHuongByEmail(string email)
        {
            return _luuTruNguoiThuHuong.GetNguoiThuHuongByEmail(email);
        }

        // Lấy người dùng theo ID
        public NguoiThuHuong GetNguoiThuHuongById(int maNguoiThuHuong)
        {
            return _luuTruNguoiThuHuong.GetNguoiThuHuongById(maNguoiThuHuong);
        }
    }
}
