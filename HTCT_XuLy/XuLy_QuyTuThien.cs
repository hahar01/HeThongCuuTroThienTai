using Entities;
using HTCT_Entities;
using HTCT_LuuTru;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace HTCT_XuLy
{
    public class XuLy_QuyTuThien
    {
        private readonly LuuTru_QuyTuThien _luuTruQuyTuThien;
        private readonly LuuTru_GiayPhep _luuTruGiayPhep;

        public XuLy_QuyTuThien(LuuTru_QuyTuThien luuTruQuyTuThien, LuuTru_GiayPhep luuTruGiayPhep)
        {
            _luuTruQuyTuThien = luuTruQuyTuThien;
            _luuTruGiayPhep = luuTruGiayPhep;
        }
        //Đăng ký quỹ
        public void DangKyQuyTuThienTam(QuyTuThien quyTuThien, GiayPhep giayPhep, ISession session)
        {
            // Tạo OTP
            var otp = new Random().Next(1000, 9999).ToString();
            quyTuThien.OTP = otp;
            quyTuThien.OTPExpires = DateTime.Now.AddMinutes(3);

            // Lưu giấy phép tạm thời vào session
            var jsonGiayPhep = JsonConvert.SerializeObject(giayPhep);
            session.SetString("GiayPhepTam", jsonGiayPhep);

            // Lưu thông tin tạm thời vào session
            var jsonQuyTuThien = JsonConvert.SerializeObject(quyTuThien);
            session.SetString("QuyTuThienTam", jsonQuyTuThien);

            // Gửi OTP qua SMS
            SmsSender smsSender = new SmsSender();
            smsSender.SendOtp(quyTuThien.SDT, otp);
        }

        // Xác thực OTP
        public bool XacThucOTP(ISession session, string otpNhap)
        {
            var jsonQuyTuThien = session.GetString("QuyTuThienTam");
            var jsonGiayPhep = session.GetString("GiayPhepTam");

            if (string.IsNullOrEmpty(jsonQuyTuThien) || string.IsNullOrEmpty(jsonGiayPhep))
            {
                throw new Exception("Không tìm thấy thông tin quỹ từ thiện hoặc giấy phép.");
            }

            var quyTuThien = JsonConvert.DeserializeObject<QuyTuThien>(jsonQuyTuThien);
            var giayPhep = JsonConvert.DeserializeObject<GiayPhep>(jsonGiayPhep);

            // Kiểm tra OTP
            if (quyTuThien.OTP == otpNhap && quyTuThien.OTPExpires > DateTime.Now)
            {
                // Xác thực thành công, xóa OTP và trạng thái tạm thời
                quyTuThien.OTP = null;
                quyTuThien.OTPExpires = null;
                quyTuThien.TrangThai = "PendingApproval";  // Cập nhật trạng thái quỹ từ thiện

                // Kiểm tra giấy phép có tồn tại chưa trước khi thêm mới
                var existingGiayPhep = _luuTruGiayPhep.GetGiayPhepBySoQuyetDinh(giayPhep.SoQuyetDinh);
                if (existingGiayPhep == null)
                {
                    // Nếu chưa có, thêm giấy phép mới
                    _luuTruGiayPhep.AddGiayPhep(giayPhep);
                    quyTuThien.MaGiayPhep = giayPhep.MaGiayPhep;  // Lưu mã giấy phép vào Quỹ từ thiện
                }
                else
                {
                    // Nếu đã có, thông báo lỗi
                    throw new Exception("Giấy phép với số quyết định này đã tồn tại.");
                }

                // Lưu quỹ từ thiện vào CSDL
                _luuTruQuyTuThien.AddQuyTuThien(quyTuThien);

                // Xóa thông tin tạm thời trong session chỉ sau khi đã lưu vào cơ sở dữ liệu
                session.Remove("QuyTuThienTam");
                session.Remove("GiayPhepTam");

                return true;
            }
            else
            {
                // OTP không hợp lệ hoặc đã hết hạn
                return false;
            }
        }

        // Lấy Quỹ từ thiện theo email
        public QuyTuThien LayQuyTuThienByEmail(string email)
        {
            return _luuTruQuyTuThien.GetQuyTuThienByEmail(email);
        }

        // Lấy quỹ theo ID
        public QuyTuThien LayQuyTuThienById(int maquyTuThien)
        {
            return _luuTruQuyTuThien.GetQuyTuThienById(maquyTuThien);
        }

        // Lấy danh sách tất cả quỹ
        public List<QuyTuThien> LayAllQuyTuThiens()
        {
            return _luuTruQuyTuThien.GetAllQuyTuThiens();
        }

        // Lấy tên quỹ
        public string LayTenQuyTuThien(int maquyTuThien)
        {
            var quyTuThien = _luuTruQuyTuThien.GetQuyTuThienById(maquyTuThien);
            return quyTuThien?.TenQuyTuThien ?? "Không rõ";
        }
    }
}
