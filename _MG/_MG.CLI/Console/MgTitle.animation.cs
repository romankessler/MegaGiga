using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using _MG.CLI.Helper;

namespace _MG.CLI.Console
{
    public partial class MgTitle
    {
        private const int DEFAULT_COLOR_TIME = 200;
        private const int DEFAULT_BOTTOM_TOP_MARGIN = 0;
        private static Task runningTitleTask;

        private static CancellationTokenSource cancellationTokenSource;


        public void StopBlinkingMainTitle()
        {
            cancellationTokenSource.Cancel();
        }

        public void StartBlinkingMainTitle(string title, int bottomAndTopLineMargin = DEFAULT_BOTTOM_TOP_MARGIN)
        {
            var titleLines = StringLineMarginHelper.GetTitleWithMarginLines(title, bottomAndTopLineMargin);
            StartBlinkingMainTitle(titleLines);
        }

        public void StartBlinkingMainTitle(string[] titleLines, int colorTime = DEFAULT_COLOR_TIME,
            List<Tuple<ConsoleColor, ConsoleColor>> colorList = null)
        {
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
                WriteMainTitle(titleLines, colorList.First().Item1, colorList.First().Item2);

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
                                WriteMainTitle(titleLines, color.Item1, color.Item2);
                                Thread.Sleep(colorTime);
                            }
                        }
                    },
                    cancellationToken);
            }
        }
    }
}
