using System;
using BaseProject.Areas.Identity.Data;
using BaseProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BaseProject.Areas.Identity.IdentityHostingStartup))]
namespace BaseProject.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BaseProject")));

                services.AddIdentity<User, IdentityRole>(opts =>
                {
                    // this is all very bad, but is fine for class
                    opts.Password.RequireDigit = false;
                    opts.Password.RequiredLength = 1;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequiredUniqueChars = 1;

                    opts.Lockout.MaxFailedAccessAttempts = -1;
                    opts.Lockout.DefaultLockoutTimeSpan = default(TimeSpan);
                    opts.Lockout.AllowedForNewUsers = false;

                    opts.SignIn.RequireConfirmedEmail = false;
                    opts.SignIn.RequireConfirmedPhoneNumber = false;

                    opts.User.RequireUniqueEmail = true;
                    opts.User.AllowedUserNameCharacters = opts.User.AllowedUserNameCharacters; // keep default
                })
                .AddDefaultUI()
                .AddEntityFrameworkStores<IdentityContext>();
            });
        }
    }
}