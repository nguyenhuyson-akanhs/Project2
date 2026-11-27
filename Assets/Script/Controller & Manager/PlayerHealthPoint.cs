using System;
using UnityEngine;

public class PlayerHealthPoint : HealthPoint
{
    [Header("Asset")]
    [SerializeField] protected PlayerGeneralStatus _Status;
    protected PlayerBehaviour _Behaviour;
    protected override void Awake()
    {
        _Behaviour = GetComponentInParent<PlayerBehaviour>();
    }

    protected override void Start()
    {
        _MaxHP = _Status._BasicMaxHP;
        base.Start();
    }
}
