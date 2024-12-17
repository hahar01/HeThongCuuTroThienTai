using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages
{
    
    public class MH_Add_ThongTinConModel : PageModel
    {
        private readonly XuLy_ThongBaoChienDich _xuLyThongBaoChienDich;

        [BindProperty]
        public ThongBaoChienDich ThongBaoChienDich { get; set; }

        public int MaChienDich { get; set; }

        public MH_Add_ThongTinConModel(XuLy_ThongBaoChienDich xuLyThongBaoChienDich)
        {
            _xuLyThongBaoChienDich = xuLyThongBaoChienDich;
        }

        public void OnGet(int maChienDich)
        {
            MaChienDich = maChienDich;
            ThongBaoChienDich = new ThongBaoChienDich
            {
                MaChienDich = maChienDich
            };
        }

        public IActionResult OnPost()
        {
            _xuLyThongBaoChienDich.ThemThongBaoChienDich(ThongBaoChienDich);
            return RedirectToPage("/QuanLyChienDich/MH_DanhSach_ChienDich");
        }
    }
}
