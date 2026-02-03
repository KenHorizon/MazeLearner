using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner
{
    public struct IntRange
    {
        [JsonProperty("Min")]
        public readonly int Minimum;
        [JsonProperty("Max")]
        public readonly int Maximum;

        public IntRange(int minimum, int maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public static IntRange operator *(IntRange range, float scale)
        {
            return new IntRange((int)((float)range.Minimum * scale), (int)((float)range.Maximum * scale));
        }

        public static IntRange operator *(float scale, IntRange range)
        {
            return range * scale;
        }

        public static IntRange operator /(IntRange range, float scale)
        {
            return new IntRange((int)((float)range.Minimum / scale), (int)((float)range.Maximum / scale));
        }

        public static IntRange operator /(float scale, IntRange range)
        {
            return range / scale;
        }
    }
}
