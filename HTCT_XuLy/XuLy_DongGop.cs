using HTCT_Entities;
using HTCT_LuuTru;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_XuLy
{
    public class XuLy_DongGop
    {
        private readonly LuuTru_DongGop _luuTruDongGop;

        public XuLy_DongGop(LuuTru_DongGop luuTruDongGop)
        {
            _luuTruDongGop = luuTruDongGop;
        }
        //Thêm đóng góp
        public void ThemDongGop(DongGopTuThien dongGop)
        {
            _luuTruDongGop.AddDongGop(dongGop);
        }
    }
}
