using System.Threading.Tasks;
using xMetronomeClient.Models;
using xMetronomeClient.Repositories;
using xMetronomeClient.Services;

[assembly: Xamarin.Forms.Dependency(typeof(ISettingsService))]
namespace xMetronomeClient.Services
{
    public class DefaultSettingsService : ISettingsService
    {
        private ISettingsRepository _settingsRepository;

        private static Settings _settings;

        public DefaultSettingsService(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public Settings LoadSettings()
        {
            if (_settings == null)
            {
                _settings = _settingsRepository.ReadSettingsAsync().Result;
                return _settings;
            }

            return _settings;
        }

        public async Task<Settings> LoadSettingsAsync()
        {
            if (_settings == null)
            {
                return await _settingsRepository.ReadSettingsAsync();
            }           

            return _settings;
        }

        public async Task SaveSettingsAsync(Settings settings)
        {
            _settings = settings;
            await _settingsRepository.PersistSettingsAsync(_settings);
        }
    }
}
