using HTCT_Entities;
using HTCT_XuLy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeThongCuuTroThienTai.Pages
{
    
    public class MH_Xem_ChienDichModel : PageModel
    {
        private readonly XuLy_ThongBaoChienDich _xuLyThongBaoChienDich;
        private readonly XuLy_ChienDich _xuLyChienDich;
        public int MaChienDich { get; set; }
        public ChienDich ChienDich { get; set; }
        public List<ThongBaoChienDich> ThongBaoChienDichList { get; set; }
     
        public MH_Xem_ChienDichModel(XuLy_ThongBaoChienDich xuLyThongBaoChienDich, XuLy_ChienDich xuLyChienDich)
        {
            _xuLyThongBaoChienDich = xuLyThongBaoChienDich;
            _xuLyChienDich = xuLyChienDich;
        }

        public void OnGet(int maChienDich)
        {
            // Lấy chi tiết chiến dịch từ mã chiến dịch
            ChienDich = _xuLyChienDich.LayChienDichTheoId(maChienDich);

            // Lấy danh sách thông báo chiến dịch dựa trên MaChienDich
            ThongBaoChienDichList = _xuLyThongBaoChienDich.TimThongBaoChienDichByMaChienDich(maChienDich);
            MaChienDich = maChienDich;
        }
    }
}
