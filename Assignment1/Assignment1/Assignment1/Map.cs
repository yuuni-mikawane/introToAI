using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class Map
    {
        public int[,] cellsState;
        public Vector2 mapSize;
        public Vector2 startPosition;
        public List<Vector2> goalPositions;
        public List<Wall> walls;
        public bool allGoalsReached;
        public int nodeTraversed;
        public int nodeExpanded;
        public int pathLength;
        public string agentName;

        public Map(Vector2 mapSize, Vector2 startPosition, List<Vector2> goalPositions, List<Wall> walls, bool allGoalsReached = false)
        {
            cellsState = new int[mapSize.x, mapSize.y];
            this.mapSize = mapSize;
            this.startPosition = startPosition;
            this.goalPositions = goalPositions;
            this.walls = walls;
            this.allGoalsReached = allGoalsReached;
            nodeTraversed = 0;
            nodeExpanded = 0;
            pathLength = 0;
            agentName = "none";
            GenerateMap();
        }

        public void GenerateMap()
        {
            //set every node as traversable by default
            for (int i = 0; i < mapSize.x * mapSize.y; i++)
            {
                cellsState[i % mapSize.x, i / mapSize.x] = (int)CellState.Traversable;
            }

            //populate map with the start
            cellsState[startPosition.x, startPosition.y] = (int)CellState.Start;

            //populate map with walls
            foreach (Wall wall in walls)
            {

                for (int x = wall.TopLeftPivotPos.x; x < wall.TopLeftPivotPos.x + wall.Width; x++)
                {
                    for (int y = wall.TopLeftPivotPos.y; y < wall.TopLeftPivotPos.y + wall.Height; y++)
                    {
                        cellsState[x, y] = (int)CellState.Wall;
                    }
                }
            }

            //populate map with goals
            foreach (Vector2 goal in goalPositions)
            {
                cellsState[goal.x, goal.y] = (int)CellState.Goal;
            }
        }

        //get starting node
        public TraversalNode GetStartingNode()
        {
            return new TraversalNode(startPosition, (int)CellState.Start);
        }

        public TraversalNode GetFirstGoalNode()
        {
            return new TraversalNode(goalPositions[0], (int)CellState.Goal);
        }

        //return adjacent nodes on the grid (return the leaves of a node)
        public LinkedList<TraversalNode> ExpandAllPossibleNodes(TraversalNode currentNode, int[]? allowedStates = null)
        {
            TraversalNode? tempNode;

            LinkedList<TraversalNode> adjacentNodes = new LinkedList<TraversalNode>();
            
            //go up
            tempNode = ExpandNode(currentNode, Direction.Up, allowedStates);
            if (tempNode != null)
                adjacentNodes.AddLast(tempNode);
            //go left
            tempNode = ExpandNode(currentNode, Direction.Left, allowedStates);
            if (tempNode != null)
                adjacentNodes.AddLast(tempNode);
            //go down
            tempNode = ExpandNode(currentNode, Direction.Down, allowedStates);
            if (tempNode != null)
                adjacentNodes.AddLast(tempNode);
            //go right
            tempNode = ExpandNode(currentNode, Direction.Right, allowedStates);
            if (tempNode != null)
                adjacentNodes.AddLast(tempNode);

            return adjacentNodes;
        }

        public TraversalNode? ExpandNode(TraversalNode parentNode, Direction dir, int[]? allowedStates = null)
        {
            int xStep = 0;
            int yStep = 0;
            string msg;


            //find x or y movement based on direction
            switch (dir)
            {
                case Direction.Up:
                    yStep -= 1;
                    msg = "up; ";
                    break;
                case Direction.Down:
                    yStep += 1;
                    msg = "down; ";
                    break;
                case Direction.Left:
                    xStep -= 1;
                    msg = "left; ";
                    break;
                default:
                    xStep += 1;
                    msg = "right; ";
                    break;
            }

            //get the coordinate of the node that will be explored
            Vector2 target = new Vector2(parentNode.coordinate.x + xStep, parentNode.coordinate.y + yStep);

            if (mapSize.x > target.x &&
                target.x >= 0 &&
                mapSize.y > target.y &&
                target.y >= 0)
            {
                if (IsAllowedToBeExpandedTo(cellsState[target.x, target.y], allowedStates))
                {
                    bool nodeIsGoal = (cellsState[target.x, target.y] == (int)CellState.Goal);
                    Vector2 tempCoordinate = new Vector2(target.x, target.y);
                    TraversalNode tempNode = new TraversalNode(tempCoordinate, cellsState[target.x, target.y], parentNode, msg, nodeIsGoal);
                    MarkNodeAsExplored(tempNode);
                    nodeExpanded++;
                    return tempNode;
                }
            }

            return null;
        }

        public TraversalNode? ExpandOneNode(TraversalNode parentNode, int[]? allowedStates = null)
        {
            TraversalNode? result = null;
            for (int i = 0; i < 4; i++)
            {
                result = ExpandNode(parentNode, (Direction)i, allowedStates);
                if (result != null)
                {
                    break;
                }
            }
            return result;
        }

        public bool IsAllowedToBeExpandedTo(int nodeState, int[]? allowedStates = null)
        {
            bool isSpeciallyAllowed = false;
            if (allowedStates != null)
            {
                foreach (CellState cellState in allowedStates)
                {
                    if ((int)cellState == nodeState)
                    {
                        isSpeciallyAllowed = true;
                        break;
                    }
                }
            }

            if (nodeState == (int)CellState.Goal ||
                nodeState == (int)CellState.Traversable ||
                isSpeciallyAllowed)
            {
                return true;
            }
            else
                return false;
        }

        public void VisitNode(TraversalNode node)
        {
            nodeTraversed++;
            cellsState[node.coordinate.x, node.coordinate.y] = (int)CellState.Visiting;
        }

        public void MarkNodeAs(TraversalNode node, CellState state)
        {
            cellsState[node.coordinate.x, node.coordinate.y] = (int)state;
        }

        public void LeaveNode(TraversalNode node)
        {
            cellsState[node.coordinate.x, node.coordinate.y] = (int)CellState.Visited;
        }

        public void MarkNodeAsSolution(TraversalNode node)
        {
            cellsState[node.coordinate.x, node.coordinate.y] = (int)CellState.Solution;
        }

        public void MarkNodeAsExplored(TraversalNode node)
        {
            if (cellsState[node.coordinate.x, node.coordinate.y] != (int)CellState.Visited)
                cellsState[node.coordinate.x, node.coordinate.y] = (int)CellState.Explored;
        }

        public void DrawMap(int delay = 50)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            pathLength = 0;
            for (int i = 0; i < mapSize.y; i++)
            {
                for (int j = 0; j < mapSize.x; j++)
                {
                    if (cellsState[j, i] == (int)CellState.Wall)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("*");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (cellsState[j, i] == (int)CellState.Start)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("S");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (cellsState[j, i] == (int)CellState.Goal)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("O");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (cellsState[j, i] == (int)CellState.Start)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("S");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (cellsState[j, i] == (int)CellState.Visited)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("*");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (cellsState[j, i] == (int)CellState.Visiting)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("*");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (cellsState[j, i] == (int)CellState.Traversable)
                    {
                        Console.Write("-");
                    }
                    else if (cellsState[j, i] == (int)CellState.Solution)
                    {
                        pathLength++;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("o");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (cellsState[j, i] == (int)CellState.Explored)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("?");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("@");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                Console.Write("\n");

            }
            Console.Write("\nChosen agent: " + agentName);
            Console.Write("\nNode expanded: " + nodeExpanded);
            Console.Write("\nNode traversed: " + nodeTraversed);
            Console.Write("\nPath length: " + pathLength);
            Thread.Sleep(delay);
        }

    }
}
