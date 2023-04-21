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
        public float heuristicValue;
        public int cost;
        public int localCost;
        public bool isGoal;
        public int currenThreshold;

        public TraversalNode(Vector2 coordinate, int nodeState, TraversalNode? parent = null, string pathMsg = "", bool isGoal = false)
        {
            this.coordinate = coordinate;
            this.nodeState = nodeState;
            this.parent = parent;
            this.pathMsg = pathMsg;
            heuristicValue = 0;
            localCost = 1;
            if (parent != null)
                cost = localCost + parent.cost;
            else
                cost = 1;
            this.isGoal = isGoal;
            currenThreshold = 0;
        }
    }
}
