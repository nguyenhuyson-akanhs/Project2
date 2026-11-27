using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerBehaviour : MonoBehaviour
{
    [Header("Asset Saved")]
    [SerializeField] protected PlayerGeneralStatus _Status;

    [Header("Basic Status")]
    public int _BasicMaxHP;
    public int _BasicDamageDeal;
    public float _BasicRangeAttack;
    public float _BasicMoveSpeed;
    public float _BasicAttackSpeed;

    [Header("Skills")]
    protected bool _IsCastable = true;
    public SkillActiveController _SkillActive1;
    public SkillActiveController _SkillActive2;
    public SkillActiveController _SkillActive3;

    [Header("Animtor")]
    public Animator _Animator;
    public bool _IsAnimatorBusy = false;

    [Header("Require")]
    [HideInInspector] public Rigidbody2D _ParentRB;
    [HideInInspector] public Rigidbody2D _RB;

    [Header("StateController")]
    protected StateMachine _StateMachine;

    [Header("TurnBased")]
    protected int _MaxAttackPoints;
    protected int _CurrentAttackPoints;

    [Header("Collider")]
    [SerializeField] protected List<DealDamage> _DealDamageScript;
    protected CollideEnemisArea _CollideEnemis;

    [Header("Action Button Script")]
    [SerializeField] protected ChangeRunL _RunL;
    [SerializeField] protected ChangeRunR _RunR;
    [SerializeField] protected ChangeStopButton _StopButton;

    [Header("MoveDirection")]
    [HideInInspector] public int DirX = 1;

    protected virtual void Awake()
    {
        _ParentRB = gameObject.transform.parent.GetComponent<Rigidbody2D>();
        _RB = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();

        _StateMachine = new StateMachine();

        _CollideEnemis = GetComponentInChildren<CollideEnemisArea>();

        CopyStatus();
    }

    protected virtual void PlayerTurn(TurnBased turn)
    {
        if (turn != TurnBased.Player)
        {
            foreach(var i in _DealDamageScript) i._CapsuleCollider.enabled = false;
            return;
        }
        _CurrentAttackPoints = _MaxAttackPoints;
        foreach (var i in _DealDamageScript) i._CapsuleCollider.enabled = true;
    }

    public virtual void test()
    {
        PlayerAttack attack = _Status._Attack;
    }

    protected virtual void OnEnable()
    {
        // đăng kí event
        MainEventChannel._SwitchTurn += PlayerTurn;
        _RunL.ChangeRun += ChangeDirL;
        _RunR.ChangeRun += ChangDirR;
    }

    protected virtual void OnDisable()
    {
        // hủy event
        MainEventChannel._SwitchTurn -= PlayerTurn;
        _RunL.ChangeRun -= ChangeDirL;
        _RunR.ChangeRun -= ChangDirR;
    }

    protected virtual void Start()
    {
    }

    protected virtual void IntitializeFirstState(IState state)
    {
        _StateMachine.Initialize(state);
    }

    protected virtual void FixedUpdate()
    {
        _StateMachine._CurrentState.FrameUpdate();
    }
    protected virtual void ChangeDirL()
    {
        DirX = -1;
    }
    protected virtual void ChangDirR()
    {
        DirX = 1;
    }

    protected virtual async Awaitable ChangeAttack(PlayerAttack _Attack)
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

    protected virtual async Awaitable ChangeIdle(PlayerIdle _Idle)
    {
        if (_IsAnimatorBusy) return;
        _StateMachine.ChangeState(_Idle);
        await Awaitable.NextFrameAsync();
    }

    protected virtual async Awaitable ChangeMove(PlayerMove _Move)
    {
        if (_IsAnimatorBusy) return;
        _StateMachine.ChangeState(_Move);
        await Awaitable.NextFrameAsync();
    }

    [HideInInspector] public string _SkillName;
    [HideInInspector] public float _SkillLength;
    [HideInInspector] public int _SkillDamageDeal;

    #region ----- Copy Status From Data -----: CopyStatus() 
    protected virtual void CopyStatus()
    {
        _BasicMaxHP = _Status._BasicMaxHP;
        _BasicDamageDeal = _Status._BasicDamageDeal;
        _BasicRangeAttack = _Status._BasicRangeAttack;
        _BasicMoveSpeed = _Status._BasicMoveSpeed;
        _BasicAttackSpeed = _Status._BasicAttackSpeed;
        _MaxAttackPoints = _Status._TotalAttackPoints;
    }
    #endregion
    #region ----- Copy && Deallocation SKill Data ----- 
    protected virtual void CopyTempSkillStatus(SkillGeneralStatus Skill)
    {
        _SkillName = Skill._SkillName;
        _SkillLength = Skill._SkillLength;
        _SkillDamageDeal = Skill._DamageDeal;
    }

    protected virtual void DeallocationSkillStatus()
    {
        _SkillName = "";
        _SkillLength = 0;
        _SkillDamageDeal = 0;
    }
    #endregion
}
