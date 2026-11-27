using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus_Main", menuName = "ScriptableObject/PlayerStatus_Main")]
public class PlayerGeneralStatus : Enemis
{
    [Header("Basic Status")]
    public int _BasicMaxHP;
    public int _BasicDamageDeal;
    public float _BasicRangeAttack;
    public float _BasicMoveSpeed;
    public float _BasicAttackSpeed;
    public int _TotalAttackPoints;

    [Header("Model")]
    public GameObject _Prefab;

    [Header("Skills")]
    public List<SkillGeneralStatus> _ListSkill = new();

    [Header("States")]
    public PlayerAttack _Attack;
    public PlayerMove _Move;
    public PlayerIdle _Idle;
}

// skill list: <so> onl
// TODO: skill so
