using CompanyBLL.Interfaces;
using CompanyBLL.Repositrios;
using CompanyDAL.Contexts;
using CompanyDAL.Models;
using CompanyPL.Helpers;
using CompanyPL.MapperProfiles;
using CompanyPL.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyPL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<CompanyDBContext>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //mapper profiler
            services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile())); 
            services.AddAutoMapper(M => M.AddProfile(new DepartmentProfile()));
            services.AddAutoMapper(M => M.AddProfile(new UserProfile()));
            services.AddAutoMapper(M => M.AddProfile(new RoleProfile()));


            //3- Send Mail By Milkit
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings")) ;

            services.AddTransient<IMailSettings, EmailSettings>();

            //


            //    ***
            //createAsc ناقصه implmentation 
            services.AddIdentity<ApplicationUser , IdentityRole>(Options =>
            {
                Options.Password.RequireNonAlphanumeric = true;//# $@
                Options.Password.RequireDigit = true;
                //Options.Password.RequireLowercase = true;
                Options.Password.RequireUppercase = true;
				Options.User.RequireUniqueEmail = true;
			}).AddEntityFrameworkStores<CompanyDBContext>().AddDefaultTokenProviders();
			//AddDefaultTokenProviders => محتاج انه يخزنه ف سكيما cookey ,زي localStorage , اباصيها عند AddAuthentication

			// services.AddScoped<UserManager<ApplicationUser>>(); //all
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)//create Tocken
				.AddCookie(Options =>
                {
                    Options.LoginPath = "Account/Login";
                    Options.AccessDeniedPath = "Home/Error";
                });
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
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

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
