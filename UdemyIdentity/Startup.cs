using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UdemyIdentity.Context;
using UdemyIdentity.Validators;

namespace UdemyIdentity
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UdemyContext>();

            services.AddIdentity<AppUser, AppRole>
            (
                opt =>
                {
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequiredLength = 1;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireUppercase = false;
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10); // verilen süre kadar hesap kilitlenir (10 dk)
                    opt.Lockout.MaxFailedAccessAttempts = 3; // kullanýcý bilgileri 3 kez yanlýþ girilince kilitlenir
                    opt.SignIn.RequireConfirmedEmail = true; // doðrulanmýþ email þartý
                }
            ).AddErrorDescriber<CustomIdentityValidator>()
            .AddPasswordValidator<CustomPasswordValidator>()
            .AddEntityFrameworkStores<UdemyContext>();

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Home/Index");
                opt.AccessDeniedPath = new PathString("/Home/AccessDenied");
                opt.Cookie.HttpOnly = true;
                opt.Cookie.Name = "UdemyCookie";
                opt.Cookie.SameSite = SameSiteMode.Strict;
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                opt.ExpireTimeSpan = TimeSpan.FromDays(20);
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("FemalePolicy", cnf =>
                {
                    cnf.RequireClaim("gender", "female");
                });
            });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}