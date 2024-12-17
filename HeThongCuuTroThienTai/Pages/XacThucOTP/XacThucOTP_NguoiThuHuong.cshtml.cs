using HTCT_XuLy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.XacThucOTP
{
    public class XacThucOTP_NguoiThuHuongModel : PageModel
    {
        private readonly XuLy_NguoiThuHuong _xuLyNguoiThuHuong;

        public XacThucOTP_NguoiThuHuongModel(XuLy_NguoiThuHuong xuLyNguoiThuHuong)
        {
            _xuLyNguoiThuHuong = xuLyNguoiThuHuong;
        }

        [BindProperty]
        public string OTP { get; set; }

        [BindProperty(SupportsGet = true)]
        public int MaNguoiThuHuong { get; set; }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(OTP))
            {
                ModelState.AddModelError(string.Empty, "Mã OTP không được bỏ trống.");
                return Page();
            }

            bool isOtpValid = _xuLyNguoiThuHuong.XacThucOTP(HttpContext.Session, OTP);

            if (isOtpValid)
            {
                return RedirectToPage("/XetDuyetGiayPhep/MH_ChoXetDuyet"); 
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Mã OTP không chính xác hoặc đã hết hạn.");
                return Page();
            }
        }
    }
}
