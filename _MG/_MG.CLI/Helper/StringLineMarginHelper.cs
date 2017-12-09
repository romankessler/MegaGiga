using System;
using System.Collections.Generic;

namespace _MG.CLI.Helper
{
    internal static class StringLineMarginHelper
    {
        internal static string[] GetTitleWithMarginLines(string title, int bottomAndTopLineMargin)
        {
            var cache = new List<string>();

            // Top Margin
            for (var i = 0; i < bottomAndTopLineMargin; i++)
                cache.Add(Environment.NewLine);

            cache.Add(title);

            // Bottom Margin
            for (var i = 0; i < bottomAndTopLineMargin; i++)
                cache.Add(Environment.NewLine);

            var titleLines = cache.ToArray();
            return titleLines;
        }
    }
}