using UnityEngine;

public class EnemisIdle : EnemisState
{
    public EnemisIdle(StateMachine state, EnemisBehaviour behaviour) : base(state, behaviour)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }
}
