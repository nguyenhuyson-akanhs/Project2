using UnityEngine;

public class GreenSwampyNormalAttack : EnemisAttack
{
    private GreenSwampyBehaviour _Behaviour;
    public GreenSwampyNormalAttack(StateMachine state, GreenSwampyBehaviour _Behaviour) : base(state, _Behaviour)
    {
        this._Behaviour = _Behaviour;
    }

    public override void Enter()
    {
        _IsCasting = true;
        base.Enter();

        _ = TriggerAttack();
    }

    protected virtual async Awaitable TriggerAttack()
    {
        Debug.Log("attack");
        // appear next point
        await Awaitable.WaitForSecondsAsync(1f);

        _Behaviour._Animator.CrossFade(_Behaviour._Name, 0);

        await Awaitable.WaitForSecondsAsync(_Behaviour._Length);
        await Awaitable.NextFrameAsync();

        Exit();
    }
    public override void Exit()
    {
        _IsCasting = false;

        base.Exit();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }
}
