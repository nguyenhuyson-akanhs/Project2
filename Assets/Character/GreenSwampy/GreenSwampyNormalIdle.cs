using UnityEngine;

public class GreenSwampyNormalIdle : EnemisIdle
{
    private GreenSwampyBehaviour _Behaviour;
    public GreenSwampyNormalIdle(StateMachine state, GreenSwampyBehaviour _Behaviour) : base(state, _Behaviour)
    {
        this._Behaviour = _Behaviour;
    }

    public override void Enter()
    {
        // run idle animation here;
        _Behaviour._Animator.CrossFade("NormalIdle", 0f, 0);
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
