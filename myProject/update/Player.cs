using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myProject.gameloop
{
    public class Player
    {
        public int positionPlayerX { get; set; } = 0;
        public int positionPlayerY { get; set; } = 0;
        private int vitesse { get; } = 1;

        public void Render()
        {
            System.Console.WriteLine("Position X : " + this.positionPlayerX + ", Position Y : " + this.positionPlayerY);
        }

        public void Update(string input)
        {
            switch (input)
            {
                case "UP":
                    this.positionPlayerY += this.vitesse;
                    break;
                case "DOWN":
                    this.positionPlayerY -= this.vitesse;
                    break;
                case "LEFT":
                    this.positionPlayerX -= this.vitesse;
                    break;
                case "RIGHT":
                    this.positionPlayerX += this.vitesse;
                    break;
                default:
                    break;
            }
        }
    }
}
