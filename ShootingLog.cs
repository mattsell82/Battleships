using System;
using System.Collections.Generic;
using System.Text;

namespace sänkaskepp
{
    class ShootingLog
    {
        int[] CoordinatesX { get; set; }
        string[] CoordinatesY { get; set; }
        int[,] Canvas { get; set; }

        public ShootingLog()
        {
            this.CoordinatesX = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            this.CoordinatesY = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            this.Canvas = new int[,]
            {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            };
        }

        public bool ValidateShot(int row, int col)
        {
            if (Canvas[row, col] > 0)
            {
                return false;
            }
            else
            {

                return true;
            }
        }

        public void MarkShot(int row, int col, bool status) //Om status = true markeras skottet som träff (2), annars som miss (1).
        {
            if (status == true)
            {
                Canvas[row, col] = 2;
            }
            else
            {
                Canvas[row, col] = 1;
            }
        }

        public void PrintCanvas()
        {
            //Console.Clear();
            Console.Write("  ");
            for (int i = 0; i < CoordinatesX.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{CoordinatesX[i],3}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine(string.Empty);

            for (int row = 0; row < Canvas.GetLength(0); row++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{CoordinatesY[row],3}");
                Console.ForegroundColor = ConsoleColor.White;

                for (int col = 0; col < Canvas.GetLength(1); col++)
                {
                    if (Canvas[row, col] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"{Canvas[row, col],2} ");
                    }
                    else if (Canvas[row, col] == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{Canvas[row, col],2} ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write($"{Canvas[row, col],2} ");
                    }
                    Console.ForegroundColor = ConsoleColor.White;

                }
                Console.WriteLine(string.Empty);

            }



        }

    }
}
