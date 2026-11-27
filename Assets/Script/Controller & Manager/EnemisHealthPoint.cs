using UnityEngine;
using AYellowpaper.SerializedCollections;

public class EnemisHealthPoint : HealthPoint
{
    [Header("Asset")]
    [SerializeField] protected EnemisGeneralStatus _Status;
    protected EnemisDropItem _EnemisDrop;
    protected EnemisBehaviour _Behaviour;
    protected override void Awake()
    {
        _Behaviour = GetComponentInParent<EnemisBehaviour>();
        _EnemisDrop = GetComponent<EnemisDropItem>();
    }
    protected override void Start()
    {
        _MaxHP = _Status._MaxHP;
        base.Start();
    }

    public override void TakeDamgage(int Amount)
    {
        base.TakeDamgage(Amount);

        if(_CurrentHP == 0)
        {
            DropItem();
        }
    }

    protected virtual void DropItem()
    {
        _EnemisDrop.Drop();
    }
}
