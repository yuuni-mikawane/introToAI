using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class TraversalNode
    {
        public Vector2 coordinate;
        public int nodeState;
        public TraversalNode? parent;
        public string pathMsg;

        public TraversalNode(Vector2 coordinate, int nodeState, TraversalNode? parent = null, string pathMsg = "")
        {
            this.coordinate = coordinate;
            this.nodeState = nodeState;
            this.parent = parent;
            this.pathMsg = pathMsg;
        }
    }
}
