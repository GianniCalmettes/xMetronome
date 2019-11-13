using System.Threading.Tasks;

namespace xMetronome.SoundsManagement
{
    public interface ISoundManager
    {
        void Reset(int bpm);
        Task PlayHighAsync();
        Task PlayLowAsync();
    }
}