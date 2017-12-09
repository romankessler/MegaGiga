using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _MG.CLI.Sample
{
    using _MG.CLI.Console;

    using Console = System.Console;

    class Program
    {
        static void Main(string[] args)
        {
            //MgConsole.Maximize();
            //MgConsole.StartBlinkingMainTitle("YEEEEAAAAAH !!!!");
            //Console.ReadLine();
            //MgConsole.StopBlinkingMainTitle();

            var mgProgressbar = new MgProgressbar();
            mgProgressbar.InitializeProgressBar(100, true, ConsoleColor.Cyan);

            for (var value = 0; value <= 100; value++)
            {
                mgProgressbar.SetProgressValue(value, "Installing...");
                Thread.Sleep(50);
            }

            Console.ReadLine();
        }
    }
}
