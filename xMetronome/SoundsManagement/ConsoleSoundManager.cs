using System;
using System.Threading.Tasks;

namespace xMetronome.SoundsManagement
{
    public class ConsoleSoundManager : ISoundManager
    {
        public void Reset(int bpm)
        {
            Console.WriteLine("reset");
        }

        public async Task PlayHighAsync()
        {
            Console.Beep(3000, 3);
        }

        public async Task PlayLowAsync()
        {
            Console.Beep(1000, 3);
        }
    }
}