using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myProject.gameloop
{
    public class EnemyState
    {
        public StateMachine StateMachine { get; } = new StateMachine();
        public int X { get; private set; }
        public int Y { get; private set; }

        private PlayerState _player;

        public EnemyState(int startX, int startY, PlayerState player)
        {
            X = startX;
            Y = startY;
            _player = player;

            var patrollingState = new EnemyPatrollingState(this);
            StateMachine.SetInitialState(patrollingState);
        }

        public void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }

        public double DistanceToPlayer() => Math.Sqrt(Math.Pow(X - _player.X, 2) + Math.Pow(Y - _player.Y, 2));
    }

    // États des ennemis
    public class EnemyPatrollingState : IState
    {
        private readonly EnemyState _enemy;

        public EnemyPatrollingState(EnemyState enemy)
        {
            _enemy = enemy;
        }

        public void Enter() => Console.WriteLine("Enemy is patrolling.");
        public void HandleInput(ConsoleKey input) {
            if (_enemy.DistanceToPlayer() < 3)
            {
                _enemy.StateMachine.ChangeState(new EnemyChasingState(_enemy));
            }
            else
            {
                _enemy.Move(1, 0); // Patrouille simple
            }
        }
        public void Update(double deltaTime)
        {
            Console.WriteLine($"Enemy moved to ({_enemy.X}, {_enemy.Y}).");
        }
        public void Exit() { }
    }

    public class EnemyChasingState : IState
    {
        private readonly EnemyState _enemy;

        public EnemyChasingState(EnemyState enemy)
        {
            _enemy = enemy;
        }

        public void Enter() => Console.WriteLine("Enemy is chasing the player!");
        public void HandleInput(ConsoleKey input) { }
        public void Update(double deltaTime)
        {
            // Déplacement simple vers le joueur
            int dx = _enemy.X > 5 ? -1 : 1;
            int dy = _enemy.Y > 5 ? -1 : 1;
            _enemy.Move(dx, dy);
        }
        public void Exit() { }
    }

}
