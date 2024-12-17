using HTCT_Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Xml.Linq;
using PdfSharp.Drawing;

namespace HTCT_XuLy
{
    public class XuLy_ThanhToan
    {
        private readonly XuLy_ChienDich _xuLyChienDich;

        public XuLy_ThanhToan(XuLy_ChienDich xuLyChienDich)
        {
            _xuLyChienDich = xuLyChienDich;
        }

        // Phương thức xử lý thanh toán
        public string ThanhToan(DongGopTuThien model)
        {
            switch (model.HinhThucDongGop)
            {
                case "VNPay":
                    return ThanhToanVNPay(model); // Trả về URL thanh toán VNPay
                case "TienMat":
                    return ThanhToanChuyenKhoan(model); // Trả về thông tin thanh toán 
                default:
                    return "Phương thức thanh toán không hợp lệ."; // Trả về thông báo lỗi
            }
        }

        // Xử lý thanh toán VNPay
        private string ThanhToanVNPay(DongGopTuThien model)
        {
            string vnpUrl = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; // URL thanh toán VNPay
            string merchantCode = "A1C0Y23Y"; // Mã Terminal (Website)
            string secretKey = "23TY8PG43LBSSNRKQF64FV1FXQPWWRSH"; // Khóa bí mật

            // Tạo tham số yêu cầu thanh toán VNPay
            var vnpayParams = new Dictionary<string, string>
    {
        { "vnp_Version", "2.1.0" },
        { "vnp_Command", "pay" },
        { "vnp_TmnCode", merchantCode },
        { "vnp_Amount", (model.SoTien * 100).ToString() }, // VNPay yêu cầu số tiền nhân với 100
        { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") },
        { "vnp_CurrCode", "VND" },
        { "vnp_IpAddr", "127.0.0.1" }, // Địa chỉ IP người dùng (thay thế địa chỉ thật)
        { "vnp_Locale", "vn" },
        { "vnp_OrderInfo", "Thanh toán cho chiến dịch " + model.MaChienDich },
        { "vnp_OrderType", "other" }, // Loại đơn hàng (mã danh mục hàng hóa)
        { "vnp_ReturnUrl", "https://htct.ngrok.app/VnPayReturn" }, // URL trả về khi thanh toán xong
        { "vnp_TxnRef", model.MaDongGop.ToString() }, // Mã đơn hàng (hoặc mã giao dịch của bạn)
        { "vnp_ExpireDate", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss")}
    };

            // Sắp xếp các tham số theo thứ tự bảng chữ cái (trừ vnp_SecureHash)
            var queryString = string.Join("&", vnpayParams.OrderBy(x => x.Key).Select(x => $"{x.Key}={x.Value}"));

            // Tạo hashData để tính toán HMACSHA512
            var hashData = queryString + "&" + "vnp_SecureHashType=SHA512&" + secretKey;

            // Tính toán HMACSHA512 để tạo chữ ký
            string calculatedHash;
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(secretKey)))
            {
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(hashData));
                calculatedHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }

            // Thêm chữ ký vào tham số
            vnpayParams.Add("vnp_SecureHash", calculatedHash);

            // Tạo URL thanh toán VNPay hoàn chỉnh với chữ ký
            var fullUrl = vnpUrl + "?" + string.Join("&", vnpayParams.Select(x => $"{x.Key}={x.Value}"));

            // Mã hóa URL
            fullUrl = Uri.EscapeUriString(fullUrl);

            // Trả về URL thanh toán cho controller
            return fullUrl;
        }

        // Tạo chữ ký cho VNPay
       
        private bool ValidateSignature(string vnp_SecureHash, string vnp_HashSecret, Dictionary<string, string> responseData)
        {
            // Sắp xếp tham số theo thứ tự bảng chữ cái
            var queryString = string.Join("&", responseData.OrderBy(x => x.Key).Select(x => $"{x.Key}={x.Value}"));

            // Thêm mã hash và khóa bí mật vào cuối chuỗi
            var hashData = queryString + "&" + "vnp_SecureHashType=SHA512&" + vnp_HashSecret;

            // Tính toán HMACSHA512
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(vnp_HashSecret)))
            {
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(hashData));
                var calculatedHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower(); // Chữ ký dưới dạng hex

                return vnp_SecureHash.Equals(calculatedHash, StringComparison.OrdinalIgnoreCase); // So sánh chữ ký nhận được với chữ ký đã tính toán
            }
        }



        // Xử lý thanh toán VietQR (Chuyển khoản ngân hàng)
        private string ThanhToanChuyenKhoan(DongGopTuThien model)
        {
            var chienDich = _xuLyChienDich.LayChienDichTheoId(model.MaChienDich);
            if (chienDich == null)
            {
                return "Không tìm thấy thông tin chiến dịch.";
            }
         
            return "Thanh toán thành công!";
        }
    }
}




