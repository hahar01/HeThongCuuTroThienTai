using Entities;
using HTCT_Entities;
using HTCT_LuuTru;
using HTCT_XuLy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages.Public
{
    public class MH_XemTinModel : PageModel
    {
        private readonly XuLy_BinhLuan _xuLyBinhLuan;
        public BanTinAdmin BanTin { get; set; }
        public List<BinhLuan> BinhLuans { get; set; }

        [BindProperty]
        public string NoiDungBinhLuan { get; set; }

        [BindProperty]
        public string NguoiBinhLuan { get; set; }

        public MH_XemTinModel(XuLy_BinhLuan xuLyBinhLuan)
        {
            _xuLyBinhLuan = xuLyBinhLuan;
        }

        public void OnGet(int maBanTin)
        {
            BanTin = _xuLyBinhLuan.LayBanTin(maBanTin);

            // Lấy danh sách bình luận
            BinhLuans = _xuLyBinhLuan.LayBinhLuanTheoBanTin(maBanTin);

            if (BanTin == null)
            {
                RedirectToPage("/Home"); // Chuyển hướng đến trang lỗi nếu không tìm thấy bản tin
            }
        }

        public IActionResult OnPostThemBinhLuan(int maBanTin)
        {
            if (!string.IsNullOrWhiteSpace(NoiDungBinhLuan) && !string.IsNullOrWhiteSpace(NguoiBinhLuan))
            {
                _xuLyBinhLuan.ThemBinhLuan(maBanTin, NoiDungBinhLuan, NguoiBinhLuan);
                return RedirectToPage(new { maBanTin });
            }

            ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin bình luận.");
            return Page();
        }

        public IActionResult OnPostXoaBinhLuan(int maBinhLuan, int maBanTin)
        {
            if (_xuLyBinhLuan.XoaBinhLuan(maBinhLuan))
            {
                return RedirectToPage(new { maBanTin });
            }

            ModelState.AddModelError("", "Không thể xóa bình luận.");
            return Page();
        }

        public IActionResult OnPostSuaBinhLuan(int maBinhLuan, string noiDung, int maBanTin)
        {
            if (_xuLyBinhLuan.SuaBinhLuan(maBinhLuan, noiDung))
            {
                return RedirectToPage(new { maBanTin });
            }

            ModelState.AddModelError("", "Không thể sửa bình luận.");
            return Page();
        }
    }
}
