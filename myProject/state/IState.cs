using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myProject.gameloop
{
    public interface IState
    {
        void Enter();
        void HandleInput(ConsoleKey input);
        void Update(double deltaTime);
        void Exit();
    }
}
