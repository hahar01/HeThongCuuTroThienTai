using HTCT_Entities;
using HTCT_LuuTru;
using HTCT_XuLy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HeThongCuuTroThienTai.Pages.LoginLogout
{
    public class DangNhapModel : PageModel
    {             
            private readonly XuLy_NguoiDung _xuLyNguoiDung;
            private readonly XuLy_NguoiThuHuong _xuLyNguoiThuHuong;
            private readonly XuLy_QuyTuThien _xuLyQuyTuThien;
            private readonly XuLy_Admin _xuLyAdmin;
        public DangNhapModel(XuLy_NguoiDung xuLyNguoiDung, XuLy_NguoiThuHuong xuLyNguoiThuHuong,
                                 XuLy_QuyTuThien xuLyQuyTuThien, XuLy_Admin xuLyAdmin)
            {
                _xuLyNguoiDung = xuLyNguoiDung;
                _xuLyNguoiThuHuong = xuLyNguoiThuHuong;
                _xuLyQuyTuThien = xuLyQuyTuThien;
                _xuLyAdmin = xuLyAdmin;               
            }

            [BindProperty]
            public string Email { get; set; }

            [BindProperty]
            public string MatKhau { get; set; }

            public IActionResult OnPost()
            {
                // Kiểm tra email và mật khẩu trong các bảng
                var nguoiDung = _xuLyNguoiDung.GetNguoiDungByEmail(Email);
                var nguoiThuHuong = _xuLyNguoiThuHuong.GetNguoiThuHuongByEmail(Email);
                var quyTuThien = _xuLyQuyTuThien.LayQuyTuThienByEmail(Email);
                var admin = _xuLyAdmin.LayAdminByEmail(Email);

            // Nếu không tìm thấy người dùng trong bất kỳ bảng nào
            if (nguoiDung == null && nguoiThuHuong == null && quyTuThien == null && admin == null)
                {
                    ModelState.AddModelError(string.Empty, "Email không tồn tại.");
                    return Page();
                }

                // Kiểm tra mật khẩu và xác thực người dùng
                if (nguoiDung != null && nguoiDung.MatKhau == MatKhau)
                {
                    // Đăng nhập thành công cho người đóng góp
                    HttpContext.Session.SetString("UserId", nguoiDung.MaNguoiDung.ToString());
                    HttpContext.Session.SetString("UserRole", nguoiDung.VaiTro = "NguoiDung");
                    HttpContext.Session.SetString("UserName", nguoiDung.HoTen);
                    return RedirectToPage("/Index");
                }

                if (nguoiThuHuong != null && nguoiThuHuong.MatKhau == MatKhau)
                {
                // Đăng nhập thành công cho người thụ hưởng
                    HttpContext.Session.SetString("UserId", nguoiThuHuong.MaNguoiThuHuong.ToString());
                    HttpContext.Session.SetString("UserRole", nguoiThuHuong.VaiTro = "NguoiThuHuong");
                    HttpContext.Session.SetString("UserName", nguoiThuHuong.TenNguoiThuHuong);
                    return RedirectToPage("/Index");
                }

                if (quyTuThien != null && quyTuThien.MatKhau == MatKhau)
                {
                // Đăng nhập thành công cho quỹ từ thiện 
                    HttpContext.Session.SetString("UserId", quyTuThien.MaQuyTuThien.ToString());
                    HttpContext.Session.SetString("UserRole", quyTuThien.VaiTro = "QuyTuThien");
                    HttpContext.Session.SetString("UserName", quyTuThien.TenQuyTuThien);
                return RedirectToPage("/Index");
                }

                if (admin != null && admin.MatKhau == MatKhau)
                {
                // Đăng nhập thành công cho admin 
                    HttpContext.Session.SetString("UserId", admin.MaAdmin.ToString());
                    HttpContext.Session.SetString("UserRole", admin.VaiTro = "Admin");
                    HttpContext.Session.SetString("UserName", admin.HoTen);
                return RedirectToPage("/Index");
                }


                // Nếu mật khẩu không đúng
                ModelState.AddModelError(string.Empty, "Mật khẩu không chính xác.");
                return Page();
            }


        }
}
