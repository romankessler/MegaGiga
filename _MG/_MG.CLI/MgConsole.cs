using _MG.CLI.Console;
using _MG.CLI.Helper;

namespace _MG.CLI
{
    public static class MgConsole
    {
        static MgConsole()
        {
            Title = new MgTitle();
            ConsoleLogger = new MgConsoleLogger();
            Progressbar = new MgProgressbar();
            Progressbar.InitializeProgressBar();
        }

        public static MgConsoleLogger ConsoleLogger { get; set; }

        public static MgProgressbar Progressbar { get; set; }

        public static MgTitle Title { get; set; }


        public static void Maximize()
        {
            ConsoleWindowHelper.Maximize();
        }
    }
}