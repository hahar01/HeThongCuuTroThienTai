using HTCT_Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_LuuTru
{
    public class LuuTru_PhieuThu
    {
        private readonly HTContext _context;

        public LuuTru_PhieuThu(HTContext context)
        {
            _context = context;
        }
        // Thêm phiếu thu
        public void AddPhieuThu(PhieuThu phieuThu)
        {
            if (!_context.QuyTuThien.Any(q => q.MaQuyTuThien == phieuThu.MaQuyTuThien))
            {
                throw new Exception($"Quỹ từ thiện với MaQuyTuThien = {phieuThu.MaQuyTuThien} không tồn tại.");
            }

            if (!_context.ChienDich.Any(c => c.MaChienDich == phieuThu.MaChienDich && c.MaQuyTuThien == phieuThu.MaQuyTuThien))
            {
                throw new Exception($"Chiến dịch với MaChienDich = {phieuThu.MaChienDich} không hợp lệ hoặc không liên kết đúng với Quỹ từ thiện.");
            }

            _context.PhieuThu.Add(phieuThu);
            _context.SaveChanges();
        }

        // Lấy phiếu thu theo ID
        public PhieuThu GetPhieuThuById(int maPhieuThu)
        {
            return _context.PhieuThu.FirstOrDefault(pt => pt.MaPhieuThu == maPhieuThu);
        }

        // Lấy phiếu thu theo ID người dùng
        public List<PhieuThu> GetDanhSachPhieuThuTheoNguoiDung(int maNguoiDung)
        {
            return _context.PhieuThu.Where(p => p.MaNguoiDung == maNguoiDung).ToList();
        }

        // Lấy tất cả phiếu thu
        public List<PhieuThu> GetDanhSachPhieuThu()
        {
            return _context.PhieuThu.ToList();
        }

        // Lấy tất cả phiếu thu theo mã chiến dịch
        public List<PhieuThu> GetPhieuThuTheoChienDich(int maChienDich)
        {
            return _context.PhieuThu.Where(p => p.MaChienDich == maChienDich)
                            .Include(p => p.ChienDich)
                            .Include(p => p.QuyTuThien)
                            .Include(p => p.NguoiDung)
                            .ToList();
        }
    }
}
