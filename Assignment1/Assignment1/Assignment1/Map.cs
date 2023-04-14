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
        public bool allGoalsReached;

        public Map(int[,] cells, Vector2 startPosition, List<Vector2> goalPositions, bool allGoalsReached = false)
        {
            cellsState = cells;
            mapSize = new Vector2(cells.GetLength(0), cells.GetLength(1));
            this.startPosition = startPosition;
            this.goalPositions = goalPositions;
            this.allGoalsReached = allGoalsReached;
        }

        //get starting node
        public TraversalNode GetStartingNode()
        {
            return new TraversalNode(startPosition, (int)CellState.Start);
        }

        //return adjacent nodes on the grid (return the leaves of a node)
        public LinkedList<TraversalNode> ExpandNode(TraversalNode currentNode)
        {
            Vector2 tempCoordinate;
            TraversalNode tempNode;
            int x = currentNode.coordinate.x;
            int y = currentNode.coordinate.y;

            LinkedList<TraversalNode> adjacentNodes = new LinkedList<TraversalNode>();
            //go up
            if (y != 0)
            {
                if (cellsState[x, y - 1] != (int)CellState.Wall && cellsState[x, y - 1] != (int)CellState.Traversed)
                {
                    tempCoordinate = new Vector2(x, y - 1);
                    tempNode = new TraversalNode(tempCoordinate, cellsState[x, y - 1], currentNode, "up");
                    adjacentNodes.AddLast(tempNode);
                }
            }
            //go left
            if (x != 0)
            {
                if (cellsState[x - 1, y] != (int)CellState.Wall && cellsState[x - 1, y] != (int)CellState.Traversed)
                {
                    tempCoordinate = new Vector2(x - 1, y);
                    tempNode = new TraversalNode(tempCoordinate, cellsState[x - 1, y], currentNode, "left");
                    adjacentNodes.AddLast(tempNode);
                }
            }
            //go down
            if (y != mapSize.y - 1)
            {
                if (cellsState[x, y + 1] != (int)CellState.Wall && cellsState[x, y + 1] != (int)CellState.Traversed)
                {
                    tempCoordinate = new Vector2(x, y + 1);
                    tempNode = new TraversalNode(tempCoordinate, cellsState[x, y + 1], currentNode, "down");
                    adjacentNodes.AddLast(tempNode);
                }
            }
            //go right
            if (x != mapSize.x - 1)
            {
                if(cellsState[x + 1, y] != (int)CellState.Wall && cellsState[x + 1, y] != (int)CellState.Traversed)
                {
                    tempCoordinate = new Vector2(x + 1, y);
                    tempNode = new TraversalNode(tempCoordinate, cellsState[x + 1, y], currentNode, "right");
                    adjacentNodes.AddLast(tempNode);
                }
            }
            return adjacentNodes;
        }

        public TraversalNode? Move(Vector2 currentCoordinate, Direction dir)
        {
            Vector2 nodeCoordinate = new Vector2(currentCoordinate.x, currentCoordinate.y);
            switch (dir)
            {
                case Direction.Up:
                    if (currentCoordinate.y == 0)
                        return null;
                    else
                        nodeCoordinate.y--;
                    break;
                case Direction.Left:
                    if (currentCoordinate.x == 0)
                        return null;
                    else
                        nodeCoordinate.x--;
                    break;
                case Direction.Down:
                    if (currentCoordinate.y == mapSize.y - 1)
                        return null;
                    else
                        nodeCoordinate.y++;
                    break;
                case Direction.Right:
                    if (currentCoordinate.x == mapSize.x - 1)
                        return null;
                    else
                        nodeCoordinate.x++;
                    break;
            }

            return new TraversalNode(nodeCoordinate, cellsState[nodeCoordinate.x, nodeCoordinate.y]);
        }
        public void DrawMap(Map map)
        {
            for (int i = 0; i < map.mapSize.y; i++)
            {
                for (int j = 0; j < map.mapSize.x; j++)
                {
                    if (map.cellsState[j, i] == (int)CellState.Wall)
                        Console.Write("X");
                    else if (map.cellsState[j, i] == (int)CellState.Goal)
                        Console.Write("O");
                    else if (map.cellsState[j, i] == (int)CellState.Start)
                        Console.Write("S");
                    else
                        Console.Write("-");
                }
                Console.Write("\n");
            }
        }

    }
}
