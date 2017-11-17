using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _MG.CLI.Sample
{
    using _MG.CLI.Console;

    using Console = System.Console;

    class Program
    {
        static void Main(string[] args)
        {
            MgConsole.Maximize();
            MgConsole.WriteFancyMainTitle("YEEEEAAAAAH !!!!");
            Console.ReadLine();
        }
    }
}
