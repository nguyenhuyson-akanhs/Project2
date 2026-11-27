using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CapsuleCollider2D))]
public class DealDamage : MonoBehaviour
{
    protected int DamageDeal;
    [HideInInspector] public CapsuleCollider2D _CapsuleCollider;

    protected virtual void Awake()
    {
        _CapsuleCollider = GetComponent<CapsuleCollider2D> ();
    }

    protected virtual void Start()
    {
        _CapsuleCollider.isTrigger = true;
        _CapsuleCollider.enabled = false;
    }

    // loc damage
    [HideInInspector] public HashSet<int> _HashSetDamageContacts = new();
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
