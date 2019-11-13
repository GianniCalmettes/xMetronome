using System;
using System.Collections.Generic;
using System.Text;

namespace xMetronomeClient.Models
{
    public class Settings
    {
        public int DefaultBpm { get; set; }

        /// <summary>
        /// Interval where the accent will appear ( 4/4, 5/4 ... )
        /// </summary>
        public int DefaultBeatPerBar { get; set; }

        /// <summary>
        /// Frequency of the higher sound (in Mhz)
        /// </summary>
        public int HighSoundFrequency { get; set; }

        /// <summary>
        /// Frequency of the lower sound (in Mhz)
        /// </summary>
        public int LowSoundFrequency { get; set; }
    }
}
