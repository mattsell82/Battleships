using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace sänkaskepp
{
    public class GameCanvas
    {
        int[] CoordinatesX { get; set; }
        string[] CoordinatesY { get; set; }
        public List<Ship> Ships { get; set; }
        public int[,] Canvas { get; set; }


        public GameCanvas()
        {
            this.Ships = new List<Ship>();
            this.CoordinatesX = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            this.CoordinatesY = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            this.Canvas = new int[10, 10];         
        }

        //FindPosition används av AddShip för att hitta en ledig båtplacering
        public int[] FindPosition(int shipLength, int shipOrientation, int shipId)
        {
            
            //beräknar koordinater baserat på båtens längd och shipOrientation (0 = horisontell / 1 = vertikal)
            Random randomGenerator = new Random();
            int row;
            int col;

            if (shipOrientation == 0)
            {
                row = randomGenerator.Next(10);
                col = randomGenerator.Next(11 - shipLength);
            }
            else
            {
                row = randomGenerator.Next(11 - shipLength);
                col = randomGenerator.Next(10);
            }


            //Testar om position är ledig (horisontellt båt)
            int sumOfPositions = 0;

            if (shipOrientation == 0)
            {
                for (int i = 0; i < shipLength; i++)
                {
                    Debug.WriteLine($"checking: {Canvas[row, col + i]}");
                    sumOfPositions += Canvas[row, col + i];
                }

                //Om positionen är ledig returneras positionen
                Debug.WriteLine("sum of positions:" + sumOfPositions);
                if (sumOfPositions == 0)
                {
                    Debug.WriteLine($"Position Accepted for row: {row} Col: {col} Längd: {shipLength} Orientation: {shipOrientation} ShipId: {shipId}");
                    return new int[] { row, col };
                }
                //Om positionen är upptagen testas en ny position rekursivt
                else
                {
                    Debug.WriteLine("position rejected");
                    return FindPosition(shipLength, shipOrientation, shipId);
                }
            }
            //Testar om position är ledig (vertikal båt)
            else
            {
                for (int i = 0; i < shipLength; i++)
                {
                    Debug.WriteLine($"checking: {Canvas[row + i, col]}");
                    sumOfPositions += Canvas[row + i, col];
                }

                Debug.WriteLine("sum of positions:" + sumOfPositions);
                //Om positionen är ledig returneras positionen
                if (sumOfPositions == 0)
                {
                    Debug.WriteLine($"Position accepted for row: {row} Col: {col} Längd: {shipLength} Orientation: {shipOrientation} ShipId: {shipId}");
                    return new int[] { row, col };
                }
                //Om positionen är upptagen testas en ny position rekursivt
                else
                {
                    Debug.WriteLine("position rejected");
                    return FindPosition(shipLength, shipOrientation, shipId);
                }
            }

        }

        //AddShip används för att lägga till båtobjekt i listan Ships sa
        public void AddShip(int shipLength, int shipId)
        {
            Debug.WriteLine($"-------Adding ShipId {shipId} with length {shipLength}------------");

            //Lägger till båtobjektet i listan Ships
            this.Ships.Add(new Ship(shipLength, shipId));

            //slumpar fram shipOrientation. 0 = horisontellt dvs. liggande båt, 1 = vertikalt dvs. stående båt
            Random randomGenerator = new Random();
            int shipOrientation = randomGenerator.Next(2);

            int[] pos = FindPosition(shipLength, shipOrientation, shipId);

            //gameCanvas[pos[0], pos[1]] = 1; Behövs verkligen denna?


            //lägg till en båt som  ligger horisontellt
            if (shipOrientation == 0)
            {
                for (int i = 0; i < shipLength; i++)
                {
                    Canvas[pos[0], pos[1] + i] = shipId;
                    Debug.WriteLine($"Added {shipLength} at row: {pos[0]} Col: {pos[1]+1} With orientation: {shipOrientation} ShipId: {shipId}");
                }
            }
            //annars lägg till en båt som står vertikalt
            else
            {
                for (int i = 0; i < shipLength; i++)
                {
                    Canvas[pos[0] + i, pos[1]] = shipId;
                    Debug.WriteLine($"Added {shipLength} at row: {pos[0]+i} Col: {pos[1]} With orientation: {shipOrientation} ShipId: {shipId}");
                }
            }

        }

        public void PrintCanvas()
        {
            Console.Write("  ");
            for (int i = 0; i < CoordinatesX.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{CoordinatesX[i], 3}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine(string.Empty);


            for (int row = 0; row < Canvas.GetLength(0); row++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{CoordinatesY[row],3}");
                Console.ForegroundColor = ConsoleColor.White;

                for (int col = 0; col <Canvas.GetLength(1); col++)
                {
                    if (Canvas[row, col] == 0 || Canvas[row, col] == 9 )
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"{Canvas[row, col],2} ");
                    }
                    else if (Canvas[row, col] == 1 || Canvas[row, col] == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{Canvas[row, col],2} ");
                    }
                    else if (Canvas[row, col] == 3 || Canvas[row, col] == 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"{Canvas[row, col],2} ");
                    }
                    else if (Canvas[row, col] == 5 || Canvas[row, col] == 6)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"{Canvas[row, col],2} ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write($"{Canvas[row, col],2} ");
                    }
                    Console.ForegroundColor = ConsoleColor.White;

                }
                Console.WriteLine(string.Empty);

            }



        }

        public int GetScore()
        {
            int score = 0;
            for (int row = 0; row < Canvas.GetLength(0); row++)
            {
                for (int col = 0; col < Canvas.GetLength(1); col++)
                {
                    if (Canvas[row, col] != 0 && Canvas[row, col] != 9)
                    {
                        score += 1;
                    }
                }
            }

            return score;
        }

        public int ReceiveShot(int row, int col)
        {
            int result = this.Canvas[row, col];
            if (result != 0)
            {
                this.Canvas[row, col] = 9; //Markera med 9 om det var träff
            }

            return result; //Returnerar id på det skepp som träffades
        }
    }
}
