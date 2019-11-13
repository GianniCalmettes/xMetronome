using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using xMetronomeClient.Models;

namespace xMetronomeClient.Repositories
{
    public class CrossPlateformSettingsRepository : ISettingsRepository
    {
        private static readonly string _settingsFileName = "settings.json";
        private readonly string _settingsFilePath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, _settingsFileName);

        public CrossPlateformSettingsRepository() { }

        public Task PersistSettingsAsync(Settings settingsToPersist)
        {
            using (StreamWriter file = File.CreateText(_settingsFilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, settingsToPersist);

                return Task.FromResult(true);
            }
        }

        public async Task<Settings> ReadSettingsAsync()
        {
            if(!File.Exists(_settingsFilePath))
            {
                // create it with default values
                await PersistSettingsAsync(new Settings { DefaultBeatPerBar = 4, DefaultBpm = 100, HighSoundFrequency = 880, LowSoundFrequency = 440 });
            }

            using (var reader = File.OpenText(_settingsFilePath))
            {
                var jsonSerializer = new JsonSerializer();
                return (Settings) jsonSerializer.Deserialize(reader, typeof(Settings));
            }
        }
    }
}
