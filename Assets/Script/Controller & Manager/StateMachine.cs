using UnityEngine;

public class StateMachine
{
    public IState _CurrentState { get; private set; }

    public void Initialize(IState StartingState)
    {
        _CurrentState = StartingState;
        _CurrentState.Enter();
    }

    public void ChangeState(IState NewState)
    {
        if (_CurrentState == NewState) return;

        _CurrentState?.Exit();
        _CurrentState = NewState;
        _CurrentState.Enter();
    }
}

// break time ti
