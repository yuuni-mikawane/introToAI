using Assignment1;


class MainClass
{
    static void Main(string[] args)
    {
        //reading content from text file
        string path = args[0];
        StreamReader reader = new StreamReader(path);
        string content = reader.ReadToEnd();
        reader.Close();

        Map map = GenerateMapWithData(content);
        bool quit = false;

        //result variables
        TraversalNode? result = null;
        int numberOfNodes = 0;
        Agent? agent = new BFSAgent(map);
        bool GUIMode = false;

        //search option for BFS
        if (args[2] == "BFS")
        {
            agent = new BFSAgent(map);
        }
        //DFS
        else if (args[2] == "DFS")
        {
            agent = new DFSAgent(map);
        }
        //GBFS
        else if (args[2] == "GBFS")
        {
            agent = new GBFSAgent(map);
        }
        //AStar
        else if (args[2] == "AS")
        {
            agent = new AStarAgent(map);
        }
        else if (args[2] == "CUS1")
        {
            agent = new Cus1Agent(map);
        }
        else if (args[2] == "CUS2")
        {
            agent = new Cus2Agent(map);
        }
        //GUI mode condition
        else
        {
            GUIMode = true;
        }

        //basic searches with no GUI
        if (!GUIMode)
        {
            //perform the search
            result = agent.Search();
            numberOfNodes = agent.numberOfNodes;

            //result printing
            Console.WriteLine(path + " " + args[2] + " NodeCount:" + numberOfNodes);
            if (result != null && result.isGoal)
            {
                PrintPathText(result);
            }
            else
            {
                Console.WriteLine("No path found!");
            }
        }
        //GUI rendering
        else
        {
            map.DrawMap();
            while(true)
            {
                agent = PromptOptionsAndCreateAgent(map);
                map.nodeTraversed = 0;
                map.nodeExpanded = 0;
                map.GenerateMap();
                result = agent.Search(true);
                if (result != null && result.isGoal)
                {
                    DrawPath(result, map, 5);
                }
                else
                {
                    Console.WriteLine("No path found!");
                }
            }
        }
        
    }


    static Map GenerateMapWithData(string content)
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

        
        //create the map object
        return new Map(mapSize, startPos, goalPosArray, wallsPos);
    }

    static void PrintPathText(TraversalNode? node)
    {
        //path stack
        Stack<string> stack = new Stack<string>();

        while (node != null)
        {
            stack.Push(node.pathMsg);
            node = node.parent;
        }

        while (stack.Count > 0)
        {
            Console.Write(stack.Pop());
        }
    }

    static void DrawPath(TraversalNode? node, Map map, int delay = 50)
    {
        while (node != null)
        {
            map.MarkNodeAsSolution(node);
            map.DrawMap(delay);
            node = node.parent;
        }
    }
    
    static Agent? PromptOptionsAndCreateAgent(Map map)
    {
        while (true)
        {
            Console.WriteLine("\n===========\nChoose options by pressing a number key:\n1.BFS\n2.DFS\n3.GBFS\n4.AS\n5.Dijkstra\n6.Iterative Deepening A*\n9.Quit");
            var input = Console.ReadKey();
            Console.Clear();
            switch (input.KeyChar)
            {
                case '1':
                    map.agentName = "BFS";
                    return new BFSAgent(map);
                case '2':
                    map.agentName = "DFS";
                    return new DFSAgent(map);
                case '3':
                    map.agentName = "GBFS";
                    return new GBFSAgent(map);
                case '4':
                    map.agentName = "A*";
                    return new AStarAgent(map);
                case '5':
                    map.agentName = "Dijkstra";
                    return new Cus1Agent(map);
                case '6':
                    map.agentName = "IDA*";
                    return new Cus2Agent(map);
                case '9':
                    Environment.Exit(0);
                    return new Cus2Agent(map);
                default:
                    Console.WriteLine("Invalid input!");
                    break;
            }
        }
    }
}