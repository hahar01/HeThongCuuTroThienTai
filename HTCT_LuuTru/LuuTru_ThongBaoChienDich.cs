using HTCT_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_LuuTru
{
    public class LuuTru_ThongBaoChienDich
    {
        private readonly HTContext _context;     
        public LuuTru_ThongBaoChienDich(HTContext context)
        {
            _context = context;
        }

        // Thêm chiến dịch
        public void AddThongBaoChienDich(ThongBaoChienDich thongBaoChienDich)
        {
            if (thongBaoChienDich == null)
                throw new ArgumentNullException(nameof(thongBaoChienDich), "Chiến dịch không được null");

            _context.ThongBaoChienDich.Add(thongBaoChienDich);
            _context.SaveChanges();
        }

        // Lấy danh sách tất cả chiến dịch
        public List<ThongBaoChienDich> GetAllThongBaoChienDichs()
        {
            return _context.ThongBaoChienDich.Where(cd => !cd.IsDeleted).ToList();
        }

        // Tìm chiến dịch theo ID
        public ThongBaoChienDich GetThongBaoChienDichById(int maThongBaoChienDich)
        {
            return _context.ThongBaoChienDich.FirstOrDefault(cd => cd.MaThongBaoChienDich == maThongBaoChienDich && !cd.IsDeleted);
        }
        public void UpdateThongBaoChienDich(ThongBaoChienDich thongBaoChienDich)
        {
            if (thongBaoChienDich == null)
                throw new ArgumentNullException(nameof(thongBaoChienDich), "Chiến dịch không được null");

            _context.ThongBaoChienDich.Update(thongBaoChienDich);
            _context.SaveChanges();
        }

        // Tìm chiến dịch theo ID chiến dịch
        public List<ThongBaoChienDich> GetThongBaoChienDichByMaChienDich(int maChienDich)
        {
         
            return _context.ThongBaoChienDich
                .Where(t => t.MaChienDich == maChienDich) 
                .OrderByDescending(t => t.NgayTao) 
                .ToList(); 
        }
    }
}
