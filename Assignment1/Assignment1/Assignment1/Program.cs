using Assignment1;


class MainClass
{
    static void Main(string[] args)
    {
        //reading content from text file
        string path = "RobotNav-test.txt";
        if (args.Length == 2)
            path = args[0];
        StreamReader reader = new StreamReader(path);
        string content = reader.ReadToEnd();
        reader.Close();

        Map map = GenerateMap(content);

        //result variables
        TraversalNode? result = null;
        int numberOfNodes = 0;

        //search option for BFS
        if (args[2] == "BFS")
        {
            BFSAgent bfs = new BFSAgent(map);
            result = bfs.Search();
            numberOfNodes = bfs.numberOfNodes;
        }

        //result printing
        Console.WriteLine(path + " " + args[2] + " NodeCount:" + numberOfNodes);
        if (result != null)
        {
            PrintPath(result);
        }
        else
        {
            Console.WriteLine("No path found");
        }
    }

    static void PrintPath(TraversalNode node)
    {
        //path stack
        Stack<String> stack = new Stack<String>();

        while (node != null)
        {
            stack.Push(node.pathMsg);
            node = node.parent;
        }

        while (stack.Count > 0)
        {
            Console.Write(stack.Pop() + "; ");
        }
    }

    static Map GenerateMap(string content)
    {
        //process text file - initial line split
        string[] lines = content.Split(
            new string[] { Environment.NewLine },
            //this split options eliminates the last next line at the end of the text file
            StringSplitOptions.RemoveEmptyEntries
        );

        string[] mapSizeString = lines[0].Trim(new char[] { '[', ']', ' ' }).Split(',');
        string[] startPosString = lines[1].Trim(new char[] { '(', ')', ' ' }).Split(',');
        string[] goalStatesString = lines[2].Split('|');

        //processing map size
        Vector2 mapSize = new Vector2(Int32.Parse(mapSizeString[1]), Int32.Parse(mapSizeString[0]));

        //processing start pos
        Vector2 startPos = new Vector2(Int32.Parse(startPosString[0]), Int32.Parse(startPosString[1]));

        //processing goal positions
        List<Vector2> goalPosArray = new List<Vector2>();

        foreach (string goalState in goalStatesString)
        {
            string[] pos = goalState.Trim(new char[] { '(', ')', ' ' }).Split(',');
            int x = Int32.Parse(pos[0]);
            int y = Int32.Parse(pos[1]);
            goalPosArray.Add(new Vector2(x, y));
        }

        //processing wall positions
        List<Wall> wallsPos = new List<Wall>();

        for (int i = 3; i < lines.Length; i++)
        {
            string[] wallsString = lines[i].Trim(new char[] { '(', ')', ' ' }).Split(',');
            //creating a wall and adding it to list of walls
            wallsPos.Add(new Wall(new Vector2(Int32.Parse(wallsString[0]), Int32.Parse(wallsString[1])),
                Int32.Parse(wallsString[2]), Int32.Parse(wallsString[3])));
        }

        //create the matrix of node states, every node is traversable by default
        int[,] nodes = new int[mapSize.x, mapSize.y];
        for (int i = 0; i < mapSize.x * mapSize.y; i++)
        {
            nodes[i % mapSize.x, i / mapSize.x] = (int)CellState.Traversable;
        }

        //populate map with walls
        foreach (Wall wall in wallsPos)
        {

            for (int x = wall.TopLeftPivotPos.x; x < wall.TopLeftPivotPos.x + wall.Width; x++)
            {
                for (int y = wall.TopLeftPivotPos.y; y < wall.TopLeftPivotPos.y + wall.Height; y++)
                {
                    nodes[x, y] = (int)CellState.Wall;
                }
            }
        }

        //populate map with goals
        foreach (Vector2 goal in goalPosArray)
        {
            nodes[goal.x, goal.y] = (int)CellState.Goal;
        }

        //create the map object
        return new Map(nodes, startPos, goalPosArray);

        //double checking processed data
        //Console.WriteLine("Map size: " + mapSize);
        //Console.WriteLine("Start pos: " + startPos);

        //Console.WriteLine("List of walls:");
        //foreach (Wall wallpos in wallsPos)
        //{
        //    Console.WriteLine("Pos: " + wallpos.TopLeftPivotPos + " Width: " + wallpos.Width + " Height: " + wallpos.Height);
        //}
    }

    
}