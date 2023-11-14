using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities.Concrete;
using ApplicationCore.Interfaces;
using Infrastructure.AutoMapper;
using Infrastructure.Context;
using Infrastructure.Context.Identity;
using Infrastructure.Data;



namespace StanbulYapiApp_WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAutoMapper(typeof(Program));


            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            var connectionStringIdentity = builder.Configuration.GetConnectionString("IdentityConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(connectionStringIdentity);
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(x =>
            {
                x.SignIn.RequireConfirmedPhoneNumber = false;
                x.SignIn.RequireConfirmedEmail = false;
                x.SignIn.RequireConfirmedAccount = false;

                x.User.RequireUniqueEmail = true;

                x.Password.RequiredLength = 1;
                x.Password.RequiredUniqueChars = 0;
                x.Password.RequireUppercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<AppIdentityDbContext>();


            builder.Services.ConfigureApplicationCookie(x => x.LoginPath = "/Accounts/LogIn");

            builder.Services.AddScoped(typeof(IRepositoryService<>), typeof(EfRepository<>));

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
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}