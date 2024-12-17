using HTCT_Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages
{
    public class MH_Trang_ChienDichModel : PageModel
    {
        private readonly XuLy_ChienDich _xuLyChienDich;

        public List<ChienDich> ChienDich { get; set; }

        public MH_Trang_ChienDichModel(XuLy_ChienDich xuLyChienDich)
        {
            _xuLyChienDich = xuLyChienDich;
        }

        public void OnGet()
        {
           
            ChienDich = _xuLyChienDich.LayTatCaChienDichCongKhai();
            if (ChienDich == null || !ChienDich.Any())
            {
                TempData["ErrorMessage"] = "Không có chiến dịch công khai.";
                ChienDich = new List<ChienDich>(); 
            }


        }
    }
}
