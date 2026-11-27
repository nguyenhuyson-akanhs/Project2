using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoadPreUpdateManager : MonoBehaviour
{
    //public static LoadPreUpdateManager Instance { get; private set; }

    public List<GameObject> _ListBlock = new();

    //public event Action<T> _ReadyForLoad;

    protected virtual void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //    //_ReadyForLoad?.Invoke(Instance);
        //}
        //else Destroy(this);
    }


    public virtual async Awaitable IntitaliazeObject(Action<float> Progress)
    {
        int Count = 0;

        for (int i = 0; i < _ListBlock.Count; i++)
        {
            GameObject TypeBlock = _ListBlock[i];
            if (TypeBlock == null) continue;

            TypeBlock.SetActive(true);
            foreach (Transform child in TypeBlock.transform)
            {
                Count++;
                child.gameObject.SetActive(true);

                if (Count == 10)
                {
                    Count = 0;
                    await Awaitable.NextFrameAsync();
                }
            }
            Progress((float)(i + 1) / _ListBlock.Count);
        }
    }
}
