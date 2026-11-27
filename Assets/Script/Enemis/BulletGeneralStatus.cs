using UnityEngine;

[CreateAssetMenu(fileName = "BulletGeneral", menuName = "ScriptableObject/BulletGeneral")]
public class BulletGeneralStatus : Enemis
{
    [Header("Basic Status")]
    public float _MoveSpeed;
    public float _LifeSpan;
    public int _DamageDeal;

    [Header("Model")]
    public GameObject _Prefab;
}
