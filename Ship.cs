using System;
using System.Collections.Generic;
using System.Text;

namespace sänkaskepp
{
    public class Ship
    {
        public int Id;
        public int ShipLength;
        public string ShipType;

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

        public int GetShipId()
        {
            return Id;
        }
    }
}
