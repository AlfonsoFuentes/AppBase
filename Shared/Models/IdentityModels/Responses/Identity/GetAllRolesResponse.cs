using System.Collections.Generic;

namespace Shared.Models.IdentityModels.Responses.Identity
{
    public class GetAllRolesResponse
    {
        public IEnumerable<RoleResponse> Roles { get; set; }
    }
}