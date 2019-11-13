using System;
using System.Threading.Tasks;
using AndroidClient.Droid.SoundService;
using Xamarin.Forms;
using xMetronome.SoundsManagement;

namespace AndroidClient.Droid
{
    public class AndroidSoundManager : ISoundManager
    {
        private IAudio _audioService;


        public AndroidSoundManager(IAudio audioService)
        {
            _audioService = audioService;
        }

        public void Reset(int bpm)
        {
            _audioService.ReInit(bpm);
        }

        public async Task PlayHighAsync()
        {
            await _audioService.PlayHighAsync();
        }

        public async Task PlayLowAsync()
        {
           await _audioService.PlayLowAsync();
        }
    }
}