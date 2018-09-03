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
                services.AddDbContext<MyIdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BaseProject")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<MyIdentityContext>();
            });
        }
    }
}