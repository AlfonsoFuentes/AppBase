using Shared.Interfaces.Chat;

namespace Shared.Models.ChatHistories
{
    public partial class ChatHistory<TUser> : IChatHistory<TUser> where TUser : IChatUser
    {
        public long Id { get; set; }
        public string FromUserId { get; set; } = string.Empty;
        public string ToUserId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public virtual TUser FromUser { get; set; } = default!;
        public virtual TUser ToUser { get; set; } = default!;
    }
}