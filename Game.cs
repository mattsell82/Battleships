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
            int maxScore = Human.GameCanvas.GetMaxScore();
            int scoreHuman = 0;
            int scoreComputer = 0;

            Console.Clear();
            while (true)
            {
                scoreHuman = Computer.GameCanvas.GetHits();
                scoreComputer = Human.GameCanvas.GetHits();

                if (scoreHuman == maxScore)
                {
                    Console.WriteLine("Congratulations you won!\n");
                    Console.ReadKey();
                    break;
                }
                else if (scoreComputer == maxScore)
                {
                    Console.WriteLine("Sorry you lost!");
                    Console.ReadKey();
                    break;
                }

                Console.WriteLine($"{Human.Name} score: \t\t{scoreHuman} / {maxScore}");
                Console.WriteLine($"Computers score: \t{scoreComputer} / {maxScore}");
                Human.GameCanvas.PrintCanvas();
                Human.ShootingLog.PrintCanvas();

                bool shotOk = false;
                if (Turn) //Human = true  
                {
                    while (!shotOk)
                    {
                        Console.WriteLine($"Your turn!");
                        int rowToShoot = GetCoordinate("Enter row: ");
                        int colToShoot = GetCoordinate("Enter col: ");

                        shotOk = Shoot(Human, Computer, rowToShoot, colToShoot);
                    }
                    Turn = false;  //Byter till motståndarens tur

                    Console.WriteLine("Press any key to continue or ESC to close.");
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                    {
                        break;
                    }

                }
                else //om false är det datorns tur
                {
                    while (!shotOk)
                    {
                        int randomRow = randomGenerator.Next(0, 10);
                        int randomCol = randomGenerator.Next(0, 10);

                        Console.WriteLine("Computers turn!");
                        Thread.Sleep(500);
                        Console.WriteLine($"Computer shooting at row: {randomRow} col: {randomCol}");
                        Thread.Sleep(500);
                        shotOk = Shoot(Computer, Human, randomRow, randomCol);
                    }

                    Turn = true;
                    Thread.Sleep(1000);
                }

                Console.Clear();

            }

        }



        static int GetCoordinate(string message)
        {
            int parsedCoordinate = 0;
            bool isNumerical = false;

            while (isNumerical == false || parsedCoordinate < 0 || parsedCoordinate > 9)
            {
                Console.Write(message);
                isNumerical = int.TryParse(Console.ReadLine(), out parsedCoordinate);
            }

            return parsedCoordinate;
        }

        static bool Shoot(Player attacker, Player target, int row, int col)
        {
            int PositionResult = target.GameCanvas.ReceiveShot(row, col); // 0 = hav. > 0 == båtid.

            if (attacker.ShootingLog.ValidateShot(row, col)) //Returnerar true om positionen inte har beskjutits tidigare
            {
                if (PositionResult > 0) //Id från den ruta som träffas, 0 = miss.
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Target hit!");
                    Console.ForegroundColor = ConsoleColor.White;
                    
                    target.GameCanvas.MarkShotOnShip(PositionResult); // lägger till en träff på det fartyg som träffats.

                    attacker.ShootingLog.MarkShot(row, col, true);
                    //TODO lägg till funktion för att ta bort en enhet hälsa från det träffade fartyget i listan ships.
                }
                else
                {
                    attacker.ShootingLog.MarkShot(row, col, false);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Shot missed!");
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


        /*
        public void PrintShipType(int shipid, string Name)
        {
            Computer.PrintShipType(shipid);
        }
        */
	}
}
