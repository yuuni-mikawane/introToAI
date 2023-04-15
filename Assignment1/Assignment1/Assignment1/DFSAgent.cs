using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class DFSAgent : Agent
    {
        public DFSAgent(Map currentMap) : base(currentMap)
        {
        }

        public override TraversalNode? Search()
        {
            //initialize frontier
            frontier.AddFirst(currentMap.GetStartingNode());

            //search loop
            TraversalNode currentNode = null;
            while (frontier.Count != 0)
            {
                currentNode = frontier.Last();
                numberOfNodes++;

                //a goal is reached?
                if (currentNode.nodeState == (int)CellState.Goal)
                    break;

                //mark as visited/traversed
                currentMap.VisitNode(currentNode);
                //remove current node
                frontier.RemoveLast();

                //expand to adjacent nodes (children/leaf nodes)
                foreach (TraversalNode node in currentMap.ExpandNode(currentNode).Reverse())
                {
                    frontier.AddLast(node);
                }
            }

            return currentNode;
        }
    }
}
