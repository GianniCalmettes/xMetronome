using System.Threading.Tasks;

namespace AndroidClient.Droid.SoundService
{
    public interface IAudio
    {
        Task PlayLowAsync();
        Task PlayHighAsync();
        void ReInit(int bpm);
    }
}