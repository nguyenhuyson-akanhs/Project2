using UnityEngine;

public class PlayerAttack : PlayerState
{
    public bool _IsCasting = false;
    public PlayerAttack(StateMachine stateMachine, PlayerBehaviour playerBehaviour) : base(stateMachine, playerBehaviour)
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
