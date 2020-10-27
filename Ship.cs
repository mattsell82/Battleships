using System;
using System.Collections.Generic;
using System.Text;

namespace sänkaskepp
{
    public class Ship : IShip
    {
        public int ShipId { get; set; }
        public int ShipLength { get; set; }
        public string ShipType { get; set; }
        public int Hits { get; set; }

        public Ship(int id, int shipLength)
        {
            this.Hits = 0;
            this.ShipId = id;
            this.ShipLength = shipLength;

            if (shipLength == 1)
            {
                ShipType = "Ubåt";
            }
            else if (shipLength == 2)
            {
                ShipType = "Torpedbåt";
            }
            else if (shipLength == 3)
            {
                ShipType = "Jagare";
            }
            else
            {
                ShipType = "Hangarfartyg";
            }
        }

    }
}
