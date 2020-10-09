using System;
using System.Collections.Generic;
using System.Text;

namespace sänkaskepp
{
    public class Ship
    {
        public int Id { get; set; }
        public int ShipLength { get; set; }
        public string ShipType { get; set; }

        public Ship(int id, int shipLength)
        {
            this.Id = id;
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

        /*
        public string GetShipType()
        {
            return ShipType;
        }
        */
    }
}
