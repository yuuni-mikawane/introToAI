using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class Map
    {
        private CellState[,] cells;
        private Vector2 mapSize;
        private Vector2 startPosition;
        private Vector2 currentPosition;
        private Vector2[] goalPositions;
        private bool arriveAtAllGoals;

        public Map(CellState[,] cells, Vector2 startPosition, Vector2[] goalPositions, bool arriveAtAllGoals = false)
        {
            this.cells = cells;
            this.mapSize = new Vector2(cells.GetLength(0), cells.GetLength(1));
            this.startPosition = startPosition;
            this.currentPosition = startPosition;
            this.goalPositions = goalPositions;
            this.arriveAtAllGoals = arriveAtAllGoals;
        }

        public Vector2 CurrentPosition { get => currentPosition; set => currentPosition = value; }
        public Vector2 StartPosition { get => startPosition; set => startPosition = value; }
        public Vector2[] GoalPositions { get => goalPositions; set => goalPositions = value; }
        public bool ArriveAtAllGoals { get => arriveAtAllGoals; set => arriveAtAllGoals = value; }

        //TODO: this
        public void Traverse(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    if (currentPosition.Y == 0)
                        return;
                    else
                        currentPosition.Y++;
                    break;
                case Direction.Left:

                    break;
                case Direction.Down:
                    break;
                case Direction.Right:
                    break;
            }
        }
    }
}
