using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class Wall
    {
        Vector2 topLeftPivotPos;
        float width;
        float height;

        public Wall(Vector2 topLeftPivotPos, float width, float height)
        {
            this.topLeftPivotPos = topLeftPivotPos;
            this.width = width;
            this.height = height;
            this.topLeftPivotPos = topLeftPivotPos;
        }

        public Vector2 TopLeftPivotPos { get => topLeftPivotPos; set => topLeftPivotPos = value; }
        public float Width { get => width; set => width = value; }
        public float Height { get => height; set => height = value; }
    }
}
