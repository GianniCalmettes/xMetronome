using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xMetronomeClient.Models;

namespace xMetronomeClient.Repositories
{
    public interface ISettingsRepository
    {
        Task<Settings> ReadSettingsAsync();
        Task PersistSettingsAsync(Settings settingsToPersist);
    }
}
