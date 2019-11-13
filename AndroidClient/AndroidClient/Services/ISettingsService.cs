using System.Threading.Tasks;
using xMetronomeClient.Models;

namespace xMetronomeClient.Services
{
    public interface ISettingsService
    {
        Settings LoadSettings();
        Task<Settings> LoadSettingsAsync();
        Task SaveSettingsAsync(Settings settingsToSave);
    }
}
