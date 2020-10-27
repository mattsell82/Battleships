using System;
using System.Collections.Generic;
using System.Text;

namespace sänkaskepp
{
    public class Player : IPlayer
    {
        public string Name { get; set; }
        public IShipGrid ShipGrid { get; set; }
        public ILogGrid LogGrid { get; set; }

        public Player(string name, IShipGrid shipGrid, ILogGrid logGrid)
        {
            this.Name = name;
            this.ShipGrid = shipGrid;
            this.LogGrid = logGrid;

        }
    }
}
