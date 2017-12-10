using System;
using _MG.CLI.Helper;

namespace _MG.CLI.Console
{
    public partial class MgTitle
    {
        private const ConsoleColor DEFAUL_FOREGROUND_COLOR = ConsoleColor.White;
        private const ConsoleColor DEFAULT_BACKGROUND_COLOR = ConsoleColor.Black;

        private void WriteMainTitle(
            string[] titleLines,
            ConsoleColor color = DEFAUL_FOREGROUND_COLOR,
            ConsoleColor backgroundColor = DEFAULT_BACKGROUND_COLOR)
        {
            var cursorTop = System.Console.CursorTop;
            var cursorLeft = System.Console.CursorLeft;

            System.Console.CursorVisible = false;
            System.Console.SetCursorPosition(0, 0);
            WriteTitle(BorderStringHelper.BorderType.GradientTopBottomBorder, color, backgroundColor, titleLines);

            var top = System.Console.CursorTop;
            var left = System.Console.CursorLeft;

            System.Console.SetCursorPosition(Math.Max(left, cursorLeft), Math.Max(top, cursorTop));
            System.Console.CursorVisible = true;
        }

        public void WriteTitle(BorderStringHelper.BorderType borderType, params string[] titleLines)
        {
            WriteTitle(borderType, DEFAUL_FOREGROUND_COLOR, DEFAULT_BACKGROUND_COLOR, titleLines);
        }

        public void WriteTitle(BorderStringHelper.BorderType borderType, ConsoleColor foreground,
            ConsoleColor background, params string[] titleLines)
        {
            var foregroundColor = System.Console.ForegroundColor;
            var backgroundColor = System.Console.BackgroundColor;

            System.Console.ForegroundColor = foreground;
            System.Console.BackgroundColor = background;

            System.Console.WriteLine(BorderStringHelper.GetBorderTop(borderType));
            foreach (var line in titleLines)
                System.Console.WriteLine(BorderStringHelper.GetBorderLine(borderType, line));
            System.Console.WriteLine(BorderStringHelper.GetBorderBottom(borderType));

            System.Console.ForegroundColor = foregroundColor;
            System.Console.BackgroundColor = backgroundColor;
        }
    }
}