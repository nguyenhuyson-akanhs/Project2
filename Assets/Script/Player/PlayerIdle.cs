using UnityEngine;

public class PlayerIdle : PlayerState
{   
    public PlayerIdle(StateMachine stateMachine, PlayerBehaviour playerBehaviour) : base(stateMachine, playerBehaviour)
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
