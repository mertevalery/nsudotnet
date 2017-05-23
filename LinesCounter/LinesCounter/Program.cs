using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinesCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Ykajite rasshirenie alo!!");
                Console.ReadKey();  
                return;
            }

            string[] filepaths = Directory.GetFiles(
                /*"C:/Users/merte/Documents/Visual Studio 2017/Projects/LinesCounter/LinesCounter"*/
                Directory.GetCurrentDirectory(), "*." + args[0], SearchOption.AllDirectories);

            int allLines = 0;

            foreach (var file in filepaths)
                allLines += LineCounter(file);

            Console.WriteLine($"Total lines: {allLines}");
            Console.ReadKey();
        }

        private static int LineCounter(string filename)
        {
            int count = 0;
            bool commentMultiLine = false;
            bool countLine = true;
            using (var reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    countLine = true;
                    var readline = reader.ReadLine();
                    if (readline == null) return count;
                    readline = readline.Trim();
                    if (string.IsNullOrWhiteSpace(readline)) continue;

                    if (readline.StartsWith("//")) continue;

                    if (commentMultiLine && !readline.Contains("*/")) continue;

                    if (readline.Contains("/*"))
                    {
                        commentMultiLine = true;
                        if (readline.StartsWith("/*")) continue;
                    }
               
                    if (readline.Contains("*/"))
                    {
                        commentMultiLine = false;
                        if (readline.EndsWith("*/")) continue;
                    }

                    count++;
                }
                return count;
            }

        }
    }
}
