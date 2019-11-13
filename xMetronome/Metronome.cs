using System;
using System.Threading;
using System.Threading.Tasks;
using xMetronome.SoundsManagement;

namespace xMetronome
{
    public class Metronome
    {
        private const int MINUTE = 59950;

        private readonly ISoundManager _soundManager;
        private readonly BeatCounter _beatCounter;

        public Metronome(ISoundManager soundManager, BeatCounter beatCounter)
        {
            _soundManager = soundManager;
            _beatCounter = beatCounter;
        }

        public async Task PlayAsync(int bpm, int numberOfBeatPerBar, CancellationToken cancellationToken)
        {
            try
            {
                _beatCounter.Init(numberOfBeatPerBar);
                var interval = MINUTE / bpm;

                while (true)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (_beatCounter.IsHighlight())
                        await _soundManager.PlayHighAsync();
                    else
                        await _soundManager.PlayLowAsync();

                    _beatCounter.AddBeatCount(1);
                    await Task.Delay(interval);
                }
            }
            catch (OperationCanceledException operationCanceledOperation)
            {
                Console.WriteLine("Operation was canceled");
                Console.WriteLine(operationCanceledOperation);
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: ", e);
                throw;
            }
           
        }
    }
}