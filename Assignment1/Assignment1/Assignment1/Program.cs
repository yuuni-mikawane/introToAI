using Assignment1;


class MainClass
{
    static void Main(string[] args)
    {
        string path = "RobotNav-test.txt";
        if (args.Length == 2)
            path = args[0];
        StreamReader reader = new StreamReader(path);
        string content = reader.ReadToEnd();
        Console.WriteLine(content);
        reader.Close();

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
        Vector2 mapSize = new Vector2(Int32.Parse(mapSizeString[0]), Int32.Parse(mapSizeString[1]));

        //processing start pos
        Vector2 startPos = new Vector2(Int32.Parse(startPosString[0]), Int32.Parse(startPosString[1]));

        //processing goal positions
        List<Vector2> goalPosArray = new List<Vector2>();

        //processing wall positions
        List<Wall> wallsPos = new List<Wall>();

        for (int i = 3; i < lines.Length; i++)
        {
            string[] wallsString = lines[i].Trim(new char[] { '(', ')', ' ' }).Split(',');
            Console.WriteLine(wallsString[2]);
            //creating a wall and adding it to list of walls
            wallsPos.Add(new Wall(new Vector2(Int32.Parse(wallsString[0]), Int32.Parse(wallsString[1])),
                Int32.Parse(wallsString[2]), Int32.Parse(wallsString[3])));
        }

        foreach (string goalState in goalStatesString)
        {
            string[] pos = goalState.Trim(new char[] { '(', ')', ' ' }).Split(',');
            int x = Int32.Parse(pos[0]);
            int y = Int32.Parse(pos[1]);
            goalPosArray.Add(new Vector2(x, y));
        }

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