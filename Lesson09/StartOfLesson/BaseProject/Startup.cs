using BaseProject.Areas.Identity.Data;
using BaseProject.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BaseProject
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
                builder.AddUserSecrets<Startup>();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var authConfig = services.AddAuthentication();
            var microsoftSection = Configuration.GetSection("Microsoft");

            if (microsoftSection != null)
            {
                var settings = microsoftSection.Get<MicrosoftSettings>();
                if (settings?.Key != null && settings?.Secret != null)
                    authConfig.AddMicrosoftAccount(opts =>
                    {
                        opts.ClientId = settings.Key;
                        opts.ClientSecret = settings.Secret;
                        opts.CallbackPath = "/signin-microsoft";
                    });
            }

            // Comment out if you do not have a local Sql Server installed
            services.AddDbContext<ApplicationContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("BaseProject")));
            // Uncomment if you do not have a local Sql Server installed
            //services.AddDbContext<ApplicationContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("BaseProjectHosted")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            EnsureDatabaseUpdated(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
        }

        private void EnsureDatabaseUpdated(IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationContext>())
                {
                    context.Database.Migrate();
                }
                using (var context = serviceScope.ServiceProvider.GetService<IdentityDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
