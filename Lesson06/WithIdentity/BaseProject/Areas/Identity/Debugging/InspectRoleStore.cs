using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BaseProject.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Areas.Identity.Services
{
    public class InspectRoleStore : RoleStore<Role>
    {
        public InspectRoleStore(DbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }

        public override Task AddClaimAsync(Role role, Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.AddClaimAsync(role, claim, cancellationToken);
        }

        public override string ConvertIdFromString(string id)
        {
            return base.ConvertIdFromString(id);
        }

        public override string ConvertIdToString(string id)
        {
            return base.ConvertIdToString(id);
        }

        public override Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.CreateAsync(role, cancellationToken);
        }

        public override Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.DeleteAsync(role, cancellationToken);
        }

        public override Task<Role> FindByIdAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.FindByIdAsync(id, cancellationToken);
        }

        public override Task<Role> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.FindByNameAsync(normalizedName, cancellationToken);
        }

        public override Task<IList<Claim>> GetClaimsAsync(Role role, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.GetClaimsAsync(role, cancellationToken);
        }

        public override Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.GetNormalizedRoleNameAsync(role, cancellationToken);
        }

        public override Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.GetRoleIdAsync(role, cancellationToken);
        }

        public override Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.GetRoleNameAsync(role, cancellationToken);
        }

        public override Task RemoveClaimAsync(Role role, Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.RemoveClaimAsync(role, claim, cancellationToken);
        }

        public override Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SetNormalizedRoleNameAsync(role, normalizedName, cancellationToken);
        }

        public override Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SetRoleNameAsync(role, roleName, cancellationToken);
        }

        public override Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.UpdateAsync(role, cancellationToken);
        }

        protected override IdentityRoleClaim<string> CreateRoleClaim(Role role, Claim claim)
        {
            return base.CreateRoleClaim(role, claim);
        }
    }
}
