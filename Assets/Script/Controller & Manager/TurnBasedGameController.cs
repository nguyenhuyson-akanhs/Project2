using System.Collections.Generic;
using UnityEngine;

public class TurnBasedGameController : MonoBehaviour
{
    [Header("EnemisManger")]
    public List<GameObject> _EnemisCharacter;
    public static TurnBased CurrentTurn;

    protected virtual void Start()
    {
        _ = HandStart();
    }

    protected virtual async Awaitable HandStart()
    {
        await Awaitable.NextFrameAsync();
        await Awaitable.NextFrameAsync();

        SwitchTurn(TurnBased.Enemis);
        CurrentTurn = TurnBased.Player;
    }

    protected virtual void SwitchTurn(TurnBased turn)
    {
        if (turn == TurnBased.Enemis)
        {
            CurrentTurn = TurnBased.Player;
            MainEventChannel.AnnouceSwitchTurn(TurnBased.Player);
            _ = UpdateCheckingEnemisAlive();
        }
        else if (turn == TurnBased.Player)
        {
            CurrentTurn = TurnBased.Enemis;
            MainEventChannel.AnnouceSwitchTurn(TurnBased.Enemis);

        }
    }

    protected virtual void OnEnable()
    {
        MainEventChannel._EndTurn += SwitchTurn;
    }

    protected virtual void OnDisable()
    {
        MainEventChannel._EndTurn -= SwitchTurn;
    }

    protected virtual async Awaitable UpdateCheckingEnemisAlive()
    {
        bool allDead = false;
        while (!allDead && CurrentTurn == TurnBased.Player)
        {
            foreach (var enemy in _EnemisCharacter)
            {
                if (enemy.activeInHierarchy)
                {
                    allDead = false;
                    break;
                }
                else allDead = true;
            }

            await Awaitable.WaitForSecondsAsync(1f);
            if (allDead)
            {
                Debug.Log("all enemis dead");
                LoadSceneManger.Instance.OnInteract(LoadSceneManger.Instance._LastTown);
            } 
        }
    }
}
