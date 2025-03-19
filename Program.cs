using DiceGame;

namespace DiceGame
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine(ValidationError.InvalidDiceCountLength);
                return;
            }

            List<Die> dice = args.Select(arg => new Die(arg.Split(',').Select(int.Parse).ToArray())).ToList();
            new Game(dice).Start();
        }
    }

}