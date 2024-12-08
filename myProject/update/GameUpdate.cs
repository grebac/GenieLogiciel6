using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace myProject.gameloop
{
    public class Game_Update
    {
        public string _lastInput {  get; set; }
        const int MS_PER_UPDATE = 16;
        Player player = new Player();

        public Game_Update() { }
        

        public void Run()
        {
            double lag = 0.0;
            double last_time = GetCurrentTime();
            while (true) {
                int current_time = GetCurrentTime();
                double elapsed_time = current_time - last_time;

                lag += elapsed_time;

                this.ProcessInput();

                while (lag >= MS_PER_UPDATE) // on pourrait mettre une limite
                {
                    player.Update(this._lastInput); // MS_PER_UPDATE doit être plus grand que le temps nécessaire à Update même sur les machines lentes
                    this._lastInput = "INVALID";
                    lag -= MS_PER_UPDATE;
                }

                player.Render();

                last_time = current_time;
            }
        }

        private int GetCurrentTime()
        {
            return (int)DateTime.Now.TimeOfDay.TotalMilliseconds;
        }

        private void Sleep(int millisecondsTimeout)
        {
            if(millisecondsTimeout > 0)
                Thread.Sleep(millisecondsTimeout);
        }

        public void ProcessInput() {
            var input = Console.ReadKey(true).Key;
            switch (input) {
                case ConsoleKey.Z:
                    this._lastInput = "UP";
                    break;
                case ConsoleKey.Q:
                    this._lastInput = "LEFT";
                    break;
                case ConsoleKey.S:
                    this._lastInput = "DOWN";
                    break;
                case ConsoleKey.D:
                    this._lastInput = "RIGHT";
                    break;
                default:
                    this._lastInput = "INVALID";
                    break;
            }
        }        
    }
}
