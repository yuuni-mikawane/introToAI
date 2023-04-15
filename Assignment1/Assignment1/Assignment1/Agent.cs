using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public abstract class Agent
    {
        public Map currentMap;
        public LinkedList<TraversalNode> frontier;
        public int numberOfNodes;
        public Vector2 startPosition;

        public Agent(Map currentMap)
        {
            this.currentMap = currentMap;
            frontier = new LinkedList<TraversalNode>();
            numberOfNodes = 0;
            startPosition = currentMap.startPosition;
        }

        public abstract TraversalNode? Search();
        
    }
}
