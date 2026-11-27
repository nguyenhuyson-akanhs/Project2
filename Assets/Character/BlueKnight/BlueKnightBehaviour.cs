using UnityEngine;

public class BlueKnightBehaviour : PlayerBehaviour, SkillUser
{
    [Header("States")]
    protected BlueKnightNormalAttack _NAttack;
    protected BlueKnightNormalIdle _NIdle;
    protected BlueKnightNormalMove _NMove;

    protected override void Awake()
    {
        base.Awake();
        _NAttack = new(_StateMachine, this);
        _NIdle = new(_StateMachine, this);
        _NMove = new(_StateMachine, this);
    }
    protected override void Start()
    {
        base.Start();
        IntitializeFirstState(_NIdle);
    }

    public void CastSkill(SkillGeneralStatus Skill)
    {
        if (_IsAnimatorBusy) return;
        if (_CurrentAttackPoints < Skill._AttackPointsCost) return; // not enough AP

        _CurrentAttackCost = Skill._AttackPointsCost;

        CopyTempSkillStatus(Skill);
        _ = ChangeAttack(_NAttack);

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _StopButton.ChangeStop += CallIdle;
        _CollideEnemis._EncounterEnemis += CallIdle;

        if (_SkillActive1) _SkillActive1.ActiveSkill += CastSkill;
        if (_SkillActive2) _SkillActive2.ActiveSkill += CastSkill;
        if (_SkillActive3) _SkillActive3.ActiveSkill += CastSkill;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _StopButton.ChangeStop -= CallIdle;
        _CollideEnemis._EncounterEnemis -= CallIdle;

        if (_SkillActive1) _SkillActive1.ActiveSkill += CastSkill;
        if (_SkillActive2) _SkillActive2.ActiveSkill += CastSkill;
        if (_SkillActive3) _SkillActive3.ActiveSkill += CastSkill;
    }

    protected override void ChangDirR()
    {
        base.ChangDirR();
        CallMove();
    }
    protected override void ChangeDirL()
    {
        base.ChangeDirL();
        CallMove();
    }

    protected override async Awaitable ChangeAttack(PlayerAttack _Attack)
    {
        _ = base.ChangeAttack(_Attack);

        _CurrentAttackPoints -= _CurrentAttackCost;

        while (_Attack._IsCasting) await Awaitable.NextFrameAsync();

        DeallocationSkillStatus();

        _IsAnimatorBusy = false;

        _ = ChangeIdle(_NIdle);
    }

    #region -----Move Action----- 
    protected virtual void CallMove()
    {
        _ = ChangeMove(_NMove);
    }
    protected override async Awaitable ChangeMove(PlayerMove _move)
    {
        _ = base.ChangeMove(_move);
        await Awaitable.NextFrameAsync();
    }
    #endregion

    #region -----Idle Action----- 

    protected virtual void CallIdle()
    {
        Debug.Log("call Idle");
        _ = ChangeIdle(_NIdle);
    }
    protected override async Awaitable ChangeIdle(PlayerIdle _Idle)
    {
        _ = base.ChangeIdle(_Idle);
        await Awaitable.NextFrameAsync();
    }
    #endregion

    private int _CurrentAttackCost;
}
