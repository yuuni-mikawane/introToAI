using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class Cus1Agent : Agent
    {
        public Cus1Agent(Map currentMap) : base(currentMap)
        {
        }

        public override TraversalNode? Search()
        {
            //find the closest goal to the start position and set as targeted goal
            Vector2 targetGoal = currentMap.goalPositions[0];
            foreach (Vector2 goal in currentMap.goalPositions)
            {
                if (Vector2.GetManhattanDistance(goal, startPosition) <
                    Vector2.GetManhattanDistance(targetGoal, startPosition))
                {
                    targetGoal = goal;
                }
            }

            //initialize frontier and the node's heuristic value
            frontier.AddFirst(currentMap.GetStartingNode());
            frontier.First().heuristicValue = Vector2.GetManhattanDistance(startPosition, frontier.First().coordinate);

            //search loop
            TraversalNode currentNode = null;
            while (frontier.Count != 0)
            {
                //get the node in the frontier with minimum heuristic value (min manhattan distance)
                int minHeuristicInFrontier = -1;
                foreach (TraversalNode node in frontier)
                {
                    if (minHeuristicInFrontier == -1)
                    {
                        currentNode = node;
                        minHeuristicInFrontier = node.heuristicValue;
                    }
                    else if (minHeuristicInFrontier > node.heuristicValue)
                    {
                        currentNode = node;
                        minHeuristicInFrontier = node.heuristicValue;
                    }
                }
                numberOfNodes++;

                //a goal is reached?
                if (currentNode.nodeState == (int)CellState.Goal)
                    break;

                //mark as visited/traversed on the map
                currentMap.VisitNode(currentNode);
                //remove current node
                frontier.Remove(currentNode);

                //expand to adjacent nodes (children/leaf nodes)
                foreach (TraversalNode node in currentMap.ExpandNode(currentNode))
                {
                    node.heuristicValue = Vector2.GetManhattanDistance(node.coordinate, targetGoal);
                    frontier.AddLast(node);
                }
            }

            return currentNode;
        }
    }
}
