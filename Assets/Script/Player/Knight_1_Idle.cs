using UnityEngine;

public class Knight_1_Idle : PlayerIdle
{
    private Knight_1_Behaviour _Behaviour;
    public Knight_1_Idle(StateMachine stateMachine, Knight_1_Behaviour _Knight_1_Behaviour) : base(stateMachine, _Knight_1_Behaviour)
    {
        _Behaviour = _Knight_1_Behaviour;
    }

    public override void Enter()
    {
        Debug.Log("Enter idle");
        base.Enter();
        _Behaviour._Animator.CrossFade("Idle",1f,0);
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
