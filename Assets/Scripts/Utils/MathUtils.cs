
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        public static bool IsRightDir(Vector2 v1, Vector2 v2)
        {
            float Z = v1.x * v2.y - v1.y * v2.x;
            return Z < 0;
        }
    }
}