using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class Vector2
    {
        float x;
        float y;

        public Vector2(int x, int y)
        {
            this.x = (float)x;
            this.y = (float)y;
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static float Distance(Vector2 v1, Vector2 v2)
        {
            return MathF.Sqrt(MathF.Pow(v2.x - v1.x, 2) + MathF.Pow(v2.y - v1.y, 2));
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", x, y);
        }
    }
}
