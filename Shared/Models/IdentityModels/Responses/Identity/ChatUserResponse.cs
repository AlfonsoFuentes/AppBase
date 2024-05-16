using Shared.Interfaces.Chat;
using Shared.Models.ChatHistories;

namespace Shared.Models.IdentityModels.Responses.Identity
{
    public class ChatUserResponse
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string? ProfilePictureDataUrl { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public bool IsOnline { get; set; }
        public virtual ICollection<ChatHistory<IChatUser>> ChatHistoryFromUsers { get; set; } = null!;
        public virtual ICollection<ChatHistory<IChatUser>> ChatHistoryToUsers { get; set; } = null!;
    }
}