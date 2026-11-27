using UnityEngine;

public class BlueKnightNormalIdle : PlayerIdle
{
    private BlueKnightBehaviour _Behaviour;
    public BlueKnightNormalIdle(StateMachine stateMachine, BlueKnightBehaviour _Behaviour) : base(stateMachine, _Behaviour)
    {
        this._Behaviour = _Behaviour;
    }

    public override void Enter()
    {
        base.Enter();
        _Behaviour._Animator.CrossFade("NormalIdle", 0.9f, 0);
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
