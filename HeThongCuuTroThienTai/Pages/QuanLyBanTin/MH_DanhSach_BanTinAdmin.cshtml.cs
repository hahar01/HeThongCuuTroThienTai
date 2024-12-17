using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.QuanLyBanTin
{
    public class MH_DanhSach_BanTinAdminModel : PageModel
    {
        private readonly XuLy_BanTin _xuLyBanTin;

        public List<BanTinAdmin> BanTinAdmins { get; set; }

        public MH_DanhSach_BanTinAdminModel(XuLy_BanTin xuLyBanTin)
        {
            _xuLyBanTin = xuLyBanTin;
        }

        public void OnGet()
        {
            var maAdmin = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(maAdmin))
            {
                ModelState.AddModelError(string.Empty, "Bạn chưa đăng nhập hoặc không phải là Admin.");
                BanTinAdmins = new List<BanTinAdmin>();
                return;
            }

            // Lấy danh sách bản tin theo MaAdmin
            BanTinAdmins = _xuLyBanTin.LayBanTinAdminTheoMaAdmin(int.Parse(maAdmin));
        }
        public IActionResult OnPostCongKhaiBanTinAdmin(int maBanTin)
        {
            try
            {
               
                // Kiểm tra và công khai bản tin
                _xuLyBanTin.CongKhaiBanTin(maBanTin);

                // Thông báo thành công
                TempData["SuccessMessage"] = "Bản tin đã được công khai.";
                return RedirectToPage("/QuanLyBanTin/MH_DanhSach_BanTinAdmin");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                TempData["ErrorMessage"] = $"Đã xảy ra lỗi: {ex.Message}";
                return RedirectToPage("/QuanLyBanTin/MH_DanhSach_BanTinAdmin");
            }
        }
    }
}
