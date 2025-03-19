using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    internal class Player(string name, bool isComputer)
    {
        public string Name { get; } = name;
        public Die? ChosenDie { get; private set; }
        public bool IsComputer { get; } = isComputer;
        public int? LastThrow { get; private set; }

        public int ChooseDie(List<Die> availableDice, int excludeIndex = -1)
        {
            if (IsComputer)
            {
                return ComputerChooseDie(availableDice, excludeIndex);
            }
            else
            {
                return HumanChooseDie(availableDice, excludeIndex);
            }
        }

        public int HumanChooseDie(List<Die> availableDice, int excludeIndex)
        {
            Console.WriteLine("Choose your dice:");
            for (int i = 0; i < availableDice.Count; i++)
            {
                if (i != excludeIndex)
                    Console.WriteLine($"{i} - {string.Join(",", availableDice[i].Faces)}");
            }
            Console.WriteLine("X - exit\n? - help");

            int choice;
            do
            {
                choice = UserInput.GetUserChoice(availableDice.Count);
                if (choice == -1) return -1;
                if (choice == excludeIndex)
                {
                    Console.WriteLine("This die is already taken. Choose another one.");
                }
            } while (choice == excludeIndex);

            ChosenDie = availableDice[choice];
            Console.WriteLine($"You chose the [{string.Join(",", ChosenDie.Faces)}] dice.");
            return choice;
        }

        public int ComputerChooseDie(List<Die> availableDice, int excludeIndex)
        {
            int index;
            do
            {
                index = RandomNumberGenerator.GetInt32(availableDice.Count);
            } while (index == excludeIndex);
            ChosenDie = availableDice[index];
            Console.WriteLine($"{Name} chose the [{string.Join(",", ChosenDie.Faces)}] dice.");
            return 0;
        }

        public int MakeMove()
        {
            if (IsComputer)
            {
                LastThrow = ComputerChooseNumber();
            }
            else
            {
                LastThrow = PlayerChooseNumber();
            }
            return LastThrow ?? 0;
        }

        public int ComputerChooseNumber()
        {
            var (_, key, value) = HmacGenerator.GenerateHmac(6);
            Console.WriteLine($"{Name}'s number is {value} (KEY={key}).");
            return value;
        }

        public static int PlayerChooseNumber()
        {
            Console.WriteLine("Choose a number modulo 6:");
            return UserInput.GetUserChoice(6);
        }
    }
}
