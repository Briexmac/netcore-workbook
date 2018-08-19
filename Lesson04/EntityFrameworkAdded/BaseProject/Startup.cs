using System;
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Comment out if you do not have a local Sql Server installed
            //services.AddDbContext<ApplicationContext>(opts => opts.UseSqlServer(GetConnectionString("BaseProject")));
            // Uncomment if you do not have a local Sql Server installed
            services.AddDbContext<ApplicationContext>(opts => opts.UseSqlServer(GetConnectionString("BaseProjectHosted")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc();
        }
        private string GetConnectionString(string nameOfConnectionString)
        {
            var username = Environment.GetEnvironmentVariable("SQL_USERNAME", EnvironmentVariableTarget.User);
            var password= Environment.GetEnvironmentVariable("SQL_PASSWORD", EnvironmentVariableTarget.User);
            return string.Format(Configuration.GetConnectionString(nameOfConnectionString), username, password);
        }
    }
}
