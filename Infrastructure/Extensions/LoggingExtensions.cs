using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Logging;

namespace Infrastructure.Extensions
{
    public static class LoggingExtensions
    {
        private static readonly Lazy<List<MessageColor>> MessageColors 
            = new Lazy<List<MessageColor>>(() => Enum.GetValues(typeof(MessageColor)).Cast<MessageColor>().ToList());

        public static MessageColor GetColor(this int number)
        {
            var allColors = MessageColors.Value;

            number = Math.Abs(number);

            return allColors[number % allColors.Count];
        }
    }
}