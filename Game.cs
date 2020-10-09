using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace sänkaskepp
{
    class Game
    {        
        public Player Computer { get; set; }
        public Player Human { get; set; }
        public bool Turn { get; set; }

        public Game()
        {
            this.Turn = true;
            this.Computer = new Player("Computer");
            Console.WriteLine("Please enter your name:");
            this.Human = new Player(Console.ReadLine());
        }

        public void PlayGame()
        {
            Random randomGenerator = new Random();
            Console.Clear();
            while (true)
            {
                Console.WriteLine($"{Human.Name} score: \t\t{16 - Computer.GameCanvas.GetScore()}");
                Console.WriteLine($"Computers score: \t{16 - Human.GameCanvas.GetScore()}");
                Human.GameCanvas.PrintCanvas();
                Human.ShootingLog.PrintCanvas();


                if (Turn) //Human = 0 
                {
                    Console.WriteLine($"Your turn!");
                    int rowToShoot = GetCoordinate("Enter row: ");
                    int colToShoot = GetCoordinate("Enter col: ");

                    //FIXA shoot så att man får skjuta igen om positionen redan träffats
                    Shoot(Human, Computer, rowToShoot, colToShoot);
                    Turn = false;

                    Console.WriteLine("Press any key to continue or ESC to close.");
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine("Good bye!");
                        Environment.Exit(0);
                    }

                }
                else
                {
                    int randomRow = randomGenerator.Next(0, 10);
                    int randomCol = randomGenerator.Next(0, 10);

                    Console.WriteLine("Computers turn!");
                    Thread.Sleep(1000);
                    Console.WriteLine($"Computer shooting at row: {randomRow} col: {randomCol}");
                    Thread.Sleep(1000);
                    Shoot(Computer, Human, randomRow, randomCol);
                    Turn = true;
                    Thread.Sleep(2000);
                }

                Console.Clear();

            }

            static int GetCoordinate(string message)
            {
                int parsedOutput = 0;
                bool isNumerical = false;

                while (isNumerical == false || parsedOutput < 0 || parsedOutput > 9)
                {
                    Console.Write(message);
                    isNumerical = int.TryParse(Console.ReadLine(), out parsedOutput);
                }

                return parsedOutput;
            }

            static bool Shoot(Player attacker, Player target, int row, int col)
            {
                if (attacker.ShootingLog.ValidateShot(row, col)) //Returnerar true om positionen inte har beskjutits tidigare
                {
                    if (target.GameCanvas.ReceiveShot(row, col) > 0) //Returnerar id från den ruta som träffas, 0 = miss.
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Target hit!");
                        Console.ForegroundColor = ConsoleColor.White;
                        attacker.ShootingLog.MarkShot(row, col, true);
                        //lägg till funktion för att ta bort en plupp hälsa från det träffade fartyget i listan ships.
                    }
                    else
                    {
                        attacker.ShootingLog.MarkShot(row, col, false);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Sorry you missed!");
                        Console.ForegroundColor = ConsoleColor.White;
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

        /*
        public void PrintShipType(int shipid, string Name)
        {
            Computer.PrintShipType(shipid);
        }
        */
	}
}
