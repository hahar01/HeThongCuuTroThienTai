using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HTCT_Entities;
using Entities;

namespace HTCT_LuuTru
{
    public class HTContext : IdentityDbContext<IdentityUser>
    {
        public HTContext(DbContextOptions<HTContext> options) : base(options)
        {            
        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<NguoiDung> NguoiDung { get; set; }
        public DbSet<NguoiThuHuong> NguoiThuHuong { get; set; }
        public DbSet<QuyTuThien> QuyTuThien { get; set; }
        public DbSet<ChienDich> ChienDich { get; set; }
        public DbSet<ThongBaoChienDich> ThongBaoChienDich { get; set; }
        public DbSet<DongGopTuThien> DongGopTuThien { get; set; }
        public DbSet<PhieuThu> PhieuThu { get; set; }
        public DbSet<GoiThanhToan> GoiThanhToan { get; set; }
        public DbSet<PhanHoiDanhGia> PhanHoiDanhGia { get; set; }
        public DbSet<GiayPhep> GiayPhep { get; set; }
        public DbSet<BanTinAdmin> BanTinAdmin { get; set; }
        public DbSet<BanTinTichHop> BanTinTichHop { get; set; }
        public DbSet<BinhLuan> BinhLuan { get; set; }   
        public DbSet<ChienDichNguoiThuHuong> ChienDichNguoiThuHuong { get; set; }
   

        // Phương thức cấu hình chuỗi kết nối
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=desktop-hbdh3d6\\lgsql2019v1;Persist Security Info=True;User ID=sa;Password=password123!;Initial Catalog=HTCT;TrustServerCertificate=True");
                optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            }
        }

        // Phương thức cấu hình các quan hệ hoặc ràng buộc đặc biệt giữa các bảng
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình khóa chính cho bảng admin
            modelBuilder.Entity<Admin>()
                .HasKey(gd => gd.MaAdmin);
          
            // Cấu hình khóa chính cho bảng người dùng
            modelBuilder.Entity<NguoiDung>()
                .HasKey(nd => nd.MaNguoiDung);

            // Cấu hình khóa chính cho bảng người thụ hưởng
            modelBuilder.Entity<NguoiThuHuong>()
                .HasKey(gd => gd.MaNguoiThuHuong);

            // Cấu hình khóa chính cho bảng quỹ từ thiện
            modelBuilder.Entity<QuyTuThien>()
                .HasKey(gd => gd.MaQuyTuThien);



            // Cấu hình khóa chính cho bảng giấy phép
            modelBuilder.Entity<GiayPhep>()
                .HasKey(gp => gp.MaGiayPhep);

            // Cấu hình mối quan hệ một - một giữa giấy phép và quỹ từ thiện/ người thụ hưởng
            modelBuilder.Entity<QuyTuThien>()
               .HasOne(qt => qt.GiayPhep)
                .WithOne()
               .HasForeignKey<QuyTuThien>(qt => qt.MaGiayPhep);

            modelBuilder.Entity<NguoiThuHuong>()
                .HasOne(qt => qt.GiayPhep)
                 .WithOne()
                .HasForeignKey<NguoiThuHuong>(qt => qt.MaGiayPhep);



            // Cấu hình khóa chính cho bảng chiến dịch
            modelBuilder.Entity<ChienDich>()
                .HasKey(cd => cd.MaChienDich);

            // Cấu hình khóa chính cho bảng thông báo chiến dịch
            modelBuilder.Entity<ThongBaoChienDich>()
                .HasKey(gd => gd.MaThongBaoChienDich);
          
            // Cấu hình quan hệ một-nhiều giữa Chiến Dịch và Quy Từ Thiện
            modelBuilder.Entity<ChienDich>()
                .HasOne(cd => cd.QuyTuThien)
                .WithMany(qt => qt.ChienDiches)
                .HasForeignKey(cd => cd.MaQuyTuThien);


            // Cấu hình mối quan hệ nhiều-nhiều cho bảng trung gian Chiến Dịch - Người Thụ Hưởng
            modelBuilder.Entity<ChienDichNguoiThuHuong>()
                .HasKey(cdnt => new { cdnt.MaChienDich, cdnt.MaNguoiThuHuong });

            // Cấu hình mối quan hệ giữa bảng Chiến Dịch và bảng trung gian
            modelBuilder.Entity<ChienDichNguoiThuHuong>()
                .HasOne(cdnt => cdnt.ChienDich)
                .WithMany(cd => cd.ChienDichNguoiThuHuongs) // ChienDich có nhiều ChienDichNguoiThuHuongs
                .HasForeignKey(cdnt => cdnt.MaChienDich);

