using UnityEngine;

public class BlueKnightNormalAttack : PlayerAttack
{
    private BlueKnightBehaviour _Behaviour;
    public BlueKnightNormalAttack(StateMachine stateMachine, BlueKnightBehaviour _Behaviour) : base(stateMachine, _Behaviour)
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

        _Behaviour._Animator.CrossFade("NormalAttack", 0);

        await Awaitable.WaitForSecondsAsync(_Behaviour._SkillLength);
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
