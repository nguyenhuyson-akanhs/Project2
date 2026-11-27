using UnityEngine;

public class EnemisAttack : EnemisState
{
    public bool _IsCasting = false;
    public EnemisAttack(StateMachine state, EnemisBehaviour behaviour) : base(state, behaviour)
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
