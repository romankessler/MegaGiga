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
            //SampleBlinkingTitle();
            //SampleProgressbar();
            SampleStateMessages();
        }

        private static void SampleStateMessages()
        {
            MgConsole.WriteDebug("Die Einstellungen sind leer", "Bitte Feld sowieso ausfüllen.");
            MgConsole.WriteMessage("Die Einstellungen sind leer", "Bitte Feld sowieso ausfüllen.");
            MgConsole.WriteInformation("Die Einstellungen sind leer", "Bitte Feld sowieso ausfüllen.");
            MgConsole.WriteWarning("Die Einstellungen sind leer", "Bitte Feld sowieso ausfüllen.");
            MgConsole.WriteError("Die Einstellungen sind leer", "Bitte Feld sowieso ausfüllen.");

            Console.ReadLine();
        }

        private static void SampleProgressbar()
        {
            var mgProgressbar = new MgProgressbar();
            mgProgressbar.InitializeProgressBar();

            for (var value = 0; value <= 100; value++)
            {
                mgProgressbar.SetProgressValue(value, "Installing...");
                Thread.Sleep(50);
            }
            Console.ReadLine();
            Console.Clear();
        }

        private static void SampleBlinkingTitle()
        {
            MgConsole.StartBlinkingMainTitle("YEEEEAAAAAH !!!!");
            Console.ReadLine();
            MgConsole.StopBlinkingMainTitle();
        }
    }
}
