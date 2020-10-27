using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace sänkaskepp
{

    public abstract class Grids : IGrids
    {
        int[] AxisX { get; }
        string[] AxisY { get; }

        public int[,] Grid { get; set; }
        public Random Randomizer { get; set; }


        public Grids()
        {
            this.AxisX = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            this.AxisY = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            this.Grid = new int[10, 10];
            this.Randomizer = new Random();
        }

        public void PrintGrid() //Skriver ut spelplanen och koordinaterna
        {
            Console.Write("  ");
            for (int i = 0; i < AxisX.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{AxisX[i],3}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine(string.Empty);

            for (int row = 0; row < Grid.GetLength(0); row++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{AxisY[row],3}");
                Console.ForegroundColor = ConsoleColor.White;

                for (int col = 0; col < Grid.GetLength(1); col++)
                {
                    if (Grid[row, col] == 0 || Grid[row, col] == 9)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"{Grid[row, col],2} ");
                    }
                    else if (Grid[row, col] == 1 || Grid[row, col] == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{Grid[row, col],2} ");
                    }
                    else if (Grid[row, col] == 3 || Grid[row, col] == 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"{Grid[row, col],2} ");
                    }
                    else if (Grid[row, col] == 5 || Grid[row, col] == 6)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"{Grid[row, col],2} ");
                    }
                    else if (Grid[row, col] == 8)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write($"{Grid[row, col],2} ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write($"{Grid[row, col],2} ");
                    }
                    Console.ForegroundColor = ConsoleColor.White;

                }
                Console.WriteLine(string.Empty);

            }



        }



    }
}
