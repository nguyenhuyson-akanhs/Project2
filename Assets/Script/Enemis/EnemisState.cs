using UnityEngine;

public abstract class EnemisState : IState
{
    protected StateMachine _StateMachine;
    protected EnemisBehaviour _EnemisBehaviour;
    public EnemisState(StateMachine state,EnemisBehaviour behaviour)
    {
        this._EnemisBehaviour = behaviour;
        this._StateMachine = state;
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
