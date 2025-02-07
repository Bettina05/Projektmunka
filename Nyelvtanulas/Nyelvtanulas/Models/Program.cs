using Nyelvtanulas.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 🔹 Session regisztráció
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
    options.Cookie.HttpOnly = true;
});

// 🔹 IoC container - Dependency Injection
builder.Services.AddScoped<IUserManager, DatabaseUserManager>();
builder.Services.AddScoped<IEncryptionService, SHA256EncryptionService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationServiceWithSession>();

// 🔹 Adatbázis beállítás
builder.Services.AddDbContext<UserDbContext>();

// 🔹 Itt kell befejezni a konfigurációt!
var app = builder.Build();

// 🔹 Middleware konfiguráció
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();  // Fontos, hogy itt legyen!
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
