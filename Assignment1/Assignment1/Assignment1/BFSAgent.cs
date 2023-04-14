using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class BFSAgent
    {
        private Map currentMap;
        private LinkedList<TraversalNode> frontier;
        public int numberOfNodes;

        public BFSAgent(Map currentMap)
        {
            this.currentMap = currentMap;
            frontier = new LinkedList<TraversalNode>();
            numberOfNodes = 0;
        }

        public Map CurrentMap { get => currentMap; set => currentMap = value; }

        public TraversalNode? Search()
        {
            //initialize frontier
            frontier.AddFirst(currentMap.GetStartingNode());

            //search loop
            TraversalNode destinationNode = null;
            while(frontier.Count != 0)
            {
                destinationNode = frontier.First();
                numberOfNodes++;

                //a goal is reached?
                if (destinationNode.nodeState == (int)CellState.Goal)
                    break;

                destinationNode.nodeState = (int)CellState.Traversed;
                //remove current node
                frontier.RemoveFirst();

                //expand to adjacent nodes (children/leaf nodes)
                foreach (TraversalNode node in currentMap.ExpandNode(destinationNode))
                {
                    frontier.AddLast(node);
                }
            }

            return destinationNode;
        }
    }
}
