

namespace Domain.IdentityModels.Identity
{
    public class BlazorHeroRoleClaim : IdentityRoleClaim<string>, IAuditableEntity<int>
    {
        public string? Description { get; set; } = string.Empty;
        public string? Group { get; set; } = string.Empty;
        public string? CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public string? LastModifiedBy { get; set; } = string.Empty;
        public DateTime? LastModifiedOn { get; set; }
        public virtual BlazorHeroRole Role { get; set; } = null!;

        public BlazorHeroRoleClaim() : base()
        {
        }

        public BlazorHeroRoleClaim(string roleClaimDescription = null!, string roleClaimGroup = null!) : base()
        {
            Description = roleClaimDescription;
            Group = roleClaimGroup;
        }

    }
}