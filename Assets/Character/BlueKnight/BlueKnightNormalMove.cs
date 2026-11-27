using UnityEngine;

public class BlueKnightNormalMove : PlayerMove
{
    private BlueKnightBehaviour _Behaviour;
    public BlueKnightNormalMove(StateMachine stateMachine, BlueKnightBehaviour _Behaviour) : base(stateMachine, _Behaviour)
    {
        this._Behaviour = _Behaviour;
    }

    public override void Enter()
    {
        Debug.Log("EnterMove");
        base.Enter();
        _Behaviour._Animator.CrossFade("NormalRun", 0.9f, 0);
        _ = RunAction();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    protected virtual async Awaitable RunAction()
    {
        await Awaitable.WaitForSecondsAsync(1.2f);
        while (_StateMachine._CurrentState == this)
        {
            _Behaviour._ParentRB.linearVelocity = new Vector3(1, 0, 0) * _Behaviour.DirX;
            await Awaitable.NextFrameAsync();
        }
        await Awaitable.WaitForSecondsAsync(0.2f);
        _Behaviour._ParentRB.linearVelocity = Vector3.zero;
    }
}
