using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;

namespace sänkaskepp
{
    public interface IGrids
    {
        void PrintGrid(IGraphics gui);
    }

    public abstract class Grids : IGrids //Denna klass är abstrakt och ärvs av klasserna ShipGrid och LogGrid
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


        public void PrintGrid(IGraphics colorWriter) //Skriver ut spelplanen och koordinaterna
        {
            Console.Write($"\t{" ", 2}");
            for (int i = 0; i < AxisX.Length; i++)
            {
                colorWriter.PrintColorString("green", $"{AxisX[i], 3}");
            }
            Console.Write("\n");

            for (int row = 0; row < Grid.GetLength(0); row++)
            {
                colorWriter.PrintColorString("green", $"\t{AxisY[row], 2}");

                for (int col = 0; col < Grid.GetLength(1); col++)
                {
                    colorWriter.PrintColorGrid(Grid[row, col], 3);
                }
                Console.WriteLine(string.Empty);

            }
        }
    }
}
