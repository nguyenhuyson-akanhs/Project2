using UnityEngine;

[CreateAssetMenu(fileName = "EnemisGeneral", menuName = "ScriptableObject/EnemisGeneral")]
public class EnemisGeneralStatus : Enemis
{
    [Header("Basic Status")]
    public int _MaxHP;
    public int _CurrentHP;
    public int _DamageDeal;
    public float _RangeAttack;
    public float _MoveSpeed;
    public float _AttackSpeed;
    public int _TotalAttackPoints;
    [Range(0, 1000)] public int _SpawnRate;

    [Header("Model")]
    public GameObject _Prefab;
}
