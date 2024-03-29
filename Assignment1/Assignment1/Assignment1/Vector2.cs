﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class Vector2
    {
        public int x;
        public int y;

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        
        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return a.x != b.x || a.y != b.y;
        }

        public static float Distance(Vector2 v1, Vector2 v2)
        {
            return MathF.Sqrt(MathF.Pow(v2.x - v1.x, 2) + MathF.Pow(v2.y - v1.y, 2));
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", x, y);
        }
        public static int GetManhattanDistance(Vector2 a, Vector2 b)
        {
            return Math.Abs(a.x - b.x) +
                Math.Abs(a.y - b.y);
        }

        public static float GetEuclideanDistance(Vector2 a, Vector2 b)
        {
            return MathF.Sqrt((MathF.Pow(a.x - b.x, 2) + MathF.Pow(a.y - b.y, 2)));
        }

        public override bool Equals(object? obj)
        {
            return obj is Vector2 vector &&
                   x == vector.x &&
                   y == vector.y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }
    }
}
