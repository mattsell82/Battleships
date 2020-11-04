using System;
using System.Collections.Generic;
using System.Text;

namespace sänkaskepp

{
    public interface IPlayer
    {
        ILogGrid LogGrid { get; }
        string Name { get; }
        IShipGrid ShipGrid { get; }
    }
    public class Player : IPlayer
    {
        public string Name { get; }
        public IShipGrid ShipGrid { get; set; }
        public ILogGrid LogGrid { get; set; }

        public Player(string name, IShipGrid shipGrid, ILogGrid logGrid) //Objekten injiceras i samband med att Player instansieras i klassen Game.
        {
            this.Name = name;
            this.ShipGrid = shipGrid;
            this.LogGrid = logGrid;

            if (String.IsNullOrWhiteSpace(name) || String.IsNullOrEmpty(name)) //Om spelaren utelämnat namn sätts det till Unknown.
            {
                this.Name = "Unknown";
            }
        }
    }
}
