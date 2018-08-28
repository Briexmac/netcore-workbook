using Microsoft.AspNetCore.Identity;

namespace BaseProject.Areas.Admin.Models
{
    public class RoleViewModelBase
    {
        public IdentityRole Role { get; set; }

        public RoleViewModelBase()
        {
            // Required for Model Binding
        }

        public RoleViewModelBase(IdentityRole role)
        {
            Role = role;
        }
    }
}
