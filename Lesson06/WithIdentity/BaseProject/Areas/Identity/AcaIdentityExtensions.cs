using System;
using System.Linq;
using BaseProject.Areas.Identity.Data;
using BaseProject.Areas.Identity.Debugging;
using BaseProject.Areas.Identity.Services;
using BaseProject.Cookies;
using BaseProject.Cookies.Authorization;
using BaseProject.Migrations.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BaseProject.Areas.Identity
{
    public static class AcaIdentityExtensions
    {
        private static bool CannotSeemToFigureOutWhatIsGoingOnMode = true;

        public static IApplicationBuilder UseAcaAuthentication(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                CheckConsentNeeded = (ctx) => ctx.Request.Cookies.All(c => c.Key != "ConsentCookie"),
                ConsentCookie = new CookieBuilder()
                {
                    Name = "ConsentCookie"
                },
                Secure = CookieSecurePolicy.SameAsRequest
            });
            return app;
        }


        public static IServiceCollection AddAcaAuthentication(this IServiceCollection services)
        {
            var authBuilder = services.AddAuthentication();

            if (!CannotSeemToFigureOutWhatIsGoingOnMode)
            {
                authBuilder.AddCookie();
            }
            else
            {
                authBuilder.AddCookie(opts =>
                {
                    opts.EventsType = typeof(InspectCookieAuthenticationEvents);
                });
                services.AddScoped<InspectCookieAuthenticationEvents>();
                services.AddScoped<SignInManager<User>, InspectSignInManager>();
                services.AddScoped<UserManager<User>, InspectUserManager>();
                services.AddScoped<IUserStore<User>, InspectUserStore>();
                services.AddScoped<IRoleStore<Role>, InspectRoleStore>();
                services.AddScoped<IPasswordHasher<User>, PlainTextPasswordHasher>();
                services.AddScoped<IAuthorizationService, InspectAuthorizationService>();
                services.AddScoped<DefaultAuthorizationService>();
                services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomUserClaimsPrincipalFactory>();
            }

            return services;
        }

        public static bool IsAdmin(this IdentityRole role)
        {
            if (role.NormalizedName == null)
                return false;
            return role.NormalizedName.Equals(ScriptDefaultRoles.NormalizedAdminName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
