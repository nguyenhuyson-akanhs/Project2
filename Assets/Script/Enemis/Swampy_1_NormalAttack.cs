using UnityEngine;

public class Swampy_1_NormalAttack : EnemisAttack
{
    private Swampy_1_Behaviour _Behaviour;
    public Swampy_1_NormalAttack(StateMachine state, Swampy_1_Behaviour behaviour) : base(state, behaviour)
    {
        _Behaviour = behaviour;
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
