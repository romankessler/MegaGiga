using System;

namespace _MG.CLI.Console
{
    public class MgProgressbar
    {
        private const int DEFAULT_MAX_VALUE = 100;
        private const int PROGRESSBAR_WIDTH_IN_CHARACTERS = 50;

        private const string DEFAULT_FINISH_MESSAGE = "FINISH :-)";

        private const string CHARACTER_PROGRESS_EMPTY = "░";
        private const string CHARACTER_PROGRESS_DONE = "█";

        private const ConsoleColor DEFAULT_PROGRESS_COLOR = ConsoleColor.Green;
        private const ConsoleColor DEFAULT_BAR_COLOR = ConsoleColor.Gray;

        private int _currentProgressValue;
        private int _maxProgressValue;

        private ConsoleColor _progressColor;

        private string _progressMessage;
        private bool _writeProgress;

        private void ResetProgressValue()
        {
            _currentProgressValue = 0;
        }

        public void SetProgressValue(int currentValue, string progressMassage = null)
        {
            var value = Math.Min(_maxProgressValue, currentValue);
            _currentProgressValue = value;
            _progressMessage = progressMassage;
            WriteProgress();
        }

        private void WriteProgress()
        {
            System.Console.Clear();
            System.Console.WriteLine();

            if (IsFinished())
            {
                System.Console.ForegroundColor = _progressColor;
                _progressMessage = DEFAULT_FINISH_MESSAGE;
            }

            System.Console.Write($" {_progressMessage} ");

            var currentAmount = GetCurrentCharacterAmount();
            for (var i = 0; i < currentAmount; i++)
            {
                System.Console.BackgroundColor = ConsoleColor.Black;
                System.Console.ForegroundColor = _progressColor;
                System.Console.Write(CHARACTER_PROGRESS_DONE);
            }

            var fillingAmount = GetFillingAmount(currentAmount);
            for (var i = 0; i < fillingAmount; i++)
            {
                System.Console.BackgroundColor = ConsoleColor.Black;
                System.Console.ForegroundColor = DEFAULT_BAR_COLOR;
                System.Console.Write(CHARACTER_PROGRESS_EMPTY);
            }

            if (_writeProgress)
            {
                if (IsFinished())
                    System.Console.ForegroundColor = _progressColor;
                var value = GetProgressPercentage();
                System.Console.Write($" {value}% ");
            }
        }

        private bool IsFinished()
        {
            return _maxProgressValue == _currentProgressValue;
        }

        private int GetProgressPercentage()
        {
            var currentPercentage = 100 / _maxProgressValue * _currentProgressValue;
            return currentPercentage;
        }

        private int GetFillingAmount(int currentAmount)
        {
            var characterAmount = PROGRESSBAR_WIDTH_IN_CHARACTERS - currentAmount;
            return characterAmount;
        }

        private int GetCurrentCharacterAmount()
        {
            var percentPerCharacter = GetPercentPerCharacter();
            var characterAmount = _currentProgressValue / percentPerCharacter;

            return Math.Min(PROGRESSBAR_WIDTH_IN_CHARACTERS, characterAmount);
        }

        private int GetPercentPerCharacter()
        {
            return _maxProgressValue / PROGRESSBAR_WIDTH_IN_CHARACTERS;
        }

        public void InitializeProgressBar(int maxValue = DEFAULT_MAX_VALUE, bool writeProgress = true,
            ConsoleColor progressColor = DEFAULT_PROGRESS_COLOR)
        {
            ResetProgressValue();
            _maxProgressValue = maxValue;
            _writeProgress = writeProgress;
            _progressColor = progressColor;
        }
    }
}