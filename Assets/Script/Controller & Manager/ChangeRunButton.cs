using System;
using UnityEngine;

public class ChangeRunButton : MonoBehaviour
{
    public event Action ChangeRun;

    protected virtual void Awake()
    {
    }

    public virtual void OnInteract()
    {
        ChangeRun?.Invoke();
    }
}
