using System;
using System.Collections.Generic;
using System.Text;

namespace sänkaskepp
{
    public interface IShip
    {
        int Hits { get; set; }
        int ShipId { get;  }
        int ShipLength { get; }
        string ShipType { get; }
    }

    public class Ship : IShip
    {
        public int ShipId { get; }
        public int ShipLength { get; }
        public string ShipType { get; }
        public int Hits { get; set; }

        public Ship(int id, int shipLength)
        {
            this.Hits = 0;
            this.ShipId = id;
            this.ShipLength = shipLength;
            this.ShipType = GetShipType(shipLength);
        }

        public string GetShipType(int shipLength) //Denna metod sätter vilken typ av skepp det är baserat på båtens längd.
        {
            List<string> shipTypes = new List<string>()
            {
                { "Submarine" },
                { "Torpedo boat" },
                { "Destroyer" },
                { "Aircraft carrier" }
            };

            try
            {
                return shipTypes[shipLength - 1];
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Invalid ship length, ship type set to Unknown.\nMore info: " + ex.Message);
                return "Unknown";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unknown error, ship type set to Unknown.\nMore info: " + ex.Message);
                return "Unknown";
            }
        }
    }
}
