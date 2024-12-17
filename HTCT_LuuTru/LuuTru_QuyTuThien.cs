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
    public class LuuTru_QuyTuThien
    {
        private readonly HTContext _context;

        public LuuTru_QuyTuThien(HTContext context)
        {
            _context = context;
        }
        // Thêm Quỹ từ thiện
        public void AddQuyTuThien(QuyTuThien quyTuThien)
        {
            _context.QuyTuThien.Add(quyTuThien);
            _context.SaveChanges();
        }

        // Cập nhật Quỹ từ thiện
        public void UpdateQuyTuThien(QuyTuThien quyTuThien)
        {
            _context.QuyTuThien.Update(quyTuThien);
            _context.SaveChanges();
        }

        // Lấy Quỹ từ thiện theo email
        public QuyTuThien GetQuyTuThienByEmail(string email)
        {
            return _context.QuyTuThien.FirstOrDefault(qt => qt.Email == email);
        }

       
        // Lấy danh sách tất cả quỹ
        public List<QuyTuThien> GetAllQuyTuThiens()
        {
            return _context.QuyTuThien.Include(q => q.GiayPhep).ToList();
        }

        // Lấy quỹ theo ID
        public QuyTuThien GetQuyTuThienById(int maQuyTuThien)
        {
            var quyTuThien = _context.QuyTuThien
                    .Include(q => q.GiayPhep)  // Bao gồm thông tin Giấy Phép khi lấy Quỹ Từ Thiện
                    .FirstOrDefault(q => q.MaQuyTuThien == maQuyTuThien);  // Lấy quỹ từ thiện theo id

            return quyTuThien;
        }
               
        // Xóa mềm quỹ
        public void SoftDeleteQuyTuThien(int MaDonVi)
        {
            var QuyTuThien = GetQuyTuThienById(MaDonVi);
            if (QuyTuThien != null)
            {
                QuyTuThien.IsDeleted = true;
                QuyTuThien.DeletedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }        
    }
}
