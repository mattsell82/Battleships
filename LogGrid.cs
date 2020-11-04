using System;
using System.Collections.Generic;
using System.Text;

namespace sänkaskepp
{
    public interface ILogGrid : IGrids
    {
        void LogShot(int row, int col, bool status);
        int[] RandomCoordinate(); 
        bool ValidateShot(int row, int col);
    }

    public class LogGrid : Grids, ILogGrid //LogGrid används för att hålla reda på de skott man själv skjuter
    {
        public List<int[]> Coordinates { get; } //Datorspelarens slumpade skott plockas från denna lista

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

        public void LogShot(int row, int col, bool status) //Markerar skottet i grid
        {
            if (status == true)
            {
                Grid[row, col] = 8; //Träff markeras med 8
            }
            else
            {
                Grid[row, col] = 1; //Miss markeras med 1
            }
        }
    }
}
