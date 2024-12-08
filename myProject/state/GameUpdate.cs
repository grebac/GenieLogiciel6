using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace myProject.gameloop
{
    public class Game_State
    {
        public ConsoleKey _lastInput {  get; set; }
        const int MS_PER_UPDATE = 16;
        PlayerState player;
        EnemyState enemy;

        List<StateMachine> states = new List<StateMachine>();

        public Game_State() {
            player = new PlayerState(0, 0);
            enemy = new EnemyState(5, 10, player);

            states.Add(player.StateMachine);
            states.Add(enemy.StateMachine);
        }
        

        public void Run()
        {
            while (true) {
                this.ProcessInput();

                foreach(var state in states)
                {
                    state.HandleInput(_lastInput);
                }
                _lastInput = ConsoleKey.Sleep;

                foreach(var state in states)
                {
                    state.Update(GetCurrentTime());
                }

                Console.WriteLine("Fin de frame\n");
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
            _lastInput = Console.ReadKey(true).Key;
        }        
    }
}
