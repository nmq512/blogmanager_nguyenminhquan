using Microsoft.EntityFrameworkCore;
using Blogmanager_NguyenMinhQuan.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 1. Đăng ký dịch vụ khám phá API Endpoint và Swagger Gen (Buổi 11)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Inject DbContext với SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Cấu hình ASP.NET Core Identity và tích hợp Roles (Buổi 7)
builder.Services.AddDefaultIdentity<IdentityUser>(options => 
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 6;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// 4. Seeding Role (Admin, User) và tài khoản Admin, User mẫu (Buổi 7)
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    
    // Khởi tạo các vai trò
    foreach (var role in new[] { "Admin", "User" })
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
    
    // Khởi tạo tài khoản Admin mẫu
    var adminEmail = "admin@blogmanager.local";
    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
        var result = await userManager.CreateAsync(adminUser, "Admin@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }

    // Khởi tạo tài khoản User mẫu
    var userEmail = "user@blogmanager.local";
    if (await userManager.FindByEmailAsync(userEmail) == null)
    {
        var normalUser = new IdentityUser { UserName = userEmail, Email = userEmail, EmailConfirmed = true };
        var result = await userManager.CreateAsync(normalUser, "User@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(normalUser, "User");
        }
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// 5. Cấu hình Middleware Swagger trong môi trường Development (Buổi 11)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

// 6. THỨ TỰ BẮT BUỘC: Authentication phải đứng trước Authorization (Buổi 7)
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

// 7. Định tuyến cho API Controllers và MVC / Razor Pages
app.MapControllers(); // BẮT BUỘC: Nhận diện các route /api/...[cite: 2]

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages(); // BẮT BUỘC: Nhận diện trang Login/Register của Identity[cite: 3]

app.Run();