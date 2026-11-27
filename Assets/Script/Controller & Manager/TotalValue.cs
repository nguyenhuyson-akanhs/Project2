using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TotalValue : MonoBehaviour
{
    public static TotalValue instance;
    [SerializeField] protected List<Value_n> _ListNumber = new();

    protected virtual void Awake()
    {
        instance = this;
    }
    protected virtual void Start()
    {
        transform.parent.parent.gameObject.SetActive(false);
    }
    public void Test()
    {
        Debug.Log(ReturnTotalValue());
    }

    public virtual int ReturnTotalValue()
    {
        double v = 0;
        for (int i = 0; i < _ListNumber.Count; i++)
        {
            v += _ListNumber[i]._Value * Mathf.Pow(10f, i);
        }
        transform.parent.parent.gameObject.SetActive(false);

        return (int)v;
    }
}
