using HTCT_Entities;
using HTCT_LuuTru;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_XuLy
{
    public class XuLy_BanTinTichHop
    {
        private readonly LuuTru_BanTinTichHop _luuTruBanTinTichHop;
             
        public XuLy_BanTinTichHop(LuuTru_BanTinTichHop luuTruBanTinTichHop)
        {
            _luuTruBanTinTichHop = luuTruBanTinTichHop;
        }

        // Thêm bản tin
        public void ThemBanTinTichHop(BanTinTichHop banTinTichHop)
        {
            if (banTinTichHop == null || string.IsNullOrEmpty(banTinTichHop.NoiDung))
            {
                throw new ArgumentException("Thông tin bản tin không đầy đủ");
            }

            _luuTruBanTinTichHop.AddBanTinTichHop(banTinTichHop);
        }

        // Cập nhật bản tin
        public void CapNhatBanTinTichHop(BanTinTichHop banTinTichHop)
        {
            _luuTruBanTinTichHop.UpdateBanTinTichHop(banTinTichHop);
        }
        // Phương thức lấy bản tin tích hợp
        public List<BanTinTichHop> LayTatCaBanTinTichHop()
        {
            return _luuTruBanTinTichHop.GetAllBanTinTichHop();
        }

        // Phương thức lấy bản tin tích hợp theo ID
        public BanTinTichHop LayBanTinTichHopById(int maTinTichHop)
        {
            return _luuTruBanTinTichHop.GetBanTinTichHopById(maTinTichHop);
        }

    }
}
