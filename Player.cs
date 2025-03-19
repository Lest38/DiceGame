﻿using System;
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
            DisplayAvailableDice(availableDice, excludeIndex);

            int choice = GetValidDieChoice(availableDice.Count, excludeIndex);
            if (choice == -1) return -1;

            AssignChosenDie(availableDice, choice);
            return choice;
        }

        private static void DisplayAvailableDice(List<Die> availableDice, int excludeIndex)
        {
            Console.WriteLine("Choose your dice:");
            for (int i = 0; i < availableDice.Count; i++)
            {
                if (i != excludeIndex)
                    Console.WriteLine($"{i} - {string.Join(",", availableDice[i].Faces)}");
            }
            Console.WriteLine("X - exit\n? - help");
        }

        private static int GetValidDieChoice(int diceCount, int excludeIndex)
        {
            int choice;
            do
            {
                choice = UserInput.GetUserChoice(diceCount);
                if (choice == -1) return -1;
                if (choice == excludeIndex)
                {
                    Console.WriteLine("This die is already taken. Choose another one.");
                }
            } while (choice == excludeIndex);

            return choice;
        }

        private void AssignChosenDie(List<Die> availableDice, int choice)
        {
            ChosenDie = availableDice[choice];
            Console.WriteLine($"You chose the [{string.Join(",", ChosenDie.Faces)}] dice.");
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
            Console.WriteLine($"Choose a number modulo {Die.FaceNumber}:");
            LastThrow = UserInput.GetUserChoice(Die.FaceNumber);
        }
    }
}
