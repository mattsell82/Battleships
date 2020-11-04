using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;

namespace sänkaskepp
{
    class Game
    {        
        public IPlayer Computer { get; }
        public IPlayer Human { get; }
        public bool Turn { get; set; }
        IGraphics ColorWriter { get; }

        public Game(IGraphics colorWriter)
        {
            this.ColorWriter = colorWriter;
            this.Turn = true;
            this.Computer = new Player("Computer", new ShipGrid(), new LogGrid()); //instantierar datorspelaren och dess grids
            Console.Write("\tPlease enter your name: ");
            this.Human = new Player(Console.ReadLine(), new ShipGrid(), new LogGrid()); //instantierar den mänskliga spelaren och dess grids
            InstantiateShips();

        }

        public void InstantiateShips() //Lägger till alla skepp
        {
            int[] ships = { 2, 2, 2, 1, }; //Arrayens index + 1 = båtens längd. Värdena i arrayen = antal båtar av en viss längd.
            int id = 1;
            for (int i = 0; i < ships.Length; i++)
            {
                for (int j = 0; j < ships[i]; j++)
                {
                    Human.ShipGrid.AddShip(new Ship(id, i + 1));
                    Computer.ShipGrid.AddShip(new Ship(id, i + 1));
                    id++;
                }
            }
        }

        public void PlayGame() //Denna metod sköter allt game-play. Dvs. poängräkning, turordning osv.
        {
            int maxScoreHuman = Computer.ShipGrid.GetMaxScore();
            int maxScoreComputer = Human.ShipGrid.GetMaxScore();
            int scoreHuman = 0;
            int scoreComputer = 0;

            Console.Clear();
            while (true)  //Game loop, körs tills någon vinner eller tills användaren avslutar
            {
                scoreHuman = Computer.ShipGrid.GetHitsTotal();
                scoreComputer = Human.ShipGrid.GetHitsTotal();

                if (scoreHuman == maxScoreHuman) //Kollar om den mänskliga spelaren har vunnit
                {
                    ColorWriter.PrintVictoryGraphic();
                    Console.WriteLine("n\tYour enemy has been defeated!\n");
                    Console.ReadKey();
                    break;
                }
                else if (scoreComputer == maxScoreHuman) //Kollar om dator-spelaren har vunnit
                {
                    ColorWriter.PrintDefeatGraphic();
                    Console.WriteLine("\n\tYou have been defeated!");
                    Console.ReadKey();
                    break;
                }

                //Skriver ut poäng
                Console.WriteLine("\t----------------------");
                Console.WriteLine($"\t{Human.Name + " score:",-25}{scoreHuman} / {maxScoreHuman}");
                Console.WriteLine($"\t{"Computers score:",-25}{scoreComputer} / {maxScoreComputer}");
                Console.WriteLine("\t----------------------");

                //Skriver ut spelplanen
                Console.WriteLine("\tYour ships: ");
                Human.ShipGrid.PrintGrid(ColorWriter);
                Console.WriteLine("\n\tYour shots: ");
                Human.LogGrid.PrintGrid(ColorWriter);
                

                //Påbörjar eldgivning
                bool shotOk = false;
                if (Turn) //Om Turn = true är det människans tur  
                {
                    while (!shotOk) //Upprepas tills att avändaren skjuter ett giltigt skott
                    {
                        Console.WriteLine($"\tYour turn!"); 
                        int rowToShoot = GetRowInput("\tEnter row: ");
                        int colToShoot = GetColInput("\tEnter col: ");

                        shotOk = Shoot(Human, Computer, rowToShoot, colToShoot);
                    }
                    Turn = false;  //Byter till motståndarens tur

                    Console.Write("\tPress enter to continue or -q to quit or -s for status: ");
                    var command = Console.ReadLine().ToLower();

                    //Möjlighet att avbryta spelet eller visa status
                    if (command == "-q")  //Går tillbaka till huvudmenyn
                    {
                        break;
                    }
                    else if (command == "-s") //Visar båtarnas status
                    {
                        Console.Clear();
                        Human.ShipGrid.PrintStatus(Human.Name);
                        Console.WriteLine();
                        Computer.ShipGrid.PrintStatus(Computer.Name);
                        Console.ReadKey();
                    }

                }
                else //Om Turn = false är det datorns tur
                {
                    while (!shotOk)
                    {
                        int[] target = Computer.LogGrid.RandomCoordinate(); //Detta är den slumpade positionen som datorn skjuter.

                        Console.WriteLine("\tComputers turn!");
                        Thread.Sleep(100);
                        Console.WriteLine($"\tComputer shooting at row: {target[0]} col: {target[1]}");
                        Thread.Sleep(100);
                        shotOk = Shoot(Computer, Human, target[0], target[1]);
                    }

                    Turn = true;
                    Thread.Sleep(1000);
                }

                Console.Clear();
            }
        }

        static int GetRowInput(string message) //Tar emot vilken rad som ska beskjutas
        {
            string input = "";

            Dictionary<string, int> rowValues = new Dictionary<string, int>()
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

            while (!rowValues.ContainsKey(input))
            {
                Console.Write(message);
                input = Console.ReadLine().ToUpper();
            }

            return rowValues[input];
        }

        static int GetColInput(string message) //Tar emot vilken kolumn som ska beskjutas
        {
            int parsedCoordinate = -1;
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
            
            if (attacker.LogGrid.ValidateShot(row, col)) //Om positionen inte beskjutits tidigare
            {
                int PositionResult = target.ShipGrid.MarkIncomingShot(row, col); //Lagrar resultatet från skottet

                if (PositionResult > 0) //Om en båt träffades
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\tTarget hit!\a");
                    Console.ForegroundColor = ConsoleColor.White;
                    
                    target.ShipGrid.AddHitToShip(PositionResult); //Lägger till en träff på det fartyg som träffats.

                    attacker.LogGrid.LogShot(row, col, true); //Markerar skottet i LogGrid
                }
                else //Om skottet missade
                {
                    attacker.LogGrid.LogShot(row, col, false);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tShot missed!");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                return true; //returnerar true om det gick att skjuta
            }
            else //Om positionen redan har beskjutits
            {
                Console.WriteLine("\tThis position has already been shot");
                return false; //returnerar false om det inte gick att skjuta
            }
        } //Skjuter ett skott

    }
}
