using Hospital.Train.BLL;
using Hospital.Train.BLL.Interfaces;
using Hospital.Train.BLL.Repository;
using Hospital.Train.DAL.Data.Context;
using Hospital.Train.DAL.Models;
using Hospital.Train.PL.Mapping.Consultant;
using Hospital.Train.PL.Mapping.Department;
using Hospital.Train.PL.Mapping.Medicine;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Train.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(option=>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();
            //builder.Services.AddScoped<IConsultantRepository, ConsultantRepository>();
            builder.Services.AddAutoMapper(cfg => {
                cfg.AddProfile<DepartmentProfile>();
            });

            builder.Services.AddAutoMapper(cfg => {
                cfg.AddProfile<ConsultantProfile>();
            });

            builder.Services.AddAutoMapper(cfg => {
                cfg.AddProfile<MedicineProfile>();
            });

            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";
            }
            );



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
