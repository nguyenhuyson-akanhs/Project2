using UnityEngine;

public class Swampy_1_Idle : EnemisIdle
{
    private Swampy_1_Behaviour _Behaviour;
    public Swampy_1_Idle(StateMachine state, Swampy_1_Behaviour behaviour) : base(state, behaviour)
    {
        _Behaviour = behaviour;
    }

    public override void Enter()
    {
        // run idle animation here;
        _Behaviour._Animator.CrossFade("Idle", 0f, 0);
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
