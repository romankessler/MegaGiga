using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _MG.CLI.Console
{
    public class MgProgressbar
    {
        public MgProgressbar()
        {
        }

        private void ResetProgressValue()
        {
            _currentProgressValue = 0;
        }

        public void SetProgressValue(int currentValue,string progressMassage = null)
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
                _progressMessage = "FINISH";
            }
            
            System.Console.Write($" {_progressMessage} ");

            var currentAmount = GetCurrentCharacterAmount();
            for (var i = 0; i < currentAmount; i++)
            {
                System.Console.BackgroundColor = ConsoleColor.Black;
                System.Console.ForegroundColor = _progressColor;
                System.Console.Write("█");
            }

            var fillingAmount = GetFillingAmount(currentAmount);
            for (var i = 0; i < fillingAmount; i++)
            {
                System.Console.BackgroundColor = ConsoleColor.Black;
                System.Console.ForegroundColor = ConsoleColor.Gray;
                System.Console.Write("░");
            }

            if (_writeProgress)
            {
                if (IsFinished())
                {
                    System.Console.ForegroundColor = _progressColor;
                }
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

            return Math.Min(PROGRESSBAR_WIDTH_IN_CHARACTERS, characterAmount); ;
        }

        private int GetPercentPerCharacter()
        {
            return _maxProgressValue / PROGRESSBAR_WIDTH_IN_CHARACTERS;
        }

        public void InitializeProgressBar(int maxValue = DEFAULT_MAX_VALUE, bool writeProgress = true, ConsoleColor progressColor = ConsoleColor.Green)
        {
            ResetProgressValue();
            _maxProgressValue = maxValue;
            _writeProgress = writeProgress;
            _progressColor = progressColor;
        }

        private int _currentProgressValue;
        private int _maxProgressValue;

        private string _progressMessage;

        private bool _writeProgress;

        private ConsoleColor _progressColor;

        private const int DEFAULT_MAX_VALUE = 100;

        private const int PROGRESSBAR_WIDTH_IN_CHARACTERS = 50;
    }
}
