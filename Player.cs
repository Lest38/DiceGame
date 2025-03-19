using System.Collections.Generic;

namespace DiceGame
{
    class Player
    {
        public string Name { get; }
        public bool IsComputer { get; }
        public Die? ChosenDie { get; private set; }
        public int? LastThrow { get; private set; }
        private string? currentKey;
        private string? currentHmac;

        public Player(string name, bool isComputer)
        {
            Name = name;
            IsComputer = isComputer;
        }

        public int ChooseDie(List<Die> dice, int excludeIndex = -1)
        {
            if (IsComputer)
            {
                return ComputerChooseDie(dice, excludeIndex);
            }
            else
            {
                return HumanChooseDie(dice, excludeIndex);
            }
        }

        private int ComputerChooseDie(List<Die> dice, int excludeIndex = -1)
        {
            Random random = new();
            int index;
            do
            {
                index = random.Next(dice.Count);
            } while (index == excludeIndex);
            ChosenDie = dice[index];
            Console.WriteLine($"{Name} chose die {string.Join(", ", dice[index].Faces)}.");
            return index;
        }

        private int HumanChooseDie(List<Die> dice, int excludeIndex = -1)
        {
            Console.WriteLine("Choose your die:");
            for (int i = 0; i < dice.Count; i++)
            {
                if (i != excludeIndex)
                    Console.WriteLine($"{i + 1} - {string.Join(", ", dice[i].Faces)}");
            }

            int choice = UserInput.GetUserChoice(dice.Count);
            if (choice == -1) return -1;

            ChosenDie = dice[choice - 1];
            return choice - 1;
        }

        public void PrepareMove()
        {
            if (ChosenDie == null) throw new InvalidOperationException("Die not chosen.");
            (currentHmac, currentKey, LastThrow) = HmacGenerator.GenerateHmac(Die.FaceNumber);
            ShowHmac();
        }

        public void ShowHmac()
        {
            if (currentHmac != null)
                Console.WriteLine($"{Name}'s HMAC: {currentHmac}");
        }

        public int MakeMove()
        {
            if (IsComputer)
            {
                ComputerChooseNumber();
            }
            else
            {
                PlayerChooseNumber();
            }
            return LastThrow ?? 0;
        }

        public void ComputerChooseNumber()
        {
            var (_, key, value) = HmacGenerator.GenerateHmac(Die.FaceNumber);
            Console.WriteLine($"{Name} chose the value (KEY={key}).");
            LastThrow = value;
        }

        public void PlayerChooseNumber()
        {
            Console.Write($"Choose a number modulo {Die.FaceNumber}: ");
            LastThrow = UserInput.GetUserChoice(Die.FaceNumber);
        }

        public void ShowKey()
        {
            if (currentKey != null)
                Console.WriteLine($"{Name}'s number is {LastThrow} (Key: {currentKey})");
        }
    }
}
