using Entities;
using HTCT_Entities;
using HTCT_LuuTru;
using HTCT_XuLy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.LoginLogout
{
    public class DangKy_NguoiDungModel : PageModel
    {
        private readonly XuLy_NguoiDung _xuLyNguoiDung;

        public DangKy_NguoiDungModel(XuLy_NguoiDung xuLyNguoiDung)
        {
            _xuLyNguoiDung = xuLyNguoiDung;
        }

        [BindProperty]
        public NguoiDung NguoiDung { get; set; }

        public IActionResult OnPost()
        {
           
            // Lưu thông tin người dùng tạm thời và gửi OTP
            _xuLyNguoiDung.DangKyNguoiDungTam(NguoiDung, HttpContext.Session);

            return RedirectToPage("/XacThucOTP/XacThucOTP_NguoiDung");
        }

    }
}
