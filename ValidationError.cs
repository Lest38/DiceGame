using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    internal class ValidationError
    {
        private string Message { get; }

        protected ValidationError(string message)
        {
            Message = message;
        }

        public static readonly ValidationError InvalidDiceCountLength =
            new("Please specify at least three dice.");

        public static readonly ValidationError InvalidFaceCount =
            new("Each die must have exactly 6 faces.");

        public static readonly ValidationError InvalidSelection =
            new("Invalid selection. Please choose a valid option.");

        public override string ToString()
        {
            return string.Join("\n",
                "Argument error.",
                Message);
        }
    }
}
