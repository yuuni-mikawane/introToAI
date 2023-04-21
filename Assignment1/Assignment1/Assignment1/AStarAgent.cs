using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class AStarAgent : Agent
    {
        public AStarAgent(Map currentMap) : base(currentMap)
        {
        }

        public override TraversalNode? Search(bool drawMap = false)
        {
            //set an array of specially allowed states that can be expanded to by this agent
            int[] specialStates = { (int)CellState.Explored, (int)CellState.Visited };

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

            //initialize frontier and the start node's cost and heuristic value
            frontier.AddFirst(currentMap.GetStartingNode());
            frontier.First().heuristicValue = Vector2.GetManhattanDistance(startPosition, frontier.First().coordinate);
            frontier.First().cost = 0;

            //initialize the closed list which keeps track of reachable nodes and their current optimal cost to reach
            LinkedList<TraversalNode> closedList = new LinkedList<TraversalNode>();

            //search loop
            TraversalNode? currentNode = frontier.First();
            while (frontier.Count != 0)
            {
                //get the node in the frontier with minimum cost and heuristic value ((min)f = g + h)
                float f = -1;
                foreach (TraversalNode node in frontier)
                {
                    if (f == -1 || f > node.cost + node.heuristicValue)
                    {
                        currentNode = node;
                        f = node.cost + node.heuristicValue;
                    }
                }
                numberOfNodes++;
                //the goal is reached?
                if (currentNode!.isGoal && targetGoal == currentNode.coordinate)
                    break;
                //mark as visiting on the map
                currentMap.VisitNode(currentNode);

                if (drawMap)
                {
                    currentMap.DrawMap();
                }

                //mark as visited/traversed on the map
                currentMap.LeaveNode(currentNode);
                //remove current node from frontier
                frontier.Remove(currentNode);
                //add current node to the closed list
                closedList.AddLast(currentNode);

                //expand to adjacent nodes (children/leaf nodes)
                foreach (TraversalNode node in currentMap.ExpandAllPossibleNodes(currentNode, specialStates))
                {
                    //boolean for marking whether the adjacent node has the best cost to be reached so far
                    bool isOptimal = true;

                    //calculate its heuristic value
                    node.heuristicValue = Vector2.GetManhattanDistance(node.coordinate, targetGoal);

                    //is the cell already kept track of in the frontier?
                    //If yes, does that cell currently hold a better cost to reach? If yes, skip this node. If not, proceed
                    foreach(TraversalNode frontierNode in frontier)
                    {
                        if (frontierNode.coordinate == node.coordinate &&
                            frontierNode.cost <= node.cost)
                        {
                            isOptimal = false;
                            break;
                        }
                    }

                    //if cell is optimal in open list AND
                    //is the cell already kept track off in the closed list?
                    //If yes, does that cell currently hold a better cost to reach? If yes, skip this node. If not, proceed
                    if (isOptimal)
                    {
                        foreach (TraversalNode closedListNode in closedList)
                        {
                            if (closedListNode.coordinate == node.coordinate &&
                                closedListNode.cost <= node.cost)
                            {
                                isOptimal = false;
                                break;
                            }
                        }

                        //if finally, the node is the optimal route to the cell so far, it is added to the frontier for further 
                        //exploration
                        if (isOptimal)
                            frontier.AddLast(node);
                    }
                }
            }

            return currentNode;
        }

    }
}
