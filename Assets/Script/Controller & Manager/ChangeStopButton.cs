using System;
using UnityEngine;

public class ChangeStopButton : MonoBehaviour
{
    public event Action ChangeStop;

    protected virtual void Awake()
    {
    }

    public virtual void OnInteract()
    {
        Debug.Log("click");
        ChangeStop?.Invoke();
    }
}

