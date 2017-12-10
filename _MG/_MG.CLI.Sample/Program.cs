using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using _MG.CLI.Helper;

namespace _MG.CLI.Sample
{
    using _MG.CLI.Console;

    using Console = System.Console;

    class Program
    {
        static void Main(string[] args)
        {
            //MgConsole.Maximize();
            SampleBlinkingTitle();
            SampleTitles();
            SampleProgressbar();
            SampleStateMessages();
        }

        private static void SampleTitles()
        {
            Console.Clear();
            MgConsole.Title.WriteTitle(BorderStringHelper.BorderType.SingleBorder, "Single-Border");
            Console.ReadLine();
            Console.Clear();
            MgConsole.Title.WriteTitle(BorderStringHelper.BorderType.DoubleBorder, "Double-Border");
            Console.ReadLine();
            Console.Clear();
            MgConsole.Title.WriteTitle(BorderStringHelper.BorderType.BlockBorder, "Block-Border");
            Console.ReadLine();

            Console.Clear();
        }

        private static void SampleStateMessages()
        {
            MgConsole.ConsoleLogger.WriteDebug("Die Einstellungen sind leer", "Bitte Feld sowieso ausfüllen.");
            MgConsole.ConsoleLogger.WriteMessage("Die Einstellungen sind leer", "Bitte Feld sowieso ausfüllen.");
            MgConsole.ConsoleLogger.WriteInformation("Die Einstellungen sind leer", "Bitte Feld sowieso ausfüllen.");
            MgConsole.ConsoleLogger.WriteWarning("Die Einstellungen sind leer", "Bitte Feld sowieso ausfüllen.");
            MgConsole.ConsoleLogger.WriteError("Die Einstellungen sind leer", "Bitte Feld sowieso ausfüllen.");

            Console.ReadLine();
        }

        private static void SampleProgressbar()
        {
            for (var value = 0; value <= 100; value++)
            {
                MgConsole.Progressbar.SetProgressValue(value, "Installing...");
                Thread.Sleep(20);
            }
            Console.ReadLine();
            Console.Clear();
        }

        private static void SampleBlinkingTitle()
        {
            MgConsole.Title.StartBlinkingMainTitle("YEEEEAAAAAH !!!!");
            Console.ReadLine();
            MgConsole.Title.StopBlinkingMainTitle();
        }
    }
}
