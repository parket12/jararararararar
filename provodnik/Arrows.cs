using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Arrow
{
    public static int Arrows(DirectoryInfo directory)
    {
        var entries = directory.GetFileSystemInfos();
        int max = entries.Length;
        int position = 0;
        ConsoleKeyInfo key;

        do
        {
            Program.PrintMenu(directory, position);
            key = Console.ReadKey();

            if (key.Key == ConsoleKey.UpArrow && position > 0)
            {
                position--;
            }
            else if (key.Key == ConsoleKey.DownArrow && position < max - 1)
            {
                position++;
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Console.Clear();
                return -99;
            }

        } while (key.Key != ConsoleKey.Enter);

        return position;
    }
}


