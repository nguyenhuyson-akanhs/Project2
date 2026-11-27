using System;
using UnityEngine;

[RequireComponent (typeof(CircleCollider2D))]
public class RangeAttackCollision_player : MonoBehaviour
{
    [HideInInspector] public CircleCollider2D _CC;
    [SerializeField] private PlayerAttack _Attack;

    public event Action<PlayerAttack> _RangeAttackCollided;

    protected virtual void Awake()
    {
        _CC = GetComponent<CircleCollider2D>();
    }

    protected virtual void Start()
    {
        _CC.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemis"))
        {
            _RangeAttackCollided?.Invoke(_Attack);
        }
    }
}