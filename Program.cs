using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Security.Cryptography;

namespace sänkaskepp
{
    class Program
    {
        static void Main(string[] args)
        {
            //creating player computer and adding it's ships
            Player computer = new Player("Computer");
            computer.GameCanvas.AddShip(1, 1);
            computer.GameCanvas.AddShip(1, 2);
            computer.GameCanvas.AddShip(2, 3);
            computer.GameCanvas.AddShip(2, 4);
            computer.GameCanvas.AddShip(3, 5);
            computer.GameCanvas.AddShip(3, 6);
            computer.GameCanvas.AddShip(4, 7);

            //creating human player and adding it's ships
            Console.Write("Welcome! Please enter your name: ");
            Player player1 = new Player(Console.ReadLine());

            player1.GameCanvas.AddShip(1, 1);
            player1.GameCanvas.AddShip(1, 2);
            player1.GameCanvas.AddShip(2, 3);
            player1.GameCanvas.AddShip(2, 4);
            player1.GameCanvas.AddShip(3, 5);
            player1.GameCanvas.AddShip(3, 6);
            player1.GameCanvas.AddShip(4, 7);

            while (true)
            {
                Console.WriteLine($"{player1.Name}s turn!");
                player1.GameCanvas.PrintCanvas();
                //Console.WriteLine(player1.GameCanvas.Score());        Print score?
                player1.ShootingLog.PrintCanvas();


                Console.WriteLine("enter row numer: ");
                int rowToShoot = int.Parse(Console.ReadLine());
                Console.WriteLine("enter col number: ");
                int colToShoot = int.Parse(Console.ReadLine());

                //FIXA shoot så att man får skjuta igen om positionen redan träffats
                Shoot(player1, rowToShoot, colToShoot);


                Console.WriteLine("Press any key to continue or ESC to close.");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Good bye!");
                    Environment.Exit(0);
                }
                Console.Clear();

            }



        }

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

    }
}
