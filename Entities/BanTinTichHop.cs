using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_Entities
{
    public class BanTinTichHop
    {
        public int MaTinTichHop { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string URL { get; set; }
        public string Nguon { get; set; }
        public DateTime NgayTichHop { get; set; }
        public bool IsHidden { get; set; }
        public string LoaiBanTin { get; set; }
        public int MaAdmin { get; set; }

    }
}
