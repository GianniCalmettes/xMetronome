using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Media;
using Java.Lang;
using xMetronomeClient.Services;
using xMetronomeClient.Models;
using Math = System.Math;

namespace AndroidClient.Droid.SoundService
{
    public class AudioGeneratedSoundService : IAudio
    {
        private readonly int _sampleRate = 44100;
        private readonly int _defaultClickSize = 10;

        private Settings _settings;

        private int _tocFrequency;
        private int _ticFrequency;

        private byte[] _tic;
        private byte[] _toc;

        private readonly AudioTrack _audioTrack;

        public AudioGeneratedSoundService(ISettingsService settingsService)
        {
            _settings = settingsService.LoadSettings();

            _tocFrequency = _settings.LowSoundFrequency;
            _ticFrequency = _settings.HighSoundFrequency;

            _audioTrack = new AudioTrack(
                Stream.Music,
                _sampleRate,
                ChannelOut.Stereo,
                Encoding.Pcm16bit,
                AudioTrack.GetMinBufferSize(_sampleRate, ChannelOut.Stereo, Encoding.Pcm16bit),
                AudioTrackMode.Stream);

            ReInit(_settings.DefaultBpm);
        }

        public void ReInit(int bpm)
        {
            _audioTrack.Stop();
            _audioTrack.Flush();

            _tic = Convert(Generate(_ticFrequency, _defaultClickSize, _sampleRate, bpm));
            _toc = Convert(Generate(_tocFrequency, _defaultClickSize, _sampleRate, bpm));

            _audioTrack.Play();
        }

        public async Task PlayLowAsync()
        {
            await _audioTrack.WriteAsync(_toc, 0, _toc.Length);
        }

        public async Task PlayHighAsync()
        {
            await _audioTrack.WriteAsync(_tic, 0, _tic.Length);
        }

        private static double[] Generate(double freq, int clickSize, int sampleRate, double bpm)
        {
            var periodSize = (int) ((60 * sampleRate) / bpm);
            var wStep = (2 * Math.PI * freq) / sampleRate; //Pulsation steps
            var sig = new double[periodSize];

            clickSize = (clickSize * sampleRate) / 1000;
            if (clickSize >= periodSize)
                clickSize = periodSize / 2;

            // tic
            for (var i = 0; i < clickSize; i++)
                sig[i] = Math.Sin(i * wStep);

            //Silence
            for (var i = clickSize; i < periodSize; i++)
                sig[i] = 0;

            return sig;
        }

        private static byte[] Convert(IReadOnlyCollection<double> sigle)
        {
            var sigbe = new byte[2 * sigle.Count];
            var i = 0;


            foreach (var sample in sigle)
            {
                var normalizedSample = (short) ((sample * Short.MaxValue));
                sigbe[i] = (byte) (normalizedSample &
                                   0x00ff
                    );
                i++;
                sigbe[i] = (byte) ((normalizedSample & 0xff00) >>
                                   8);
                i++;
            }

            return sigbe;
        }
    }
}