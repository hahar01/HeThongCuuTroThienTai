using Entities;
using HTCT_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_LuuTru
{
    public class LuuTru_DongGop
    {
        private readonly HTContext _context;

        public LuuTru_DongGop(HTContext context)
        {
            _context = context;
        }
        //Thêm đóng góp
        public void AddDongGop(DongGopTuThien dongGop)
        {
            _context.DongGopTuThien.Add(dongGop);
            _context.SaveChanges();
        }

    }
}
