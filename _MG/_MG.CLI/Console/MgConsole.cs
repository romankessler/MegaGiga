namespace _MG.CLI.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using _MG.CLI.Helper;

    public static class MgConsole
    {
        private static string[] titleLines;

        private static Task runningTitleTask;

        private static CancellationTokenSource cancellationTokenSource;

        public static void Maximize()
        {
            ConsoleWindowHelper.Maximize();
        }

        public static void StartBlinkingMainTitle(string title, int bottomAndTopLineMargin = 0)
        {
            var titleLines = StringLineMarginHelper.GetTitleWithMarginLines(title, bottomAndTopLineMargin);
            StartBlinkingMainTitle(titleLines);
        }

        public static void StartBlinkingMainTitle(string[] titleLines, int colorTime = 200, List<Tuple<ConsoleColor, ConsoleColor>> colorList = null)
        {
            MgConsole.titleLines = titleLines;

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
                WriteMainTitle(MgConsole.titleLines, colorList.First().Item1, colorList.First().Item2);

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
                                    WriteMainTitle(MgConsole.titleLines, color.Item1, color.Item2);
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
            WriteFancyMainTitle(titleLines, color, backgroundColor);

            var top = Console.CursorTop;
            var left = Console.CursorLeft;

            Console.SetCursorPosition(Math.Max(left, cursorLeft), Math.Max(top, cursorTop));
            Console.CursorVisible = true;
        }

        public static void WriteTitle(params string[] titleLines)
        {
            Console.WriteLine(FrameStringHelper.GetDoubleFrameTop());
            foreach (var line in titleLines)
            {
                Console.WriteLine(FrameStringHelper.GetDoubleFrameLine(line));
            }
            Console.WriteLine(FrameStringHelper.GetDoubleFrameBottom());
        }

        public static void LogMainTitle(params string[] titleLines)
        {
            Console.WriteLine(FrameStringHelper.GetBlockFrameTop());
            foreach (var line in titleLines)
            {
                Console.WriteLine(FrameStringHelper.GetBlockFrameLineCentered(line));
            }
            Console.WriteLine(FrameStringHelper.GetBlockFrameBottom());
        }

        public static void WriteFancyMainTitle(
            ConsoleColor color = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black,
            params string[] titleLines)
        {
            WriteFancyMainTitle(titleLines, color, backgroundColor);
        }

        public static void WriteFancyMainTitle(params string[] titleLines)
        {
            WriteFancyMainTitle(titleLines, ConsoleColor.White, ConsoleColor.Black);
        }

        private static void WriteFancyMainTitle(
            string[] titleLines,
            ConsoleColor color = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            // TODO (ROK): 
            var titleWidth = FrameStringHelper.GetConsoleWidth();

            Console.ForegroundColor = color;
            Console.BackgroundColor = backgroundColor;

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

            Console.ResetColor();
        }

        public static void WriteSubTitle(params string[] titleLines)
        {
            Console.WriteLine(FrameStringHelper.GetSingleFrameTop());
            foreach (var line in titleLines)
            {
                Console.WriteLine(FrameStringHelper.GetSingleFrameLine(line));
            }
            Console.WriteLine(FrameStringHelper.GetSingleFrameBottom());
        }

        public static void StopBlinkingMainTitle()
        {
            cancellationTokenSource.Cancel();
        }
    }
}