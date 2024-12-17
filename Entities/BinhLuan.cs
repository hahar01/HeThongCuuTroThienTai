using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_Entities
{
    public class BinhLuan
    {
        public int MaBinhLuan { get; set; }
        public string NoiDung { get; set; }
        public DateTime ThoiGian { get; set; }
        public string NguoiBinhLuan { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
        public int MaBanTin { get; set; }

        public BanTinAdmin BanTinAdmin { get; set; }
    }
}
