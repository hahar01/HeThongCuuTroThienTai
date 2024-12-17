using HTCT_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_LuuTru
{
    public class LuuTru_BanTinTichHop
    {
        private readonly HTContext _context;

        public LuuTru_BanTinTichHop(HTContext context)
        {
            _context = context;
        }
        // Thêm bản tin
        public void AddBanTinTichHop(BanTinTichHop banTin)
        {
            if (banTin == null)
            {
                throw new ArgumentNullException(nameof(banTin), "Bản tin không được null");
            }

            _context.BanTinTichHop.Add(banTin);
            _context.SaveChanges();
        }

        // Cập nhật bản tin
        public void UpdateBanTinTichHop(BanTinTichHop banTinTichHop)
        {
            if (banTinTichHop == null)
                throw new ArgumentNullException(nameof(banTinTichHop), "Bản tin không được null.");

            _context.BanTinTichHop.Update(banTinTichHop);
            _context.SaveChanges();
        }

        // Lấy tất cả bản tin
        public List<BanTinTichHop> GetAllBanTinTichHop()
        {
            return _context.BanTinTichHop.ToList();
        }

        // Lấy tất cả bản tin thei mã tích hợp
        public BanTinTichHop GetBanTinTichHopById(int maTinTichHop)
        {
            return _context.BanTinTichHop.FirstOrDefault(bt => bt.MaTinTichHop == maTinTichHop);
        }
    }
}
