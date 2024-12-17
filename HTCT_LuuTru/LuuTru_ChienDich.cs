using Entities;
using HTCT_Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_LuuTru
{
    public class LuuTru_ChienDich
    {
        private readonly HTContext _context;
        public LuuTru_ChienDich(HTContext context)
        {
            _context = context;
        }

        // Thêm chiến dịch
        public void AddChienDich(ChienDich chienDich)
        {
            if (chienDich == null)
                throw new ArgumentNullException(nameof(chienDich), "Chiến dịch không được null");

            _context.ChienDich.Add(chienDich);
            _context.SaveChanges(); 
        }

        // Cập nhật chiến dịch
        public void UpdateChienDich(ChienDich chienDich)
        {
            _context.ChienDich.Update(chienDich);
            _context.SaveChanges();
        }

        // Lấy danh sách tất cả chiến dịch
        public List<ChienDich> GetAllChienDichs()
        {
            return _context.ChienDich.Where(cd => !cd.IsDeleted).ToList();
        }

        // Lấy chiến dịch theo ID chiến dịch
        public ChienDich GetChienDichById(int maChienDich)
        {
            return _context.ChienDich.FirstOrDefault(cd => cd.MaChienDich == maChienDich && !cd.IsDeleted);
        }

        // Xóa mềm chiến dịch
        public void SoftDeleteChienDich(int maChienDich)
        {
            var chienDich = GetChienDichById(maChienDich);
            if (chienDich == null)
            {
                throw new ArgumentException("Không tìm thấy chiến dịch", nameof(maChienDich));
            }

            chienDich.IsDeleted = true;
            chienDich.DeletedDate = DateTime.Now;
            _context.SaveChanges();
        }

        // Lấy chiến dịch theo ID Quỹ từ thiện
        public List<ChienDich> GetTatCaChienDichTheoIDQuy(int maQuyTuThien)
        {
            return GetAllChienDichs()
                    .Where(cd => cd.MaQuyTuThien == maQuyTuThien && !cd.IsDeleted)
                    .ToList();
        }

        // Lấy tất cả chiến dịch công khai
        public List<ChienDich> GetAllChienDichCongKhai()
        {
            return _context.ChienDich
                .Where(cd => cd.IsPublic && !cd.IsDeleted)
                .ToList();
        }
    }
}
