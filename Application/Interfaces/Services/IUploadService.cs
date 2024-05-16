using Shared.Models.IdentityModels.Requests;

namespace Application.Interfaces.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}