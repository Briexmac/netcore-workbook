using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;

namespace BaseProject.Cookies
{
    public class InspectCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly ILogger _logger;

        public InspectCookieAuthenticationEvents(ILogger<InspectCookieAuthenticationEvents> logger)
        {
            _logger = logger;
        }

        public override async Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
        {
            _logger.LogDebug($"Before {nameof(RedirectToAccessDenied)}");
            await base.RedirectToAccessDenied(context);
            _logger.LogDebug($"After {nameof(RedirectToAccessDenied)}");
        }

        public override async Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        {
            _logger.LogDebug($"Before {nameof(RedirectToLogin)}");
            await base.RedirectToLogin(context);
            _logger.LogDebug($"After {nameof(RedirectToLogin)}");
        }

        public override async Task RedirectToLogout(RedirectContext<CookieAuthenticationOptions> context)
        {
            _logger.LogDebug($"Before {nameof(RedirectToLogout)}");
            await base.RedirectToLogout(context);
            _logger.LogDebug($"After {nameof(RedirectToLogout)}");
        }

        public override async Task RedirectToReturnUrl(RedirectContext<CookieAuthenticationOptions> context)
        {
            _logger.LogDebug($"Before {nameof(RedirectToReturnUrl)}");
            await base.RedirectToReturnUrl(context);
            _logger.LogDebug($"After {nameof(RedirectToReturnUrl)}");
        }

        public override async Task SignedIn(CookieSignedInContext context)
        {
            _logger.LogDebug($"Before {nameof(SignedIn)}");
            await base.SignedIn(context);
            _logger.LogDebug($"After {nameof(SignedIn)}");
        }

        public override async Task SigningIn(CookieSigningInContext context)
        {
            _logger.LogDebug($"Before {nameof(SigningIn)}");
            await base.SigningIn(context);
            _logger.LogDebug($"After {nameof(SigningIn)}");
        }

        public override async Task SigningOut(CookieSigningOutContext context)
        {
            _logger.LogDebug($"Before {nameof(SigningOut)}");
            await base.SigningOut(context);
            _logger.LogDebug($"After {nameof(SigningOut)}");
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            _logger.LogDebug($"Before {nameof(ValidatePrincipal)}");
            await base.ValidatePrincipal(context);
            _logger.LogDebug($"After {nameof(ValidatePrincipal)}");
        }
    }
}
