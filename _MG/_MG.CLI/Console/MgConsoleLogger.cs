using System;
using System.Linq;

namespace _MG.CLI.Console
{
    public class MgConsoleLogger
    {
        private const string WARNING_CAPTION = "WARNING";
        private const string ERROR_CAPTION = "ERROR";
        private const string INFORMATION_CAPTION = "INFORMATION";
        private const string MESSAGE_CAPTION = "MESSAGE";
        private const string DEBUG_CAPTION = "DEBUG";

        public void WriteWarning(params string[] warningText)
        {
            WriteStateMessage(WARNING_CAPTION, warningText, ConsoleColor.Yellow, ConsoleColor.Black);
        }

        public void WriteError(params string[] errorText)
        {
            WriteStateMessage(ERROR_CAPTION, errorText, ConsoleColor.Red, ConsoleColor.Black);
        }

        public void WriteInformation(params string[] informationText)
        {
            WriteStateMessage(INFORMATION_CAPTION, informationText, ConsoleColor.Cyan, ConsoleColor.Black);
        }

        public void WriteMessage(params string[] messageText)
        {
            WriteStateMessage(MESSAGE_CAPTION, messageText, ConsoleColor.White, ConsoleColor.Black);
        }

        public void WriteDebug(params string[] debugText)
        {
#if DEBUG
            WriteStateMessage(DEBUG_CAPTION, debugText, ConsoleColor.Gray, ConsoleColor.Black);
#endif
        }

        private static void WriteStateMessage(string stateCaption, string[] message, ConsoleColor primaryColor,
            ConsoleColor secondaryColor)
        {
            const int PADDING_LEFT = 13;
            System.Console.BackgroundColor = primaryColor;
            System.Console.ForegroundColor = secondaryColor;

            System.Console.Write(stateCaption);

            foreach (var text in message)
            {
                System.Console.BackgroundColor = primaryColor;
                System.Console.ForegroundColor = secondaryColor;
                if (message.First() == text)
                    System.Console.Write($"".PadLeft(Math.Max(PADDING_LEFT - stateCaption.Length, 0)));
                else
                    System.Console.Write($"".PadLeft(PADDING_LEFT));
                System.Console.BackgroundColor = secondaryColor;
                System.Console.ForegroundColor = primaryColor;
                System.Console.Write($" {text}");

                System.Console.WriteLine();
            }
        }
    }
}