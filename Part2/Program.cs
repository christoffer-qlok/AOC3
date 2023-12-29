using System.Text;

namespace Part2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");
            char[][] map = new char[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                map[i] = lines[i].ToCharArray();
            }

            StringBuilder curNum = new StringBuilder();
            var allGears = new Dictionary<Coord, List<int>>();
            HashSet<Coord> foundGears = new HashSet<Coord>();

            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (char.IsNumber(map[y][x]))
                    {
                        curNum.Append(map[y][x]);
                        FindAdjGear(x, y, map, foundGears);
                    } else if(curNum.Length > 0)
                    {
                        ProcessNumber(curNum, allGears, foundGears);
                    }
                }

                if (curNum.Length > 0)
                {
                    ProcessNumber(curNum, allGears, foundGears);
                }
            }

            int sum = 0;
            foreach (var kvp in allGears)
            {
                if(kvp.Value.Count() == 2)
                {
                    sum += kvp.Value[0] * kvp.Value[1];
                }
            }
            Console.WriteLine(sum);
        }

        private static void ProcessNumber(StringBuilder curNum, Dictionary<Coord, List<int>> allGears, HashSet<Coord> foundGears)
        {
            int num = int.Parse(curNum.ToString());
            foreach (var gear in foundGears)
            {
                if (!allGears.ContainsKey(gear))
                {
                    allGears[gear] = new List<int>() { num };
                }
                else
                {
                    allGears[gear].Add(num);
                }
            }
            foundGears.Clear();
            curNum.Clear();
        }

        static void FindAdjGear(int x, int y, char[][] map, HashSet<Coord> foundGears)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx + x < 0 || dx + x >= map[y].Length || dy + y < 0 || dy + y >= map.Length)
                    {
                        continue;
                    }

                    char c = map[y + dy][x + dx];
                    if (c == '*') 
                    {
                        foundGears.Add(new Coord() { X = x + dx, Y = y + dy });
                    }
                }
            }
        }
    }
}