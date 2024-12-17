using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.XacThucOTP
{
    public class XacThucOTP_NguoiDungModel : PageModel
    {
        private readonly XuLy_NguoiDung _xuLyNguoiDung;

        public XacThucOTP_NguoiDungModel(XuLy_NguoiDung xuLyNguoiDung)
        {
            _xuLyNguoiDung = xuLyNguoiDung;
        }

        [BindProperty(SupportsGet = true)] 
        public int MaNguoiDung { get; set; } 

        [BindProperty]
        public string OTP { get; set; }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(OTP))
            {
                ModelState.AddModelError(string.Empty, "Mã OTP không được bỏ trống.");
                return Page();
            }

            // Xác thực OTP
            bool isOtpValid = _xuLyNguoiDung.XacThucOTP(HttpContext.Session, OTP);

            if (isOtpValid)
            {
                // Xác thực thành công
                return RedirectToPage("/LoginLogout/DangNhap");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Mã OTP không chính xác hoặc đã hết hạn.");
                return Page();
            }
        }
    }
}
