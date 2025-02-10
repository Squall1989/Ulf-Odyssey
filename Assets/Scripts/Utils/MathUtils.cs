
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ulf
{

    public static class MathUtils
    {
        public static float GetMinAngleDiff( float x, float y )
        {
            var diffs = new List<float>() {
                Math.Abs(x - y),
                Math.Abs(x - y - 360f),
                Math.Abs(x - y + 360f)
            };

            float min = diffs.Min();

            return min;
        }
    }
}