using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace sänkaskepp
{
    class ShipGrid : Grids, IShipGrid
    {
        public List<IShip> Ships { get; set; }

        public ShipGrid()
        {
            this.Ships = new List<IShip>();
        }

        //FindPosition används av AddShip för att hitta en ledig båtplacering
        public int[] FindPosition(int shipLength, int shipOrientation, int shipId)
        {

            //beräknar koordinater baserat på båtens längd och shipOrientation (0 = horisontell / 1 = vertikal)
            int row;
            int col;

            if (shipOrientation == 0)
            {
                row = Randomizer.Next(10);
                col = Randomizer.Next(11 - shipLength);
            }
            else
            {
                row = Randomizer.Next(11 - shipLength);
                col = Randomizer.Next(10);
            }


            //Testar om position är ledig (horisontellt båt)
            int sumOfPositions = 0;

            if (shipOrientation == 0)
            {
                for (int i = 0; i < shipLength; i++)
                {
                    Debug.WriteLine($"checking: {Grid[row, col + i]}");
                    sumOfPositions += Grid[row, col + i];
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
                    Debug.WriteLine($"checking: {Grid[row + i, col]}");
                    sumOfPositions += Grid[row + i, col];
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

        //AddShip används för att lägga till objekt av klassen Ship i listan Ships
        public void AddShip(IShip ship)
        {
            Debug.WriteLine($"-------Adding ShipId {ship.ShipId} with length {ship.ShipLength}------------");

            //Lägger till båtobjektet i listan Ships
            this.Ships.Add(ship);

            //slumpar fram shipOrientation. 0 = horisontellt dvs. liggande båt, 1 = vertikalt dvs. stående båt
            int shipOrientation = Randomizer.Next(2);

            int[] pos = FindPosition(ship.ShipLength, shipOrientation, ship.ShipId);

            //gameCanvas[pos[0], pos[1]] = 1; Behövs verkligen denna?


            //lägg till en båt som  ligger horisontellt
            if (shipOrientation == 0)
            {
                for (int i = 0; i < ship.ShipLength; i++)
                {
                    Grid[pos[0], pos[1] + i] = ship.ShipId;
                    Debug.WriteLine($"Added {ship.ShipLength} at row: {pos[0]} Col: {pos[1] + 1} With orientation: {shipOrientation} ShipId: {ship.ShipId}");
                }
            }
            //annars lägg till en båt som står vertikalt
            else
            {
                for (int i = 0; i < ship.ShipLength; i++)
                {
                    Grid[pos[0] + i, pos[1]] = ship.ShipId;
                    Debug.WriteLine($"Added {ship.ShipLength} at row: {pos[0] + i} Col: {pos[1]} With orientation: {shipOrientation} ShipId: {ship.ShipId}");
                }
            }

        }

        public int GetScore() //summerar hur många rutor med båtar som finns kvar på planen
        {
            int score = 0;
            for (int row = 0; row < Grid.GetLength(0); row++)
            {
                for (int col = 0; col < Grid.GetLength(1); col++)
                {
                    if (Grid[row, col] != 0 && Grid[row, col] != 9)
                    {
                        score += 1;
                    }
                }
            }

            return score;
        }

        public int GetMaxScore() // Returnerar maxpoäng baserat på hur mång båtar som finns i listan.
        {
            int maxScore = 0;
            for (int i = 0; i < Ships.Count; i++)
            {
                maxScore += Ships[i].ShipLength;
            }
            return maxScore;
        }

        public int GetHits()
        {
            int totalHits = 0;
            for (int i = 0; i < Ships.Count; i++)
            {
                totalHits += Ships[i].Hits;
            }
            return totalHits;

        }


        public int MarkIncomingShot(int row, int col) //Markerar träff i Grid
        {
            int result = this.Grid[row, col];
            if (result != 0)
            {
                this.Grid[row, col] = 9; //Markera med 9 om det var träff
            }

            return result; //Returnerar id på det skepp som träffades
        }


        public void MarkShotOnShip(int shipId) //TODO Göra om Listan ships till en dictionary istället??
        {
            for (int i = 0; i < Ships.Count; i++)
            {

                if (Ships[i].ShipId == shipId)
                {

                    Ships[i].Hits++;

                    //Console.WriteLine($"A { Ships[i].ShipType} was hit!");

                    if (Ships[i].Hits == Ships[i].ShipLength)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"A {Ships[i].ShipType} was destroyed!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                }
            }
        }
    }
}
