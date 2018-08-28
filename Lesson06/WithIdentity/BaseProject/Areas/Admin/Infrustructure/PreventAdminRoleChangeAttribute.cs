using System.Linq;
using System.Threading.Tasks;
using BaseProject.Areas.Admin.Models;
using BaseProject.Areas.Identity;
using BaseProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Areas.Admin.Infrustructure
{
    public class PreventAdminRoleChangeAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var identityContext = context.HttpContext.RequestServices.GetService(typeof(IdentityContext)) as IdentityContext;
            var models = context.ActionArguments.Values.OfType<EditRoleViewModel>().Where(x => x.Role != null);
            foreach (var model in models)
            {
                var existing = await identityContext.Roles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Role.Id);
                if (existing.IsAdmin() && !model.Role.IsAdmin())
                {
                    var controller = context.Controller as ControllerBase;
                    controller.ModelState.AddModelError("", "Cannot modify Admin role name");
                    break;
                }
            }
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
