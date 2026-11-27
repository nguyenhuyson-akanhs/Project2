using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

[System.Serializable]


/// <summary>
/// Current turn-based
/// </summary>
public class State_Skill
{
    public EnemisAttack State { get; set; }
    public SkillGeneralStatus Skill { get; set; }

    public State_Skill(EnemisAttack state, SkillGeneralStatus skill)
    {
        State = state;
        Skill = skill;
    }
}
[RequireComponent(typeof(EnemisHealthPoint))]
[RequireComponent(typeof(EnemisDropItem))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class EnemisBehaviour : MonoBehaviour
{
    protected enum EnemisCondition { _Walking, _Attacking, _Stunned }
    protected EnemisCondition _Condition;


    [Header("Asset Saved")]
    public EnemisGeneralStatus _Status;

    [Header("Basic Status")]
    [SerializeField] protected int _MaxHP;
    [SerializeField] protected int _CurrentHP;
    public int _DamageDeal;
    public float _RangeAttack;
    public float _MoveSpeed;
    public float _AttackSpeed;

    [Header("Animation")]
    [HideInInspector] public Animator _Animator;
    protected bool _IsAnimatorBusy = false;

    [Header("Require")]
    public Rigidbody2D _RB;
    public StateMachine _StateMachine;

    [Header("TurnBased")]
    protected int _MaxAttackPoints;
    protected int _CurrentAttackPoints;

    [Header("TempSkillStatus")]
    [HideInInspector] public int _SkillDamageDeal;
    [HideInInspector] public string _Name;
    [HideInInspector] public float _Length;

    [Header("Others")]
    protected List<State_Skill> _ListState_Skill;

    [Header("Collider")]
    [SerializeField] protected List<DealDamage> _DealDamageScript;

    [Header("Controller")]
    [SerializeField] protected MultiEnemisTurnController _MultiEnemisTurnController;

    protected virtual void Awake()
    {
        _RB = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
        CopyStatus();

        _StateMachine = new();
    }

    protected virtual void OnEnable()
    {
        //_MultiEnemisTurnController._Action += ThisEnemisTurn;
    }

    protected virtual void OnDisable()
    {
        //_MultiEnemisTurnController._Action += ThisEnemisTurn;
    }

    protected virtual void Start()
    {
        DeallocationSkillStatus();
        CheckMinAP();
    }

    public virtual void TakeTurn() => ThisEnemisTurn(this);

    protected virtual void ThisEnemisTurn(EnemisBehaviour behaviour)
    {
        if (behaviour != this)
        {
            return;
        }
        _CurrentAttackPoints = _MaxAttackPoints;
        foreach (var i in _DealDamageScript) i._CapsuleCollider.enabled = true;
    }

    protected virtual void TurnOffAllAttackAreaCollider()
    {
        foreach (var i in _DealDamageScript) i._CapsuleCollider.enabled = false;
    }

    protected virtual void IntitializeFirstState(EnemisState state)
    {
        _StateMachine.Initialize(state);
    }

    #region ----- AP Controller Core ------ 
    protected virtual void IntitializeState_SkillList() { }

    #region ----- Find Min AP -----
    protected int MinAP = 10000000;
    protected virtual void CheckMinAP()
    {
        foreach (var items in _ListState_Skill)
        {
            MinAP = Mathf.Min(MinAP, items.Skill._AttackPointsCost);
        }
    }
    #endregion

    protected bool _IsEnoughAP = true;
    protected bool _IsRepeatAttack = false;
    protected virtual async Awaitable RepeatAttack()
    {
        while (true)
        {
            if (!_IsEnoughAP) break;
            if (_IsAnimatorBusy) await Awaitable.NextFrameAsync();
            await Awaitable.WaitForSecondsAsync(5f);
            CastSkill();
        }

        _IsRepeatAttack = false;
    }

    protected virtual void CallReapeatAttack()
    {
        if (_IsRepeatAttack) return;
        _IsRepeatAttack = true;
        _ = RepeatAttack();
    }

    protected virtual void CastSkill()
    {
        State_Skill ss = GetSkill();
        _CurrentAttackPoints -= ss.Skill._AttackPointsCost;

        CopyTempSkillStatus(ss.Skill);
        _ = ChangeAttack(ss.State);

        if (_CurrentAttackPoints < MinAP)
        {
            _IsEnoughAP = false;
            _ = AnnounceNotEnoughAP(ss.Skill._SkillLength);
        }
    }

    /// <summary>
    /// Announce When AP is not enough anymore
    /// Delay announce to run all animation first
    /// </summary>
    /// <param name="Time">Skill Length</param>
    /// <returns></returns>
    protected virtual async Awaitable AnnounceNotEnoughAP(float Time)
    {
        await Awaitable.WaitForSecondsAsync(Time + 0.5f);
        TurnOffAllAttackAreaCollider();
        _MultiEnemisTurnController.NotEnoughAP();
    }

    protected virtual State_Skill GetSkill() // TODO: update require cho Returnable there
    {
        bool Returnable = true;

        do
        {
            int i = UnityEngine.Random.Range(0, _ListState_Skill.Count);
            return _ListState_Skill[i];
        }
        while (Returnable);

    }

    #region ----- Copy && Deallocation SKill Data ----- 
    protected virtual void CopyTempSkillStatus(SkillGeneralStatus Skill)
    {
        _Name = Skill._SkillName;
        _Length = Skill._SkillLength;
        _SkillDamageDeal = Skill._DamageDeal;
    }

    protected virtual void DeallocationSkillStatus()
    {
        _Name = "";
        _Length = 0;
        _SkillDamageDeal = 0;
    }
    #endregion
    #endregion

    protected virtual async Awaitable ChangeAttack(EnemisAttack _Attack)
    {
        if (_IsAnimatorBusy) return;
        _IsAnimatorBusy = true;
        _StateMachine.ChangeState(_Attack);
        await Awaitable.NextFrameAsync();

        foreach (var i in _DealDamageScript)
        {
            i._HashSetDamageContacts.Clear(); // TODO: fix each skill each collider
        }
    }

    protected virtual async Awaitable ChangeIdle(EnemisIdle _Idle)
    {
        if (_IsAnimatorBusy) return;
        _StateMachine.ChangeState(_Idle);
        await Awaitable.NextFrameAsync();
    }

    protected virtual async Awaitable ChangeMove(EnemisMove _Move)
    {
        if (_IsAnimatorBusy) return;
        _StateMachine.ChangeState(_Move);
        await Awaitable.NextFrameAsync();
    }


    #region ----- Copy Status From Data -----: CopyStatus() 
    private void CopyStatus()
    {
        _MaxHP = _Status._MaxHP;
        _CurrentHP = _Status._CurrentHP;
        _DamageDeal = _Status._DamageDeal;
        _RangeAttack = _Status._RangeAttack;
        _MoveSpeed = _Status._MoveSpeed;
        _AttackSpeed = _Status._AttackSpeed;
        _MaxAttackPoints = _Status._TotalAttackPoints;
    }
    #endregion

    // TODO: học cách chuyển trạng thái mượt
}
