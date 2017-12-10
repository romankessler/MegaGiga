using System;

namespace _MG.CLI.Helper
{
    public class BorderStringHelper
    {
        public enum BorderType
        {
            SingleBorder,
            DoubleBorder,
            BlockBorder,
            GradientTopBottomBorder
        }

        internal static int GetConsoleWidth()
        {
            int windowWidth;

            try
            {
                windowWidth = System.Console.WindowWidth;
            }
            catch (Exception)
            {
                windowWidth = 80;
            }

            var width = windowWidth - 1;
            var length = width % 2 == 0 ? width : width - 1; // for odd length

            return length;
        }

        public static string GetBorderTop(BorderType borderType)
        {
            var consoleWidth = GetConsoleWidth();
            return GetBorderTop(borderType, consoleWidth);
        }

        public static string GetBorderTop(BorderType borderType, int borderWidth)
        {
            switch (borderType)
            {
                case BorderType.SingleBorder:
                    return $"┌".PadRight(borderWidth, '─') + "┐";
                case BorderType.DoubleBorder:
                    return $"╔".PadRight(borderWidth, '═') + "╗";
                case BorderType.BlockBorder:
                    return $"▄".PadRight(borderWidth, '▄') + "▄";
                case BorderType.GradientTopBottomBorder:
                    return $"░".PadRight(borderWidth, '░') + "░" + Environment.NewLine
                           + $"▒".PadRight(borderWidth, '▒') + "▒" + Environment.NewLine
                           + $"▓".PadRight(borderWidth, '▓') + "▓" + Environment.NewLine
                           + $"█".PadRight(borderWidth, '█') + "█";
                default:
                    throw new ArgumentOutOfRangeException(nameof(borderType), borderType, null);
            }
        }

        public static string GetBorderBottom(BorderType borderType)
        {
            var consoleWidth = GetConsoleWidth();
            return GetBorderBottom(borderType, consoleWidth);
        }

        public static string GetBorderBottom(BorderType borderType, int borderWidth)
        {
            switch (borderType)
            {
                case BorderType.SingleBorder:
                    return $"└".PadRight(borderWidth, '─') + "┘";
                case BorderType.DoubleBorder:
                    return $"╚".PadRight(borderWidth, '═') + "╝";
                case BorderType.BlockBorder:
                    return $"▀".PadRight(borderWidth, '▀') + "▀";
                case BorderType.GradientTopBottomBorder:
                    return $"█".PadRight(borderWidth, '█') + "█" + Environment.NewLine
                           + $"▓".PadRight(borderWidth, '▓') + "▓" + Environment.NewLine
                           + $"▒".PadRight(borderWidth, '▒') + "▒" + Environment.NewLine
                           + $"░".PadRight(borderWidth, '░') + "░";
                default:
                    throw new ArgumentOutOfRangeException(nameof(borderType), borderType, null);
            }
        }

        public static string GetBorderLine(BorderType borderType, string line)
        {
            var consoleWidth = GetConsoleWidth();
            return GetBorderLine(borderType, consoleWidth, line);
        }

        public static string GetBorderLine(BorderType borderType, int borderWidth, string line)
        {
            switch (borderType)
            {
                case BorderType.SingleBorder:
                    return $"│{line}".PadRight(borderWidth) + "│";
                case BorderType.DoubleBorder:
                    return $"║{line}".PadRight(borderWidth) + "║";
                case BorderType.BlockBorder:
                    return $"█{line}".PadRight(borderWidth) + "█";
                case BorderType.GradientTopBottomBorder:
                    return $"█{line}".PadRight(borderWidth) + "█";
                default:
                    throw new ArgumentOutOfRangeException(nameof(borderType), borderType, null);
            }
        }

        internal static string GetBlockFrameLineCentered(string line, int titleWidth)
        {
            var centerPadding = titleWidth / 2 - line.Length / 2;
            var lineLength = line.Length % 2 == 0 ? line.Length : line.Length - 1; // for odd length
            var centeredTitle = "█".PadRight(centerPadding) + $"{line}".PadRight(centerPadding + lineLength) + "█";
            return centeredTitle;
        }
    }
}