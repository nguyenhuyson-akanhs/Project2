using System.Collections.Generic;
using UnityEngine;

public class GreenSwampyBehaviour : EnemisBehaviour
{
    [Header("States")]
    protected GreenSwampyNormalIdle _Idle;
    protected GreenSwampyNormalAttack _NormalAttack;

    [Header("Skills")]
    [SerializeField] protected SkillGeneralStatus _SkillNormalAttack;

    protected override void IntitializeState_SkillList()
    {
        base.IntitializeState_SkillList();

        _ListState_Skill = new List<State_Skill>
        {
            new State_Skill(_NormalAttack,_SkillNormalAttack),
        };
    }

    protected override void Awake()
    {
        base.Awake();
        _Idle = new(_StateMachine, this);
        _NormalAttack = new(_StateMachine, this);

        // last
        IntitializeState_SkillList();
    }

    protected override void Start()
    {
        base.Start();
        IntitializeFirstState(_Idle);
    }

    protected override void ThisEnemisTurn(EnemisBehaviour behaviour)
    {
        Debug.Log("ThisEnemyTurn");

        if (TurnBasedGameController.CurrentTurn != TurnBased.Enemis) return;

        base.ThisEnemisTurn(this);
        _IsEnoughAP = true;
        if (TurnBasedGameController.CurrentTurn == TurnBased.Enemis) CallReapeatAttack();
        Debug.Log("EnemisTurn!");
    }


    protected override async Awaitable ChangeAttack(EnemisAttack _Attack)
    {
        _ = base.ChangeAttack(_Attack);

        while (_Attack._IsCasting) await Awaitable.NextFrameAsync();

        _IsAnimatorBusy = false;

        _ = ChangeIdle(_Idle);

        DeallocationSkillStatus();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    public override void TakeTurn()
    {
        base.TakeTurn();
    }

}
