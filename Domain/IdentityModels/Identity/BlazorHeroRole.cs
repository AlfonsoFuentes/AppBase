

namespace Domain.IdentityModels.Identity
{
    public class BlazorHeroRole : IdentityRole, IAuditableEntity<string>
    {
        public string? Description { get; set; } = string.Empty;
        public string? CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public string? LastModifiedBy { get; set; } = string.Empty;
        public DateTime? LastModifiedOn { get; set; }
        public virtual ICollection<BlazorHeroRoleClaim> RoleClaims { get; set; }

        public BlazorHeroRole() : base()
        {
            RoleClaims = new HashSet<BlazorHeroRoleClaim>();
        }

        public BlazorHeroRole(string roleName, string roleDescription = null) : base(roleName)
        {
            RoleClaims = new HashSet<BlazorHeroRoleClaim>();
            Description = roleDescription;
        }



    }
}