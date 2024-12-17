using HTCT_LuuTru;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HTCT_Entities;
using HTCT_XuLy;
using HTCT_LuuTru;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddDbContext<HTContext>(options =>
    options.UseSqlServer("Data Source=desktop-hbdh3d6\\lgsql2019v1;Persist Security Info=True;User ID=sa;Password=password123!;Initial Catalog=HTCT;TrustServerCertificate=True"));

// Đăng ký lớp Lưu trữ vào DI container
builder.Services.AddScoped<LuuTru_NguoiDung>();
builder.Services.AddScoped<LuuTru_QuyTuThien>();
builder.Services.AddScoped<LuuTru_NguoiThuHuong>();
builder.Services.AddScoped<LuuTru_GiayPhep>();
builder.Services.AddScoped<LuuTru_ChienDich>();
builder.Services.AddScoped<LuuTru_ThongBaoChienDich>();
builder.Services.AddScoped<LuuTru_BanTin>();
builder.Services.AddScoped<LuuTru_BanTinTichHop>();
builder.Services.AddScoped<LuuTru_Admin>();
builder.Services.AddScoped<LuuTru_DongGop>();
builder.Services.AddScoped<LuuTru_PhieuThu>();
builder.Services.AddScoped<LuuTru_BinhLuan>();



// Đăng ký lớp Xử Lý vào DI container
builder.Services.AddScoped<SmsSender>();
builder.Services.AddScoped<RSSBanDo>();
builder.Services.AddScoped<XuLy_NguoiDung>();
builder.Services.AddScoped<XuLy_QuyTuThien>();
builder.Services.AddScoped<XuLy_NguoiThuHuong>();
builder.Services.AddScoped<XuLy_GiayPhep>();
builder.Services.AddScoped<XuLy_ChienDich>();
builder.Services.AddScoped<XuLy_ThongBaoChienDich>();
builder.Services.AddScoped<XuLy_BanTin>();
builder.Services.AddScoped<XuLy_BanTinTichHop>();
builder.Services.AddScoped<XuLy_Admin>();
builder.Services.AddScoped<XuLy_DongGop>();
builder.Services.AddScoped<XuLy_PhieuThu>();
builder.Services.AddScoped<XuLy_BinhLuan>();
builder.Services.AddScoped<XuLy_ThanhToan>();
builder.Services.AddScoped<TaiPDF>();




// Đăng ký HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Đăng ký Session Middleware
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Đặt thời gian hết hạn của session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



// Thêm Razor Pages
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();



// Thêm middleware sử dụng session trước khi xác thực
app.UseSession();


// Sử dụng xác thực và phân quyền
app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();

app.Run();

async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
{
    string[] roleNames = { "NguoiDongGop", "QuyTuThien", "NguoiThuHuong", "Admin" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}
