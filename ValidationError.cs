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
            new("Each die must have exactly the same number of faces.");

        public static readonly ValidationError InvalidSelection =
            new("Invalid selection. Please choose a valid option.");

        public static readonly ValidationError InvalidFaceFormat =
            new("All die faces must be numbers.");

        public override string ToString()
        {
            return string.Join("\n", "Argument error.", Message);
        }

        public static bool ValidateDiceInput(string[] args, out List<Die> dice)
        {
            dice = [];

            if (args.Length < 3)
            {
                Console.WriteLine(InvalidDiceCountLength);
                return false;
            }

            List<int[]> parsedDice = [];

            foreach (var arg in args)
            {
                var faces = arg.Split(',');
                if (!faces.All(f => int.TryParse(f, out _)))
                {
                    Console.WriteLine(InvalidFaceFormat);
                    return false;
                }

                parsedDice.Add(faces.Select(int.Parse).ToArray());
            }
            int faceCount = parsedDice[0].Length;
            if (parsedDice.Any(d => d.Length != faceCount))
            {
                Console.WriteLine(InvalidFaceCount);
                return false;
            }
            dice = parsedDice.Select(faces => new Die(faces)).ToList();
            return true;
        }
    }

}
