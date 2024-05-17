using MudBlazor;
using Shared.Interfaces.Managers;

namespace Client.Infrastructure.Managers.Preferences
{
    public interface IClientPreferenceManager : IPreferenceManager
    {

        Task<MudTheme> GetCurrentThemeAsync();
        Task<bool> ToggleDarkModeAsync();
    }
}