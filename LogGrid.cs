using System;
using System.Collections.Generic;
using System.Text;

namespace sänkaskepp
{
    public class LogGrid : Grids, ILogGrid
    {

        public List<int[]> Coordinates { get; set; }

        public LogGrid()
        {
            this.Coordinates = new List<int[]>();
            AddCoordinates();
        }

        void AddCoordinates() //Lägger till alla koordinater i listan.
        {
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    Coordinates.Add(new int[2] { row, col });
                }
            }
        }

        public int[] RandomCoordinate() //Används av datorns AI för att slumpa fram nästa skott
        {
            int limit = Coordinates.Count;
            if (limit > 0)
            {
                int randomIndex = Randomizer.Next(0, limit);
                int[] output = Coordinates[randomIndex];
                Coordinates.RemoveAt(randomIndex);
                return output;
            }
            else
            {
                return new int[] { -1, -1 };
            }
        }


        public bool ValidateShot(int row, int col) //kollar om positionen redan har beskjutits
        {
            if (Grid[row, col] > 0)
            {
                return false;  //returnerar false om positionen redan har beskjutits
            }
            else
            {
                return true; //returnerar true om positionen inte beskjutits tidigare
            }
        }

        public void MarkShot(int row, int col, bool status) //Markerar skottet i grid
        {
            if (status == true)
            {
                Grid[row, col] = 8; //Träff markeras med 2
            }
            else
            {
                Grid[row, col] = 1; //Miss markeras med 1
            }
        }
    }
}
