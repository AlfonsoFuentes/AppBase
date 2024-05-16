using Shared.Models.IdentityModels.Requests.Mail;

namespace Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}