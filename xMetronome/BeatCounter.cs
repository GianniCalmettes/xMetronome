namespace xMetronome
{
    public class BeatCounter
    {
        private int _beatPerBar { get; set; }
        private int _beatCount { get; set; } = 1;

        public void Init(int beatPerBar)
        {
            _beatPerBar = beatPerBar;
        }

        public bool IsHighlight()
        {
            if (_beatCount > _beatPerBar)
            {
                _beatCount = 1;
                return true;
            }

            return false;
        }

        public void AddBeatCount(int numberOfBeatCount)
        {
            _beatCount += numberOfBeatCount;
        }
    }
}