using Microsoft.EntityFrameworkCore;
using TinderSwipe.Data;

namespace TinderSwipe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Registro los servicios de controladores con vistas
            builder.Services.AddControllersWithViews();

            // Registro la conexión a la base
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                )
            );

            // Registro los servicios de sesión
            builder.Services.AddDistributedMemoryCache(); // usa memoria como almacenamiento de sesión
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20); // cuánto dura inactiva
                options.Cookie.HttpOnly = true;                 // no accesible por JS
                options.Cookie.IsEssential = true;              // esencial aunque el usuario rechace cookies opcionales
            });

            // Registro los servicios de Cookies
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // duración por defecto
                options.SlidingExpiration = true; // renueva la cookie si el usuario está activo
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Login}/{id?}");


            app.Run();
        }
    }
}
