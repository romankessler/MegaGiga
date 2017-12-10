using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _MG.CLI.Console
{
    public class MgStateWriter
    {
        public void WriteWarning(params string[] warningText)
        {
            const string WARNING_CAPTION = "WARNING";
            WriteStateMessage(WARNING_CAPTION, warningText, ConsoleColor.Yellow, ConsoleColor.Black);
        }

        public void WriteError(params string[] errorText)
        {
            const string ERROR_CAPTION = "ERROR";
            WriteStateMessage(ERROR_CAPTION, errorText, ConsoleColor.Red, ConsoleColor.Black);
        }

        public void WriteInformation(params string[] informationText)
        {
            const string INFORMATION_CAPTION = "INFORMATION";
            WriteStateMessage(INFORMATION_CAPTION, informationText, ConsoleColor.Cyan, ConsoleColor.Black);
        }

        public void WriteMessage(params string[] messageText)
        {
            const string MESSAGE_CAPTION = "MESSAGE";
            WriteStateMessage(MESSAGE_CAPTION, messageText, ConsoleColor.White, ConsoleColor.Black);
        }

        public void WriteDebug(params string[] debugText)
        {
#if DEBUG
            const string DEBUG_CAPTION = "DEBUG";
            WriteStateMessage(DEBUG_CAPTION, debugText, ConsoleColor.Gray, ConsoleColor.Black);
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
                System.Console.BackgroundColor = primaryColor;
                System.Console.ForegroundColor = secondaryColor;
                if (message.First() == text)
                {
                    System.Console.Write($"".PadLeft(Math.Max(PADDING_LEFT - stateCaption.Length, 0)));
                }
                else
                {
                    System.Console.Write($"".PadLeft(PADDING_LEFT));
                }
                System.Console.BackgroundColor = secondaryColor;
                System.Console.ForegroundColor = primaryColor;
                System.Console.Write($" {text}");

                System.Console.WriteLine();
            }
        }
    }
}
