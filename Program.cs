using System;

namespace sänkaskepp
{
    class Program
    {
        static void Main(string[] args)
        {
            IGraphics graphics = new Graphics(); //Provar att använda ett interface till klassen ColorWriter för att komma åt PrintColor-metoder, istället för att göra klassen static.
            bool menuLoop = true;
            while (menuLoop)  //Menyloopen snurrar tills programmet avslutas
            {

                graphics.PrintIntroGraphic(); //Skriver ut ascii-art

                Console.Write(  //SKriver ut menyn
                    "\n\tWelcome to Battleships!\n\n" +
                    
                    "\t1. Start game\n" +
                    "\t2. Show help\n" +
                    "\t3. Quit program\n\n" +
                    "\tSelect option and press enter: ");


                if (Int32.TryParse(Console.ReadLine(), out int menuChoice)) //Valiterar att inmatningen är ett heltal 
                {

                    switch (menuChoice)
                    {
                        case 1:
                            Console.Clear();
                            Game gameOne = new Game(graphics);  //Instansierar ett nytt spel
                            gameOne.PlayGame(); //Kör spelet
                            break;

                        case 2:
                            Console.Clear();
                            Help help = new Help(graphics);  //bifogar graphics-objektet som argument till konstruktorn i klassen Help.
                            help.PrintHelp(); // Visar hjälpavsnittet
                            Console.ReadKey();
                            break;

                        case 3:
                            Console.WriteLine("\tAvslutar");
                            menuLoop = false;
                            break;

                        default:
                            Console.WriteLine("\tPlease enter a valid choice from the menu.");
                            menuLoop = true;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\tPlease enter a valid choice from the menu.");
                }

                Console.Clear();
            }
        }
    }
}
