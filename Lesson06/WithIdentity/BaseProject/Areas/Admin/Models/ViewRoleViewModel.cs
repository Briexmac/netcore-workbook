using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BaseProject.Areas.Admin.Models
{
    public class ViewRoleViewModel : RoleViewModelBase
    {
        public ViewRoleViewModel()
        {
            // Required for Model Binding
        }

        public ViewRoleViewModel(IdentityRole role) : base(role)
        {
        }

        public List<Identity.Data.User> AssignedUsers { get; set; }
    }
}
