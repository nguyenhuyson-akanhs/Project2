using UnityEngine;

public class Knight_1_Attack : PlayerAttack
{
    private Knight_1_Behaviour _Behaviour;
    public Knight_1_Attack(StateMachine stateMachine, Knight_1_Behaviour _Knight_1_Behaviour) : base(stateMachine, _Knight_1_Behaviour)
    {
        _Behaviour = _Knight_1_Behaviour;
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
