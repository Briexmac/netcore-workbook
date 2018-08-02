using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Controllers
{
    [ViewComponent(Name = "IssueTaskShort")]
    public class IssueTaskShortViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(BaseProject.Data.Models.Issue issue)
        {
            return Task.FromResult<IViewComponentResult>(View(issue));
        }
    }
}
