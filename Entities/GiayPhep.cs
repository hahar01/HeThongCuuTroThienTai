using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_Entities
{
    public class GiayPhep
    {
        public int MaGiayPhep { get; set; }        
        public string SoQuyetDinh { get; set; }        
        public DateTime NgayQuyetDinh { get; set; }
        public DateTime? NgayHetHan { get; set; }        
        public string CoQuanCap { get; set; }
        public string? HinhAnh { get; set; }
        public bool TrangThai { get; set; }
        public int? MaAdmin { get; set; }

       
    }
}
