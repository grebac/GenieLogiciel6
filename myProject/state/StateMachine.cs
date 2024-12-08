using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myProject.gameloop
{
    public class StateMachine
    {
        private IState _currentState;

        public void SetInitialState(IState initialState)
        {
            _currentState = initialState;
            _currentState.Enter();
        }

        public void HandleInput(ConsoleKey input)
        {
            _currentState.HandleInput(input);
        }

        public void Update(double deltaTime)
        {
            _currentState.Update(deltaTime);
        }

        public void ChangeState(IState newState)
        {
            _currentState.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    }

}
