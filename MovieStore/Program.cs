using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieStore.Models.Domain;
using MovieStore.Repositories.Abstract;
using MovieStore.Repositories.Implementation;

namespace MovieStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DataBaseContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

            builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
                .AddEntityFrameworkStores<DataBaseContext>().AddDefaultTokenProviders();
            builder.Services.ConfigureApplicationCookie(op => op.LoginPath = "/UserAuthentication/Login");
            builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

           // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=UserAuthentication}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
