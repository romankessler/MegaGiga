using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using _MG.CLI.Helper;

namespace _MG.CLI.Console
{
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

        public static void StartBlinkingMainTitle(string[] titleLines, int colorTime = 200,
            List<Tuple<ConsoleColor, ConsoleColor>> colorList = null)
        {
            MgConsole.titleLines = titleLines;

            if (colorList == null)
                colorList = new List<Tuple<ConsoleColor, ConsoleColor>>
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
                        ConsoleColor.Black)
                };

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
                                break;

                            foreach (var color in colorList)
                            {
                                if (cancellationToken.IsCancellationRequested)
                                    break;
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
            var cursorTop = System.Console.CursorTop;
            var cursorLeft = System.Console.CursorLeft;

            System.Console.CursorVisible = false;
            System.Console.SetCursorPosition(0, 0);
            WriteFancyMainTitle(titleLines, color, backgroundColor);

            var top = System.Console.CursorTop;
            var left = System.Console.CursorLeft;

            System.Console.SetCursorPosition(Math.Max(left, cursorLeft), Math.Max(top, cursorTop));
            System.Console.CursorVisible = true;
        }

        public static void WriteTitle(params string[] titleLines)
        {
            System.Console.WriteLine(BorderStringHelper.GetDoubleFrameTop());
            foreach (var line in titleLines)
                System.Console.WriteLine(BorderStringHelper.GetDoubleFrameLine(line));
            System.Console.WriteLine(BorderStringHelper.GetDoubleFrameBottom());
        }

        public static void LogMainTitle(params string[] titleLines)
        {
            System.Console.WriteLine(BorderStringHelper.GetBlockFrameTop());
            foreach (var line in titleLines)
                System.Console.WriteLine(BorderStringHelper.GetBlockFrameLineCentered(line));
            System.Console.WriteLine(BorderStringHelper.GetBlockFrameBottom());
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

        public static void WriteWarning(params string[] warningText)
        {
            const string WARNING_CAPTION = "WARNING:";
            WriteStateMessage(WARNING_CAPTION, warningText, ConsoleColor.Yellow, ConsoleColor.Black);
        }
        public static void WriteError(params string[] errorText)
        {
            const string ERROR_CAPTION = "ERROR:";
            WriteStateMessage(ERROR_CAPTION, errorText, ConsoleColor.Red, ConsoleColor.Black);
        }

        public static void WriteInformation(params string[] informationText)
        {
            const string INFORMATION = "INFORMATION:";
            WriteStateMessage(INFORMATION, informationText, ConsoleColor.Cyan, ConsoleColor.Black);
        }

        public static void WriteDebug(params string[] debugText)
        {
#if DEBUG
            const string DEBUG_TEXT = "DEBUG:";
            WriteStateMessage(DEBUG_TEXT, debugText, ConsoleColor.Gray, ConsoleColor.Black);
#endif 
        }

        private static void WriteStateMessage(string stateCaption, string[] message, ConsoleColor primaryColor, ConsoleColor secondaryColor)
        {
            const int PADDING_LEFT = 13;
            System.Console.BackgroundColor = primaryColor;
            System.Console.ForegroundColor = secondaryColor;

            System.Console.Write(stateCaption);

            foreach (var text in message)
            {
                System.Console.BackgroundColor = secondaryColor;
                System.Console.ForegroundColor = primaryColor;

                const string INDENT = "   ";

                if (message.First() == text)
                {
                    System.Console.Write($"".PadLeft(PADDING_LEFT-stateCaption.Length) +$"{INDENT}{text}");
                }
                else
                {
                    System.Console.Write($"".PadLeft(PADDING_LEFT) + $"{INDENT}{text}");
                }
                System.Console.WriteLine();
            }
        }

        private static void WriteFancyMainTitle(
            string[] titleLines,
            ConsoleColor color = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            // TODO (ROK): 
            var titleWidth = BorderStringHelper.GetConsoleWidth();

            System.Console.ForegroundColor = color;
            System.Console.BackgroundColor = backgroundColor;

            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine($"░".PadRight(titleWidth, '░') + "░");
            System.Console.WriteLine($"▒".PadRight(titleWidth, '▒') + "▒");
            System.Console.WriteLine($"▓".PadRight(titleWidth, '▓') + "▓");
            System.Console.WriteLine($"█".PadRight(titleWidth, '█') + "█");

            foreach (var line in titleLines)
            {
                var centerPadding = titleWidth / 2 - line.Length / 2;
                var lineLength = line.Length % 2 == 0 ? line.Length : line.Length - 1; // for odd length

                System.Console.WriteLine("█".PadRight(centerPadding) + $"{line}".PadRight(centerPadding + lineLength) +
                                         "█");
            }
            System.Console.WriteLine($"█".PadRight(titleWidth, '█') + "█");
            System.Console.WriteLine($"▓".PadRight(titleWidth, '▓') + "▓");
            System.Console.WriteLine($"▒".PadRight(titleWidth, '▒') + "▒");
            System.Console.WriteLine($"░".PadRight(titleWidth, '░') + "░");
            System.Console.WriteLine(string.Empty);

            System.Console.ResetColor();
        }

        public static void WriteSubTitle(params string[] titleLines)
        {
            System.Console.WriteLine(BorderStringHelper.GetSingleFrameTop());
            foreach (var line in titleLines)
                System.Console.WriteLine(BorderStringHelper.GetSingleFrameLine(line));
            System.Console.WriteLine(BorderStringHelper.GetSingleFrameBottom());
        }

        public static void StopBlinkingMainTitle()
        {
            cancellationTokenSource.Cancel();
        }
    }
}