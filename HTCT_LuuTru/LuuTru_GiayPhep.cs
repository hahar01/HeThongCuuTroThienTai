using Entities;
using HTCT_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_LuuTru
{
    public class LuuTru_GiayPhep
    {
        private readonly HTContext _context;

        public LuuTru_GiayPhep(HTContext context)
        {
            _context = context;
        }
        //Thêm giấy phép
        public void AddGiayPhep(GiayPhep giayPhep)
        {
            _context.GiayPhep.Add(giayPhep);
            _context.SaveChanges();
        }
        //Lấy giấy phép theo id
        public GiayPhep GetGiayPhepById(int maGiayPhep)
        {
            return _context.GiayPhep.FirstOrDefault(gp => gp.MaGiayPhep == maGiayPhep);
        }
        //Lấy giấy phép theo số quyết định
        public GiayPhep GetGiayPhepBySoQuyetDinh(string soQuyetDinh)
        {
            return _context.GiayPhep
                .FirstOrDefault(gp => gp.SoQuyetDinh == soQuyetDinh);  // Kiểm tra theo SoQuyetDinh
        }
        //Cập nhật giấy phép
        public void UpdateGiayPhep(GiayPhep giayPhep)
        {
            _context.GiayPhep.Update(giayPhep);
            _context.SaveChanges();
        }
       
    }
}
