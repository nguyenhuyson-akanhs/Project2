using System;
using System.Collections.Generic;
using UnityEngine;

public class MultiEnemisTurnController : MonoBehaviour
{
    [Header("EnemisList")]
    [SerializeField] protected List<EnemisBehaviour> _List = new();

    ///// <summary>
    ///// Use to license turn for enemis;
    ///// <para>EnemisBehaviour: Script type</para>
    ///// </summary>
    //public event Action<EnemisBehaviour> _Action;
    [HideInInspector] public bool _IsEnoughAP;

    protected virtual void Start()
    {
    }

    protected virtual async Awaitable TillListCleared()
    {
        foreach (var enemis in _List)
        {
            if(!enemis.gameObject.activeInHierarchy) continue;

            enemis.TakeTurn();
            _IsEnoughAP = true;

            while (_IsEnoughAP)
            {
                await Awaitable.NextFrameAsync();
            }
        }
        MainEventChannel.AnnouceNotEnoughAP_EndTurn(TurnBased.Enemis);
    }

    protected virtual void EnemisTurn(TurnBased turn)
    {
        if (turn == TurnBased.Enemis)
        {
            _ = TillListCleared();
        }
    }

    public virtual void NotEnoughAP()
    {
        _IsEnoughAP = false;
    }

    protected virtual void OnEnable()
    {
        MainEventChannel._SwitchTurn += EnemisTurn;
    }

    protected virtual void OnDisable()
    {
        MainEventChannel._SwitchTurn -= EnemisTurn;

    }
}
