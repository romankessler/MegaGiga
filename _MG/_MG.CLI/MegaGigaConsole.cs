namespace _MG.CLI
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    public static class MegaGigaConsole
    {
        private const int SW_MAXIMIZE = 3;

        private static string[] titleLines;

        private static Task runningTitleTask;

        private static CancellationTokenSource cancellationTokenSource;

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        public static void Maximize()
        {
            var currentProcess = Process.GetCurrentProcess();
            ShowWindow(currentProcess.MainWindowHandle, SW_MAXIMIZE);
        }

        private static int GetConsoleWidth()
        {
            int windowWidth;

            try
            {
                windowWidth = Console.WindowWidth;
            }
            catch (Exception)
            {
                windowWidth = 80;
            }

            var width = windowWidth - 1;
            var length = width % 2 == 0 ? width : width - 1; // for odd length

            return length;
        }

        public static void StartBlinkingMainTitle(params string[] titleLines)
        {
            StartBlinkingMainTitle(titleLines);
        }

        public static void StartBlinkingMainTitle(string[] titleLines, int colorTime = 200, List<Tuple<ConsoleColor, ConsoleColor>> colorList = null)
        {
            MegaGigaConsole.titleLines = titleLines;

            if (colorList == null)
            {
                colorList = new List<Tuple<ConsoleColor, ConsoleColor>>()
                                {
                                    new Tuple<ConsoleColor, ConsoleColor>(
                                        ConsoleColor.Green,
                                        ConsoleColor.Black),
                                    new Tuple<ConsoleColor, ConsoleColor>(
                                        ConsoleColor.Cyan,
                                        ConsoleColor.Black),
                                    new Tuple<ConsoleColor, ConsoleColor>(
                                        ConsoleColor.Red,
                                        ConsoleColor.Black),
                                    new Tuple<ConsoleColor, ConsoleColor>(
                                        ConsoleColor.Magenta,
                                        ConsoleColor.Black),
                                    new Tuple<ConsoleColor, ConsoleColor>(
                                        ConsoleColor.Yellow,
                                        ConsoleColor.Black),
                                };
            }

            if (runningTitleTask == null)
            {
                WriteMainTitle(MegaGigaConsole.titleLines, colorList.First().Item1, colorList.First().Item2);

                var tokenSource = new CancellationTokenSource();
                cancellationTokenSource = tokenSource;
                var cancellationToken = tokenSource.Token;
                runningTitleTask = Task.Factory.StartNew(
                    () =>
                        {
                            while (true)
                            {
                                if (cancellationToken.IsCancellationRequested)
                                {
                                    break;
                                }

                                foreach (var color in colorList)
                                {
                                    if (cancellationToken.IsCancellationRequested)
                                    {
                                        break;
                                    }
                                    WriteMainTitle(MegaGigaConsole.titleLines, color.Item1, color.Item2);
                                    Thread.Sleep(colorTime);
                                }
                            }
                        },
                    cancellationToken);
            }
        }

        private static void WriteMainTitle(
            string[] titleLines,
            ConsoleColor color = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            var cursorTop = Console.CursorTop;
            var cursorLeft = Console.CursorLeft;

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = color;
            Console.BackgroundColor = backgroundColor;
            WriteFancyMainTitle(titleLines);
            Console.ResetColor();

            var top = Console.CursorTop;
            var left = Console.CursorLeft;

            Console.SetCursorPosition(Math.Max(left, cursorLeft), Math.Max(top, cursorTop));
            Console.CursorVisible = true;
        }

        public static void WriteTitle(params string[] titleLines)
        {
            var titleWidth = GetConsoleWidth();
            Console.WriteLine($"╔".PadRight(titleWidth, '═') + "╗");

            foreach (var line in titleLines)
            {
                Console.WriteLine($"║{line}".PadRight(titleWidth) + "║");
            }
            Console.WriteLine($"╚".PadRight(titleWidth, '═') + "╝");
        }

        public static void LogMainTitle(params string[] titleLines)
        {
            var titleWidth = GetConsoleWidth();
            Console.WriteLine($"▄".PadRight(titleWidth, '▄') + "▄");

            foreach (var line in titleLines)
            {
                var centerPadding = titleWidth / 2 - line.Length / 2;
                var lineLength = line.Length % 2 == 0 ? line.Length : line.Length - 1; // for odd length

                Console.WriteLine("█".PadRight(centerPadding) + $"{line}".PadRight(centerPadding + lineLength) + "█");
            }
            Console.WriteLine($"▀".PadRight(titleWidth, '▀') + "▀");
        }

        public static void WriteFancyMainTitle(params string[] titleLines)
        {
            var titleWidth = GetConsoleWidth();

            Console.WriteLine(string.Empty);
            Console.WriteLine($"░".PadRight(titleWidth, '░') + "░");
            Console.WriteLine($"▒".PadRight(titleWidth, '▒') + "▒");
            Console.WriteLine($"▓".PadRight(titleWidth, '▓') + "▓");
            Console.WriteLine($"█".PadRight(titleWidth, '█') + "█");

            foreach (var line in titleLines)
            {
                var centerPadding = titleWidth / 2 - line.Length / 2;
                var lineLength = line.Length % 2 == 0 ? line.Length : line.Length - 1; // for odd length

                Console.WriteLine("█".PadRight(centerPadding) + $"{line}".PadRight(centerPadding + lineLength) + "█");
            }
            Console.WriteLine($"█".PadRight(titleWidth, '█') + "█");
            Console.WriteLine($"▓".PadRight(titleWidth, '▓') + "▓");
            Console.WriteLine($"▒".PadRight(titleWidth, '▒') + "▒");
            Console.WriteLine($"░".PadRight(titleWidth, '░') + "░");
            Console.WriteLine(string.Empty);
        }

        public static void WriteSubTitle(params string[] titleLines)
        {
            var titleWidth = GetConsoleWidth();
            Console.WriteLine($"┌".PadRight(titleWidth, '─') + "┐");

            foreach (var line in titleLines)
            {
                Console.WriteLine($"│{line}".PadRight(titleWidth) + "│");
            }
            Console.WriteLine($"└".PadRight(titleWidth, '─') + "┘");
        }

        public static void StopBlinkingMainTitle()
        {
            cancellationTokenSource.Cancel();
        }
    }
}