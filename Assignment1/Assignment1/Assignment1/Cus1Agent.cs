using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    //Dijkstra
    public class Cus1Agent : Agent
    {
        public Cus1Agent(Map currentMap) : base(currentMap)
        {
        }

        public override TraversalNode? Search(bool drawMap)
        {
            //initialize frontier
            frontier.AddFirst(currentMap.GetStartingNode());
            frontier.First().cost = 0;

            //search loop
            TraversalNode? currentNode = null;
            while (frontier.Count != 0)
            {
                int minCost = int.MaxValue;
                foreach (TraversalNode node in frontier)
                {
                    if (minCost > node.cost)
                    {
                        currentNode = node;
                        minCost = node.cost;
                    }
                }
                numberOfNodes++;
                //a goal is reached?
                if (currentNode!.isGoal)
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
                    //calculate new cost
                    if (node.parent != null)
                    {
                        node.cost += node.parent.cost;
                    }
                    frontier.AddLast(node);
                }
            }

            return currentNode;
        }

    }
}
