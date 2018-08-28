using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace BaseProject.Cookies.Authorization
{
    public class InspectAuthorizationService : IAuthorizationService
    {
        private readonly DefaultAuthorizationService _defaultAuthorizationService;
        private readonly ILogger<InspectAuthorizationService> _logger;
        private readonly IAuthorizationHandlerContextFactory _contextFactory;

        public InspectAuthorizationService(DefaultAuthorizationService defaultAuthorizationService, IAuthorizationHandlerContextFactory contextFactory, ILogger<InspectAuthorizationService> logger)
        {
            _defaultAuthorizationService = defaultAuthorizationService;
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, IEnumerable<IAuthorizationRequirement> requirements)
        {
            _logger.LogDebug($"Before {nameof(AuthorizeAsync)} 1");
            var context = _contextFactory.CreateContext(requirements, user, resource);
            var result = await _defaultAuthorizationService.AuthorizeAsync(user, resource, requirements);
            _logger.LogDebug($"After {nameof(AuthorizeAsync)} 1");
            return result;
        }

        public async Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, string policyName)
        {
            _logger.LogDebug($"Before {nameof(AuthorizeAsync)} 2");
            var result = await _defaultAuthorizationService.AuthorizeAsync(user, resource, policyName);
            _logger.LogDebug($"After {nameof(AuthorizeAsync)} 2");
            return result;
        }
    }
}
