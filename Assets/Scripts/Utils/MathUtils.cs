
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ulf
{
    public static class MathUtils
    {
        public static float GetMinAngleDiff( float x, float y )
        {
            List<float> diffs = new() { 
                Math.Abs(x - y),
                Math.Abs(x - y - 360f),
                Math.Abs(x - y + 360f) 
            };

            return diffs.Min();
        }
    }
}