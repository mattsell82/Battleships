using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace sänkaskepp
{
    class Game
    {        
        public IPlayer Computer { get; set; }
        public IPlayer Human { get; set; }
        public bool Turn { get; set; }

        public Game()
        {
            this.Turn = true;
            this.Computer = new Player("Computer", new ShipGrid(), new LogGrid());
            Console.WriteLine("Please enter your name:");
            this.Human = new Player(Console.ReadLine(), new ShipGrid(), new LogGrid());

            Human.ShipGrid.AddShip(new Ship(1, 1));
            Human.ShipGrid.AddShip(new Ship(2, 1));
            Human.ShipGrid.AddShip(new Ship(3, 2));
            Human.ShipGrid.AddShip(new Ship(4, 2));
            Human.ShipGrid.AddShip(new Ship(5, 3));
            Human.ShipGrid.AddShip(new Ship(6, 3));
            Human.ShipGrid.AddShip(new Ship(7, 4));

            Computer.ShipGrid.AddShip(new Ship(1, 1));
            Computer.ShipGrid.AddShip(new Ship(2, 1));
            Computer.ShipGrid.AddShip(new Ship(3, 2));
            Computer.ShipGrid.AddShip(new Ship(4, 2));
            Computer.ShipGrid.AddShip(new Ship(5, 3));
            Computer.ShipGrid.AddShip(new Ship(6, 3));
            Computer.ShipGrid.AddShip(new Ship(7, 4));


        }

        public void PlayGame()
        {
            Random randomGenerator = new Random();
            int maxScore = Human.ShipGrid.GetMaxScore();
            int scoreHuman = 0;
            int scoreComputer = 0;

            Console.Clear();
            while (true)
            {
                scoreHuman = Computer.ShipGrid.GetHits();
                scoreComputer = Human.ShipGrid.GetHits();

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
                Human.ShipGrid.PrintGrid();
                Human.LogGrid.PrintGrid();

                bool shotOk = false;
                if (Turn) //Human = true  
                {
                    while (!shotOk)
                    {
                        Console.WriteLine($"Your turn!");
                        int rowToShoot = GetRow("Enter row: ");
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
                        int[] target = Computer.LogGrid.RandomCoordinate();

                        Console.WriteLine("Computers turn!");
                        Thread.Sleep(100);
                        Console.WriteLine($"Computer shooting at row: {target[0]} col: {target[1]}");
                        Thread.Sleep(100);
                        shotOk = Shoot(Computer, Human, target[0], target[1]);
                    }

                    Turn = true;
                    Thread.Sleep(1000);
                }

                Console.Clear();

            }

        }


        static int GetRow(string message)
        {
            string input = "";

            Dictionary<string, int> RowValues = new Dictionary<string, int>()
            {
            {"A", 0 },
            {"B", 1 },
            {"C", 2 },
            {"D", 3 },
            {"E", 4 },
            {"F", 5 },
            {"G", 6 },
            {"H", 7 },
            {"I", 8 },
            {"J", 9 },
            };

            while (!RowValues.ContainsKey(input))
            {
                Console.Write(message);
                input = Console.ReadLine().ToUpper();
            }
            return RowValues[input];

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

        static bool Shoot(IPlayer attacker, IPlayer target, int row, int col)
        {
            int PositionResult = target.ShipGrid.MarkIncomingShot(row, col); // 0 = hav. > 0 == båtid.

            if (attacker.LogGrid.ValidateShot(row, col)) //Returnerar true om positionen inte har beskjutits tidigare
            {
                if (PositionResult > 0) //ShipId från den ruta som träffas, 0 = miss.
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Target hit!\a");
                    Console.ForegroundColor = ConsoleColor.White;
                    
                    target.ShipGrid.MarkShotOnShip(PositionResult); // lägger till en träff på det fartyg som träffats.

                    attacker.LogGrid.MarkShot(row, col, true);
                    //TODO lägg till funktion för att ta bort en enhet hälsa från det träffade fartyget i listan ships.
                }
                else
                {
                    attacker.LogGrid.MarkShot(row, col, false);
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
