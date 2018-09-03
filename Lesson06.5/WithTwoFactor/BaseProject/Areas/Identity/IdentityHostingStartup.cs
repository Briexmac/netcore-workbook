using BaseProject.Areas.Identity.Data;
using BaseProject.Areas.Identity.Services;
using BaseProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
                services.AddDbContext<IdentityDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BaseProject")));

                services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<IdentityDbContext>()
                    .AddDefaultTokenProviders();

                services.AddScoped<IEmailSender, TwilioSmsSender>();
            });
        }
    }
}