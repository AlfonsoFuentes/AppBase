using Shared.Interfaces.Chat;
using Shared.Models.ChatHistories;

namespace Application.Interfaces.Services
{
    public interface IChatService
    {
        Task<IResult<IEnumerable<ChatUserResponse>>> GetChatUsersAsync(string userId);

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> message);

        Task<IResult<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string userId, string contactId);
    }
}