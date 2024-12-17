using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages
{
    
    public class MH_ChiTiet_ChienDichModel : PageModel
    {
        private readonly XuLy_ThongBaoChienDich _xuLyThongBaoChienDich;
        private readonly XuLy_ChienDich _xuLyChienDich;

        public ChienDich ChienDich { get; set; }
        public List<ThongBaoChienDich> ThongBaoChienDichList { get; set; }
        public MH_ChiTiet_ChienDichModel(XuLy_ThongBaoChienDich xuLyThongBaoChienDich, XuLy_ChienDich xuLyChienDich)
        {
            _xuLyThongBaoChienDich = xuLyThongBaoChienDich;
            _xuLyChienDich = xuLyChienDich;
        }
        public void OnGet(int maChienDich)
        {     
            ChienDich = _xuLyChienDich.LayChienDichTheoId(maChienDich);

            ThongBaoChienDichList = _xuLyThongBaoChienDich.TimThongBaoChienDichByMaChienDich(maChienDich);
        }
    }
}
