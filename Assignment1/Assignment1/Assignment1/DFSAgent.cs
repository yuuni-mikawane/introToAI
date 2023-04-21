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

        public override TraversalNode? Search(bool drawMap = false)
        {
            //set an array of specially allowed states that can be expanded to by this agent
            int[] specialStates = { (int)CellState.Explored };

            //initialize frontier
            frontier.AddFirst(currentMap.GetStartingNode());

            //search loop
            TraversalNode? currentNode = null;
            while (frontier.Count != 0)
            {
                currentNode = frontier.Last();
                
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
                frontier.RemoveLast();

                //expand to adjacent nodes (children/leaf nodes)
                foreach (TraversalNode node in currentMap.ExpandAllPossibleNodes(currentNode, specialStates).Reverse())
                {
                    frontier.AddLast(node);
                }
            }

            return currentNode;
        }
    }
}
