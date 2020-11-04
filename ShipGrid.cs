using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Diagnostics.Tracing;

namespace sänkaskepp
{
    public interface IShipGrid : IGrids
    {
        void AddShip(IShip ship);
        int GetHitsTotal();
        int GetMaxScore();
        int MarkIncomingShot(int row, int col);
        void AddHitToShip(int shipId);
        void PrintStatus(string name);
    }
    class ShipGrid : Grids, IShipGrid //I ShipGrid ritas alla ens båtar
    {
        IList<IShip> Ships { get; }
        int MaxShipQty { get; }
        int RecursionLimit { get; }

        public ShipGrid()
        {
            this.Ships = new List<IShip>();
            this.MaxShipQty = 7;
            this.RecursionLimit = 30;
        }

        private int[] FindPosition(int shipLength, int shipOrientation, int shipId, int counter) //FindPosition används av AddShip för att hitta en ledig båtplacering
        {
            
            int recursionCounter = counter; //Avbryter om antalet försök övertiger gränsvärdet
            Debug.WriteLine($"Recursion counter: {recursionCounter}");
            if (recursionCounter > RecursionLimit)
            {
                return new int[] { -1, -1 };
            }

            //Slumpar koordinaten som ska testas, koordinaten avser båtens översta vänstra hörn.
            int row;
            int col;

            //Intervallet som det slumpas inom beror på vilken orientering båten har, dett för att den inte ska ska hamna utanför planen.
            if (shipOrientation == 0) //Vid horisontell båt
            {
                row = Randomizer.Next(10);
                col = Randomizer.Next(11 - shipLength);
            } 
            else //Vid vertikal båt
            {
                row = Randomizer.Next(11 - shipLength);
                col = Randomizer.Next(10);
            }
            
            int sumOfPositions = 0;

            if (shipOrientation == 0) //Testar om position är ledig (horisontellt båt)
            {
                for (int i = 0; i < shipLength; i++)
                {
                    Debug.WriteLine($"checking: {Grid[row, col + i]}");
                    sumOfPositions += Grid[row, col + i];
                }
            }
            else  //Testar om position är ledig (vertikal båt)
            {
                for (int i = 0; i < shipLength; i++)
                {
                    Debug.WriteLine($"checking: {Grid[row + i, col]}");
                    sumOfPositions += Grid[row + i, col];
                }
            }
            Debug.WriteLine("sum of positions:" + sumOfPositions);
            
            if (sumOfPositions == 0) //Om positionen är ledig returneras positionen
            {
                Debug.WriteLine($"Position accepted for row: {row} Col: {col} Längd: {shipLength} Orientation: {shipOrientation} ShipId: {shipId}");
                return new int[] { row, col };
            }
            else //Om positionen är upptagen testas en ny position rekursivt
            {
                try
                {
                    Debug.WriteLine("Position rejected");
                    return FindPosition(shipLength, shipOrientation, shipId, recursionCounter + 1); //Det rekursiva anropet
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Recursion error.\nMer info: " + ex.Message);
                    return new int[] { -1, -1 }; //Om något gått fel returneras -1, -1 
                }
            }
        }
        
        public void AddShip(IShip ship) //AddShip används för att lägga till och placera båtar
        {
            Debug.WriteLine($"-------Adding ShipId {ship.ShipId} with length {ship.ShipLength}------------");

            if (Ships.Count == MaxShipQty) //Om antalet båtar överstiger maxvärdet
            {
                Console.WriteLine($"Ship not added, too many ships! ShipId: {ship.ShipId} ShipLength: {ship.ShipLength}");
                Console.ReadKey();
            }
            else
            {
                //slumpar fram båtens orientering. 0 = horisontellt, 1 = vertikalt.
                int shipOrientation = Randomizer.Next(2);

                int[] position = FindPosition(ship.ShipLength, shipOrientation, ship.ShipId, 0);  //Söker ledig position

                if (position[0] != -1 || position[1] != -1) //Om en ledig position hittades
                {
                    //Lägger till båtobjektet i listan Ships
                    this.Ships.Add(ship);

                    for (int i = 0; i < ship.ShipLength; i++)
                    {
                        if (shipOrientation == 0) //Ritar horisontell båt i grid
                        {
                            Grid[position[0], position[1] + i] = ship.ShipId;
                            Debug.WriteLine($"Added {ship.ShipLength} at row: {position[0]} Col: {position[1] + 1} With orientation: {shipOrientation} ShipId: {ship.ShipId}");
                        }
                        else //Ritar vertikal båt i grid
                        {
                            Grid[position[0] + i, position[1]] = ship.ShipId;
                            Debug.WriteLine($"Added {ship.ShipLength} at row: {position[0] + i} Col: {position[1]} With orientation: {shipOrientation} ShipId: {ship.ShipId}");
                        }
                    }
                }
                else //Om ingen ledig position hittades
                {
                    Console.WriteLine("Ship not added, no available position found!");
                }
            }
        }
        
        public int GetMaxScore() //Returnerar maxpoäng baserat på hur mång båtar som finns i listan.
        {
            int maxScore = 0;
            for (int i = 0; i < Ships.Count; i++)
            {
                maxScore += Ships[i].ShipLength;
            }
            return maxScore;
        }

        public int GetHitsTotal() //Returnerar totala antalet träffar
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

        public void AddHitToShip(int shipId) //Adderar en träff till den båt som träffades.
        {
            for (int i = 0; i < Ships.Count; i++)
            {
                
                if (Ships[i].ShipId == shipId)
                {
                    Ships[i].Hits++;

                    if (Ships[i].Hits == Ships[i].ShipLength)  //Meddelar om båten sänks.
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"\tA {Ships[i].ShipType} was destroyed!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                }
            }
        }

        public void PrintStatus(string name) //Skriver ut listan med hur många träffar respektive båt har
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\t{name.ToUpper() + " SHIPS",-20}HITS:");
            Console.ForegroundColor = ConsoleColor.White;

            foreach (var ship in Ships)
            {
                Console.WriteLine($"\t{ship.ShipType, -20}{ship.Hits}/{ship.ShipLength}");
            }
        }
    }
}
