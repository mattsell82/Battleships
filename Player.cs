using System;
using System.Collections.Generic;
using System.Text;

namespace sänkaskepp
{
    public class Player
    {
        public string Name { get; set; }
        public GameCanvas GameCanvas { get; set; }
        public ShootingLog ShootingLog { get; set; }

        public Player(string name)
        {
            this.Name = name;
            this.GameCanvas = new GameCanvas();
            this.ShootingLog = new ShootingLog();

            GameCanvas.AddShip(1, 1);
            GameCanvas.AddShip(1, 2);
            GameCanvas.AddShip(2, 3);
            GameCanvas.AddShip(2, 4);
            GameCanvas.AddShip(3, 5);
            GameCanvas.AddShip(3, 6);
            GameCanvas.AddShip(4, 7);
        }
        /*
        public void PrintShipType(int shipid)
        {
            Console.WriteLine(GameCanvas.Ships[shipid].GetShipType());
        }

        public GameCanvas GetCanvas()
        {
            return this.GameCanvas;
        }
        */
    }
}
