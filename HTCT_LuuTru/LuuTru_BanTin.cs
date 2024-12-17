using HTCT_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_LuuTru
{
    public class LuuTru_BanTin
    {
        private readonly HTContext _context;

        public LuuTru_BanTin(HTContext context)
        {
            _context = context;
        }

        // Thêm bản tin do Admin tạo
        public void AddBanTinAdmin(BanTinAdmin banTinAdmin)
        {
            if (banTinAdmin == null)
                throw new ArgumentNullException(nameof(banTinAdmin), "Bản tin không được null");

            _context.BanTinAdmin.Add(banTinAdmin);
            _context.SaveChanges();
        }

        // Cập nhật bản tin do Admin tạo
        public void UpdateBanTinAdmin(BanTinAdmin banTinAdmin)
        {
            if (banTinAdmin == null)
                throw new ArgumentNullException(nameof(banTinAdmin), "Bản tin không được null.");

            _context.BanTinAdmin.Update(banTinAdmin);
            _context.SaveChanges();
        }

        // Lấy tất cả bản tin do Admin tạo
        public List<BanTinAdmin> GetAllBanTinAdmin()
        {
            return _context.BanTinAdmin.Where(bt => !bt.IsDeleted).ToList();
        }

        // Lấy tất cả bản tin theo mã bản tin
        public BanTinAdmin GetBanTinAdminById(int maBanTin)
        {
            return _context.BanTinAdmin.FirstOrDefault(bt => bt.MaBanTin == maBanTin && !bt.IsDeleted);
        }        

        // Xóa bản tin
        public void SoftDeleteBanTinAdmin(int maBanTin)
        {
            var banTinAdmin = GetBanTinAdminById(maBanTin);
            if (banTinAdmin == null)
            {
                throw new ArgumentException("Không tìm thấy bản tin", nameof(maBanTin));
            }

            banTinAdmin.IsDeleted = true;
            banTinAdmin.DeletedDate = DateTime.Now;
            _context.SaveChanges();
        }

        // Lấy tất cả bản tin theo Admin
        public List<BanTinAdmin> GetBanTinAdminByMaAdmin(int maAdmin)
        {
            return _context.BanTinAdmin
                .Where(bt => bt.MaAdmin == maAdmin && !bt.IsDeleted)
                .ToList();
        }

        // Lấy tất cả bản tin đã công khai
        public List<BanTinAdmin> GetAllBanTinCongKhai()
        {
            return _context.BanTinAdmin.Where(bt => !bt.IsDeleted && bt.IsPublic).ToList();
        }
    }
}
