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

            Console.WriteLine("Välkommen till till sänkaskepp!");

            Console.WriteLine("Du kan göra följande val:");
            Console.WriteLine("1. För att börja spela");
            Console.WriteLine("2. För att få hjälp");
            int choice = int.Parse(Console.ReadLine());

            

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Game gameOne = new Game();
                    gameOne.PlayGame();
                    return;

                case 2:
                    Console.WriteLine("help");
                    Console.ReadKey();
                    return;

                default:
                    Console.WriteLine("fel");
                    break;
            }
        }
        /*
        static bool Shoot(Player onName, int row, int col)
        {
            if (onName.ShootingLog.ValidateShot(row, col)) //Returnerar true om positionen inte har beskjutits tidigare
            {
                if (onName.GameCanvas.ReceiveShot(row, col) > 0) //Returnerar id från den ruta som träffas, 0 = miss.
                {
                    Console.WriteLine("Shot was hit!");
                    onName.ShootingLog.MarkShot(row, col, true);
                    //lägg till funktion för att ta bort en plupp hälsa från det träffade fartyget i listan ships.
                }
                else
                {
                    onName.ShootingLog.MarkShot(row, col, false);
                    Console.WriteLine("Sorry you missed!");
                }

                return true; //returnerar true om det gick att skjuta
            }
            else
            {
                Console.WriteLine("This position has already been shot");
                return false; //returnerar false om det inte gick att skjuta
            }
            

        }
        */
    }
}
