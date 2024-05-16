using Shared.Interfaces.Managers;

namespace Client.Infrastructure.Managers.Preferences
{
    public interface IClientPreferenceManager : IPreferenceManager
    {
     

        Task<bool> ToggleDarkModeAsync();
    }
}