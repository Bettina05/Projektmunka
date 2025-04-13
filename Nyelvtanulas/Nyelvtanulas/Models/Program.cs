using Lingarix_Database;
using Microsoft.EntityFrameworkCore;
using Nyelvtanulas.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
    options.Cookie.HttpOnly = true;
});

builder.Services.AddScoped<IUserManager, DatabaseUserManager>();
builder.Services.AddScoped<IEncryptionService, SHA256EncryptionService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationServiceWithSession>();

builder.Services.AddDbContext<LingarixDbContext>();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<LingarixDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();  
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
