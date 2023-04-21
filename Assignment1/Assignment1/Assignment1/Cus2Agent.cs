namespace Assignment1
{
    public struct IDAStarResult
    {
        public int minF;
        public TraversalNode? node;

        public IDAStarResult()
        {
            minF = int.MaxValue;
            node = null;
        }
    }

    //IDA*
    public class Cus2Agent : Agent
    {
        public Cus2Agent(Map currentMap) : base(currentMap)
        {
        }

        public override TraversalNode? Search(bool drawMap)
        {
            //an array of specially allowed states that can be expanded to by this agent
            int[] specialStates = { (int)CellState.Explored };

            //find the closest goal to the start position and set as targeted goal
            Vector2 targetNode = currentMap.goalPositions[0];
            foreach (Vector2 goal in currentMap.goalPositions)
            {
                if (Vector2.GetManhattanDistance(goal, startPosition) <
                    Vector2.GetManhattanDistance(targetNode, startPosition))
                {
                    targetNode = goal;
                }
            }

            //set up start node
            TraversalNode startNode = currentMap.GetStartingNode();
            startNode.heuristicValue = Vector2.GetManhattanDistance(targetNode, startNode.coordinate);
            startNode.cost = 0;
            //calculating initial threshold
            int threshold = (int)startNode.heuristicValue;

            //variable storing the final result
            IDAStarResult result = new IDAStarResult();

            int hardLimitDepth = 30;
            while(hardLimitDepth > 0)
            {
                hardLimitDepth--;

                //start the recursive search
                result = SearchStep(startNode, threshold, targetNode, specialStates);
                //if goal found
                if (result.node != null && result.node.coordinate == targetNode)
                {
                    return result.node;
                }
                if (result.minF == int.MaxValue)
                {
                    return null;
                }

                threshold = result.minF;
                //refreshing cell states
                currentMap.GenerateMap();
            }

            return null;
        }

        public IDAStarResult SearchStep(TraversalNode node, int threshold, Vector2 targetGoal, int[] specialStates)
        {
            //result variable to be returned
            IDAStarResult result = new IDAStarResult();

            //mark node as visiting
            currentMap.VisitNode(node);

            //calculate f value of the current node that agent is processing
            int f = node.cost + (int)node.heuristicValue;
            //if the f value of current node is more than the current threshold, return that value
            //if not, continue with the expansion
            if (f > threshold)
            {
                result.minF = f;
                return result;
            }
            //if goal is current node, return solution
            if (node.coordinate == targetGoal)
            {
                result.node = node;
                return result;
            }

            //value for finding minimum of all ‘f value’ greater than threshold encountered
            int min = int.MaxValue;
            foreach(TraversalNode childNode in currentMap.ExpandAllPossibleNodes(node, specialStates))
            {
                currentMap.LeaveNode(node);
                currentMap.VisitNode(childNode);
                currentMap.DrawMap(3);
                currentMap.MarkNodeAsExplored(childNode);

                //calculate its heuristic value
                childNode.heuristicValue = Vector2.GetManhattanDistance(childNode.coordinate, targetGoal);

                //recursive call for depth search
                IDAStarResult temp = SearchStep(childNode, threshold, targetGoal, specialStates);
                
                //if goal is found
                if (temp.node != null && temp.node.coordinate == targetGoal)
                    return temp;

                //goal is not found, continue with finding minimum f value
                //int tempF = temp.node.cost + (int)temp.node.heuristicValue;
                if (temp.minF < min)
                    min = temp.minF;

                currentMap.LeaveNode(childNode);
            }
            //update the result's minF found
            result.minF = min;

            return result;
        }
    }
}
