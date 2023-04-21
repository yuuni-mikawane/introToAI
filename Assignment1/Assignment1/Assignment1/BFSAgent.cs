using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class BFSAgent : Agent
    {
        public BFSAgent(Map currentMap) : base(currentMap)
        {
        }

        public override TraversalNode? Search(bool drawMap)
        {
            //initialize frontier
            frontier.AddFirst(currentMap.GetStartingNode());

            //search loop
            TraversalNode? currentNode = null;
            while(frontier.Count != 0)
            {
                currentNode = frontier.First();
                numberOfNodes++;
                //a goal is reached?
                if (currentNode.isGoal)
                    break;

                //mark as visiting
                currentMap.VisitNode(currentNode);

                if (drawMap)
                {
                    currentMap.DrawMap();
                }


                //mark as visited
                currentMap.LeaveNode(currentNode);

                //remove current node
                frontier.RemoveFirst();

                //expand to adjacent nodes (children/leaf nodes)
                foreach (TraversalNode node in currentMap.ExpandAllPossibleNodes(currentNode))
                {
                    frontier.AddLast(node);
                }
            }

            return currentNode;
        }
    }
}
