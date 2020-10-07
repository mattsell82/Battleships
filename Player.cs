using System;
using System.Collections.Generic;
using System.Text;

namespace sänkaskepp
{
    class Player
    {
        public  string Name { get; set; }

        public GameCanvas GameCanvas { get; set; }
        public ShootingLog ShootingLog { get; set; }

        public Player(string name)
        {
            this.Name = name;
            this.GameCanvas = new GameCanvas();
            this.ShootingLog = new ShootingLog();

        }
    }
}
