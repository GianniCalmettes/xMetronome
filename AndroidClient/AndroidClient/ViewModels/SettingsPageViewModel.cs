using xMetronomeClient.Services;
using xMetronomeClient.Models;
using Xamarin.Forms;
using System.ComponentModel;
using AndroidClient.Annotations;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace xMetronomeClient.ViewModels
{
    public class SettingsPageViewModel: INotifyPropertyChanged
    {
        private ISettingsService _settingsService;

        private Settings _settings;
        public Settings CurrentSettings {
            get => _settings;
            set
            {
                _settings = value;
                OnPropertyChanged(nameof(CurrentSettings));
            }
        }

        public int Bpm
        {
            get => CurrentSettings.DefaultBpm;
            set
            {
                CurrentSettings.DefaultBpm = value;
                OnPropertyChanged(nameof(Bpm));
            }
        }

        public int BeatPerBar
        {
            get => CurrentSettings.DefaultBeatPerBar;
            set
            {
                CurrentSettings.DefaultBeatPerBar = value;
                OnPropertyChanged(nameof(BeatPerBar));
            }
        }

        public int HighSoundFreq
        {
            get => _settings.HighSoundFrequency;
            set
            {
                _settings.HighSoundFrequency = value;
                OnPropertyChanged(nameof(HighSoundFreq));
            }
        }

        public int LowSoundFreq
        {
            get => _settings.LowSoundFrequency;
            set
            {
                _settings.LowSoundFrequency = value;
                OnPropertyChanged(nameof(LowSoundFreq));
            }
        }

        public ICommand SaveSettingsCommand { get; private set; }

        public SettingsPageViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
            CurrentSettings = _settingsService.LoadSettings();

            SaveSettingsCommand = new Command(async () => await _settingsService.SaveSettingsAsync(CurrentSettings));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
