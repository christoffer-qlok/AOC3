using System.Text;

namespace AOC3
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
            bool hasAdjSymbol = false;
            int sum = 0;
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (char.IsNumber(map[y][x]))
                    {
                        curNum.Append(map[y][x]);
                        if (HasAdjSymbol(x, y, map))
                        {
                            hasAdjSymbol = true;
                        }
                    }

                    if (!char.IsNumber(map[y][x]) && curNum.Length > 0)
                    {
                        if (hasAdjSymbol)
                        {
                            sum += int.Parse(curNum.ToString());
                        }
                        hasAdjSymbol = false;
                        curNum.Clear();
                    }
                }

                if(curNum.Length > 0)
                {
                    if (hasAdjSymbol)
                    {
                        sum += int.Parse(curNum.ToString());
                    }
                    hasAdjSymbol = false;
                    curNum.Clear();
                }
            }
            Console.WriteLine(sum);
        }

        static bool HasAdjSymbol(int x, int y, char[][] map)
        {
            for(int dx = -1; dx <= 1; dx++)
            {
                for(int dy = -1; dy <= 1; dy++)
                {
                    if(dx + x < 0 || dx + x >= map[y].Length || dy + y < 0 || dy + y >= map.Length)
                    {
                        continue;
                    }

                    char c = map[y + dy][x + dx];
                    if (char.IsNumber(c) || c == '.') { continue; }
                    return true;
                }
            }
            return false;
        }
    }
}