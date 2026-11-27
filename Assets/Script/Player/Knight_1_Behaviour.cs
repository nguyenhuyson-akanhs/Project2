using UnityEngine;

public class Knight_1_Behaviour : PlayerBehaviour, SkillUser
{
    [Header("States")]
    protected Knight_1_Attack _Attack;
    protected Knight_1_Idle _Idle;
    protected Knight_1_Move _Move;

    protected override void Awake()
    {
        base.Awake();
        _Attack = new(_StateMachine, this);
        _Idle = new(_StateMachine, this);
        _Move = new(_StateMachine, this);
    }
    protected override void Start()
    {
        base.Start();
        IntitializeFirstState(_Idle);
    }

    public void CastSkill(SkillGeneralStatus Skill)
    {
        if (_IsAnimatorBusy) return;
        if (_CurrentAttackPoints < Skill._AttackPointsCost) return; // not enough AP

        _CurrentAttackCost = Skill._AttackPointsCost;

        CopyTempSkillStatus(Skill);
        _ = ChangeAttack(_Attack);

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (_SkillActive1) _SkillActive1.ActiveSkill += CastSkill;
        if (_SkillActive2) _SkillActive2.ActiveSkill += CastSkill;
        if (_SkillActive3) _SkillActive3.ActiveSkill += CastSkill;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (_SkillActive1) _SkillActive1.ActiveSkill += CastSkill;
        if (_SkillActive2) _SkillActive2.ActiveSkill += CastSkill;
        if (_SkillActive3) _SkillActive3.ActiveSkill += CastSkill;
    }

    protected override async Awaitable ChangeAttack(PlayerAttack _Attack)
    {
        _ = base.ChangeAttack(_Attack);

        _CurrentAttackPoints -= _CurrentAttackCost;

        while (_Attack._IsCasting) await Awaitable.NextFrameAsync();

        DeallocationSkillStatus();

        _IsAnimatorBusy = false;

        _ = ChangeIdle(_Idle);
    }

    private int _CurrentAttackCost;
}
