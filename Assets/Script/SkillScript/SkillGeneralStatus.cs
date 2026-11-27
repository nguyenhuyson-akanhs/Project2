using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SkillStatus", menuName = "ScriptableObject/SkillStatus")]
public class SkillGeneralStatus : ScriptableObject
{
    [Header("Basic Status")]
    public int _DamageDeal;
    public float _CastTime;
    public float _Cooldown;
    public float _SkillTime;
    public float _RangeEffect;
    public int _AttackPointsCost;

    [Header("Animation")]
    public string _SkillName;
    public float _SkillLength;

    [Header("Model")]
    public Image Icon;
    public GameObject _Prefab;
}
