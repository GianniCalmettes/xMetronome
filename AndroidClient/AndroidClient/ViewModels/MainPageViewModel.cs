using AndroidClient.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using xMetronome;
using xMetronome.SoundsManagement;
using xMetronomeClient.Models;
using xMetronomeClient.Services;

namespace AndroidClient
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly static string _startText = "Start";
        private readonly static string _stopText = "Stop";

        private Metronome _metronome;
        private ISoundManager _soundManager;

        private Settings _settings;

        private CancellationTokenSource _canceller;
        private bool _isMetronomeStarted;

        private string _startButtonText;
        private int _currentBpm;
        private int _currentBeatPerBar;


        public int CurrentBpm
        {
            get => _currentBpm;
            set
            {
                _currentBpm = value;
                OnPropertyChanged(nameof(CurrentBpm));
            }
        }

        public int CurrentBeatPerBar
        {
            get => _currentBeatPerBar;
            set
            {
                _currentBeatPerBar = value;
                OnPropertyChanged(nameof(CurrentBeatPerBar));
            }
        }

        public string StartButtonText
        {
            get => _startButtonText ?? _startText;
            set
            {
                _startButtonText = value;
                OnPropertyChanged(nameof(StartButtonText));
            }
        }

        public ICommand StartStopAsyncCommand { get; private set; }

        public MainPageViewModel(ISoundManager soundManager, ISettingsService settingsService, Metronome metronome)
        {
            _soundManager = soundManager;

            _settings = settingsService.LoadSettings();

            _metronome = metronome;

            Init();
        }

        public async Task StartOrStopMetronome()
        {
            if (_isMetronomeStarted)
            {
                StopMetronome();
                return;
            }

            SwitchStartButtonText();
            
            _soundManager.Reset(_currentBpm);
            _canceller = GetCanceller();

            _isMetronomeStarted = true;
            await _metronome.PlayAsync(_currentBpm, _currentBeatPerBar, _canceller.Token);
        }

        private void Init()
        {
            SetDefaultParameters(_settings);
            StartStopAsyncCommand = new Command(async () => await StartOrStopMetronome().ConfigureAwait(false));
        }

        private CancellationTokenSource GetCanceller()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Token.Register(SwitchStartButtonText);

            return cancellationTokenSource;
        }

        private void SwitchStartButtonText()
        {
            StartButtonText = (StartButtonText == _stopText ? _startText : _stopText);
        }

        private void StopMetronome()
        {
            _canceller.Cancel();
            _canceller.Dispose();
            _isMetronomeStarted = false;
        }

        private void SetDefaultParameters(Settings defaultParams)
        {
            CurrentBpm = defaultParams.DefaultBpm;
            CurrentBeatPerBar = defaultParams.DefaultBeatPerBar;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (!_isMetronomeStarted || propertyName == nameof(StartButtonText)) return;

            StopMetronome();
        }
    }
}