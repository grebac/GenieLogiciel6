using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myProject.gameloop
{
    public class PlayerState
    {
        public StateMachine StateMachine { get; } = new StateMachine();
        public int X { get; private set; }
        public int Y { get; private set; }

        public PlayerState(int startX, int startY)
        {
            X = startX;
            Y = startY;

            // Initialisation des états
            var idleState = new PlayerIdleState(this);
            StateMachine.SetInitialState(idleState);
        }

        public void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }
    }

    // États du joueur
    public class PlayerIdleState : IState
    {
        private readonly PlayerState _player;

        public PlayerIdleState(PlayerState player)
        {
            _player = player;
        }

        public void Enter() => Console.WriteLine("Player is now Idle.");
        public void HandleInput(ConsoleKey input)
        {
            if (input == ConsoleKey.Z) _player.StateMachine.ChangeState(new PlayerMovingState(_player, 0, -1));
            else if (input == ConsoleKey.S) _player.StateMachine.ChangeState(new PlayerMovingState(_player, 0, 1));
            else if (input == ConsoleKey.Q) _player.StateMachine.ChangeState(new PlayerMovingState(_player, -1, 0));
            else if (input == ConsoleKey.D) _player.StateMachine.ChangeState(new PlayerMovingState(_player, 1, 0));
        }
        public void Update(double deltaTime) { }
        public void Exit() { }
    }

    public class PlayerMovingState : IState
    {
        private readonly PlayerState _player;
        private readonly int _dx, _dy;

        public PlayerMovingState(PlayerState player, int dx, int dy)
        {
            _player = player;
            _dx = dx;
            _dy = dy;
        }

        public void Enter()
        {
            _player.Move(_dx, _dy);
            Console.WriteLine($"Player moved to ({_player.X}, {_player.Y}).");
        }
        public void HandleInput(ConsoleKey input) => _player.StateMachine.ChangeState(new PlayerIdleState(_player));
        public void Update(double deltaTime) { }
        public void Exit() { }
    }

}
