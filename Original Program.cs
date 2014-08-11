using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LI_TextAdventure
{
    class Program
    {
        /// <summary>
        /// Intro screen.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("             Mr. Sprinkle's Great Escape - A Text Adventure Game! ");
            Console.WriteLine("                    : created and written by Lincoln Li :         ");
            Console.ReadLine();
            Console.Clear();
            BeginGame();
        }

        /// <summary>
        /// Begins the game.
        /// </summary>
        static public void BeginGame()
        {
            Console.WriteLine("Mr. Sprinkle's Great Escape! ");
            Console.WriteLine("            o_              ");
            Console.WriteLine("         .-'  '.            ");
            Console.WriteLine("       .'    _-'-''--o      ");
            Console.WriteLine("      J    ,'' _      >.    ");
            Console.WriteLine();
            Console.WriteLine("*NOTE*: At any time, type 'help' or 'hint' if you need aid.");
            Console.WriteLine();
            while (true)
            {
                Console.Write("Would you like to begin (Y/y)? ");
                string newGame = Console.ReadLine();
                newGame = newGame.ToLower();

                if (newGame == "y" || newGame == "Y")
                {
                    Console.Clear();
                    break;
                }
                else if (newGame == "help")
                {
                    Console.WriteLine();
                    Help.HelpAndHints();
                    Console.WriteLine();
                }
                else if (newGame == "hint")
                {
                    Console.WriteLine();
                    Help.HelpAndHints();
                    Console.WriteLine();
                }

                else
                {
                    Console.WriteLine("Invalid Input!");
                    Console.WriteLine();
                }
            }

            Area1.RoomLockedCage();
        }
    }
}
