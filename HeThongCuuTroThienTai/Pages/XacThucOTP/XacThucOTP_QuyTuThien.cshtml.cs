using HTCT_XuLy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.XacThucOTP
{
    public class XacThucOTP_QuyTuThienModel : PageModel
    {
        private readonly XuLy_QuyTuThien _xuLyQuyTuThien;

        public XacThucOTP_QuyTuThienModel(XuLy_QuyTuThien xuLyQuyTuThien)
        {
            _xuLyQuyTuThien = xuLyQuyTuThien;
        }

        [BindProperty]
        public string OTP { get; set; }

        [BindProperty(SupportsGet = true)]
        public int MaQuyTuThien { get; set; }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(OTP))
            {
                ModelState.AddModelError(string.Empty, "Mã OTP không được bỏ trống.");
                return Page();
            }

            bool isOtpValid = _xuLyQuyTuThien.XacThucOTP(HttpContext.Session, OTP);

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
