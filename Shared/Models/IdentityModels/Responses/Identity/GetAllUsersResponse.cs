using System.Collections.Generic;

namespace Shared.Models.IdentityModels.Responses.Identity
{
    public class GetAllUsersResponse
    {
        public IEnumerable<UserResponse> Users { get; set; }
    }
}