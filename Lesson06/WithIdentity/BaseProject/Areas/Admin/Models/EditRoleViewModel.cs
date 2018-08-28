using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BaseProject.Areas.Admin.Models
{
    public class EditRoleViewModel : RoleViewModelBase
    {
        public EditRoleViewModel()
        {
            // Required for Model Binding
        }

        public EditRoleViewModel(IdentityRole role) : base(role)
        {
        }

        public List<string> AssignedUsers { get; set; }

        public MultiSelectList AvailableUsers { get; set; }
    }
}
