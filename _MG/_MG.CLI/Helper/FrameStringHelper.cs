namespace _MG.CLI.Helper
{
    using System;

    internal static class FrameStringHelper
    {
        internal static int GetConsoleWidth()
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

        internal static string GetDoubleFrameBottom()
        {
            var titleWidth = GetConsoleWidth();
            return GetDoubleFrameBottom(titleWidth);
        }

        internal static string GetDoubleFrameLine(string line)
        {
            var titleWidth = GetConsoleWidth();
            return GetDoubleFrameLine(line, titleWidth);
        }

        internal static string GetDoubleFrameTop()
        {
            var titleWidth = GetConsoleWidth();
            return GetDoubleFrameTop(titleWidth);
        }

        internal static string GetDoubleFrameBottom(int titleWidth)
        {
            return $"╚".PadRight(titleWidth, '═') + "╝";
        }

        internal static string GetDoubleFrameLine(string line, int titleWidth)
        {
            return $"║{line}".PadRight(titleWidth) + "║";
        }

        internal static string GetDoubleFrameTop(int titleWidth)
        {
            return $"╔".PadRight(titleWidth, '═') + "╗";
        }

        internal static string GetSingleFrameBottom(int titleWidth)
        {
            return $"└".PadRight(titleWidth, '─') + "┘";
        }

        internal static string GetSingleFrameLine(string line, int titleWidth)
        {
            return $"│{line}".PadRight(titleWidth) + "│";
        }

        internal static string GetSingleFrameTop(int titleWidth)
        {
            return $"┌".PadRight(titleWidth, '─') + "┐";
        }

        internal static string GetSingleFrameTop()
        {
            var consoleWidth = GetConsoleWidth();
            return GetSingleFrameTop(consoleWidth);
        }

        internal static string GetSingleFrameLine(string line)
        {
            var consoleWidth = GetConsoleWidth();
            return GetSingleFrameLine(line, consoleWidth);
        }

        internal static string GetSingleFrameBottom()
        {
            var consoleWidth = GetConsoleWidth();
            return GetSingleFrameBottom(consoleWidth);
        }

        internal static string GetBlockFrameBottom(int titleWidth)
        {
            return $"▀".PadRight(titleWidth, '▀') + "▀";
        }

        internal static string GetBlockFrameLineCentered(string line, int titleWidth)
        {
            var centerPadding = titleWidth / 2 - line.Length / 2;
            var lineLength = line.Length % 2 == 0 ? line.Length : line.Length - 1; // for odd length
            var centeredTitle = "█".PadRight(centerPadding) + $"{line}".PadRight(centerPadding + lineLength) + "█";
            return centeredTitle;
        }

        internal static string GetBlockFrameTop(int titleWidth)
        {
            return $"▄".PadRight(titleWidth, '▄') + "▄";
        }

        internal static string GetBlockFrameBottom()
        {
            var consoleWidth = GetConsoleWidth();
            return GetBlockFrameBottom(consoleWidth);
        }

        internal static string GetBlockFrameLineCentered(string line)
        {
            var consoleWidth = GetConsoleWidth();
            return GetBlockFrameLineCentered(line, consoleWidth);
        }

        internal static string GetBlockFrameTop()
        {
            var consoleWidth = GetConsoleWidth();
            return GetBlockFrameTop(consoleWidth);
        }
    }
}