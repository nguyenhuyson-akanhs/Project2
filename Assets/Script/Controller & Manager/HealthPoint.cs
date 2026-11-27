using System;
using UnityEngine;

public class HealthPoint : MonoBehaviour,IDamageable
{
    [Header("HealthPoints")]
    protected int _CurrentHP;
    protected int _MaxHP;

    /// <summary>
    /// Event Update LocalUI.
    /// <para>First(int): CurrentHP</para>
    /// <para>Second(int): MaxHP</para>
    /// </summary>
    public event Action<int, int> _UpdateLocalUIHP;

    protected virtual void Awake()
    {
    }
    protected virtual void Start()
    {
        ResetHP();
        TakeDamgage(0);
    }

    protected virtual void ResetHP() => _CurrentHP = _MaxHP;

    public virtual void TakeDamgage(int Amount)
    {
        _CurrentHP -= Amount;

        if (_CurrentHP <= 0)
        {
            _CurrentHP = 0;
            transform.parent.gameObject.SetActive(false);
        }

        _UpdateLocalUIHP?.Invoke(_CurrentHP, _MaxHP);
    }
}
