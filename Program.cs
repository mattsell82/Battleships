using System;
using System.Collections.Concurrent;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;

namespace sänkaskepp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool menuLoop = true;
            while (menuLoop)
	        {
                Console.WriteLine(
                    "Välkommen till till sänkaskepp!\n\n" +
                    "Du kan nu göra följande val:\n" +
                    "1. För att börja sepla\n" +
                    "2. För att få hjälp\n" +
                    "3. För att avsluta\n\n" +
                    "Ange en siffra 1-3 och tryck enter: ");

                if (Int32.TryParse(Console.ReadLine(), out int menuChoice))
                {

                    switch (menuChoice)
                    {
                        case 1:
                            Console.Clear();
                            Game gameOne = new Game();
                            gameOne.PlayGame();
                            menuLoop = true;
                            break;

                        case 2:
                            Console.Clear();
                            Console.WriteLine("help");
                            Console.ReadKey();
                            menuLoop = true;
                            break;

                        case 3:
                            Console.WriteLine("Avslutar");
                            menuLoop = false;
                            break;

                        default:
                            Console.WriteLine("Please enter a valid choice from the menu.");
                            menuLoop = true;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid choice from the menu.");
                }


                Console.Clear();
            }
        }
    }
}
