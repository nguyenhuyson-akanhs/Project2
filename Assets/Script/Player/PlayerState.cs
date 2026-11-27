using UnityEngine;

public class PlayerState : IState
{
    protected StateMachine _StateMachine;
    protected PlayerBehaviour _PlayerBehaviour;

    public PlayerState(StateMachine stateMachine, PlayerBehaviour playerBehaviour)
    {
        this._StateMachine = stateMachine;
        this._PlayerBehaviour = playerBehaviour;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void FrameUpdate()
    {

    }
}