            // Cấu hình mối quan hệ giữa bảng Người Thụ Hưởng và bảng trung gian
            modelBuilder.Entity<ChienDichNguoiThuHuong>()
                .HasOne(cdnt => cdnt.NguoiThuHuong)
                .WithMany(nt => nt.ChienDichNguoiThuHuongs) // NguoiThuHuong có nhiều ChienDichNguoiThuHuongs
                .HasForeignKey(cdnt => cdnt.MaNguoiThuHuong);


          
            // Cấu hình khóa chính cho bảng Phản Hồi và Đánh Giá
            modelBuilder.Entity<PhanHoiDanhGia>()
                .HasKey(ph => ph.MaPhanHoi);

            // Cấu hình quan hệ giữa Phản Hồi và Chiến Dịch, Người Dùng, và Người Thụ Hưởng
            modelBuilder.Entity<PhanHoiDanhGia>()
                .HasOne(ph => ph.ChienDich)
                .WithMany(cd => cd.PhanHoiDanhGias)
                .HasForeignKey(ph => ph.MaChienDich);

            modelBuilder.Entity<PhanHoiDanhGia>()
                .HasOne(ph => ph.NguoiDung)
                .WithMany(nd => nd.PhanHoiDanhGias)
                .HasForeignKey(ph => ph.MaNguoiDung);

            modelBuilder.Entity<PhanHoiDanhGia>()
                .HasOne(ph => ph.NguoiThuHuong)
                .WithMany(nt => nt.PhanHoiDanhGias)
                .HasForeignKey(ph => ph.MaNguoiThuHuong);



            // Cấu hình khóa chính cho bảng đóng góp từ thiện
            modelBuilder.Entity<DongGopTuThien>()
                .HasKey(dg => dg.MaDongGop);

            // Cấu hình khóa chính cho bảng phiếu thu
            modelBuilder.Entity<PhieuThu>()
                .HasKey(gd => gd.MaPhieuThu);

            // Cấu hình quan hệ một-nhiều giữa Đóng góp từ thiện và phiếu thu
            modelBuilder.Entity<DongGopTuThien>()
                        .HasOne<PhieuThu>()
                        .WithMany()
                        .HasForeignKey(dg => dg.MaPhieuThu)
                        .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình quan hệ một-nhiều giữa Phiếu thu và Chiến Dịch, Người Dùng, và Người Thụ Hưởng
            modelBuilder.Entity<PhieuThu>()
                .HasOne(p => p.QuyTuThien)
                .WithMany()  // QuyTuThien có thể có nhiều PhieuThu
                .HasForeignKey(p => p.MaQuyTuThien)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PhieuThu>()
                .HasOne(p => p.ChienDich)
                .WithMany()  // ChienDich có thể có nhiều PhieuThu
                .HasForeignKey(p => p.MaChienDich)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PhieuThu>()
                .HasOne(p => p.NguoiDung)
                .WithMany()  // ChienDich có thể có nhiều PhieuThu
                .HasForeignKey(p => p.MaNguoiDung)
                .OnDelete(DeleteBehavior.Restrict);



            // Cấu hình khóa chính cho bảng binh luận
            modelBuilder.Entity<BinhLuan>().ToTable("BinhLuan");

            // Cấu hình khóa chính cho bảng binh luận
            modelBuilder.Entity<BinhLuan>()
                .HasKey(bl => bl.MaBinhLuan);

            // Cấu hình khóa chính cho bảng bản tin
            modelBuilder.Entity<BanTinAdmin>()
                .HasKey(bt => bt.MaBanTin);

            // Cấu hình quan hệ một-nhiều giữa Bản Tin Admin và Bình Luận
            modelBuilder.Entity<BinhLuan>()
                .HasOne(bl => bl.BanTinAdmin)
                .WithMany(bt => bt.BinhLuans)
                .HasForeignKey(bl => bl.MaBanTin);

            modelBuilder.Entity<BanTinAdmin>()
               .HasOne<Admin>()
               .WithMany()
               .HasForeignKey(bt => bt.MaAdmin);

            // Cấu hình khóa chính cho bảng tin tích hợp API
            modelBuilder.Entity<BanTinTichHop>()
                .HasKey(th => th.MaTinTichHop);     

            modelBuilder.Entity<BanTinTichHop>()
                         .HasOne<Admin>()
                        .WithMany()
                        .HasForeignKey(bt => bt.MaAdmin);


            // Cấu hình khóa chính cho bảng gói thanh toán
            modelBuilder.Entity<GoiThanhToan>()
                .HasKey(gd => gd.MaGiaoDich);
        }

    }
}
