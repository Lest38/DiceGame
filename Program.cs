using DiceGame;

namespace DiceGame
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            if (!ValidationError.ValidateDiceInput(args, out List<Die> dice))
            {
                return;
            }

            new Game(dice).Start();
        }
    }

}