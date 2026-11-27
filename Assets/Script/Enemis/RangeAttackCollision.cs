using System;
using UnityEngine;

[RequireComponent (typeof(CircleCollider2D))]
public class RangeAttackCollision : MonoBehaviour
{
    public CircleCollider2D _CC;

    public event Action _RangeAttackCollided;

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
        if (collision.gameObject.CompareTag("Player"))
        {
            _RangeAttackCollided?.Invoke();
        }
    }
}