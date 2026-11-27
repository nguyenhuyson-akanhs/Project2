using System;
using UnityEngine;
using UnityEngine.Events;

public enum TurnBased { Player, Enemis, None}

[CreateAssetMenu(fileName = "MainEventChannel", menuName = "ScriptableObject/MainEventChannel")]
public class MainEventChannel : ScriptableObject
{
    public UnityAction<int,int> _HealthChange;
    public void AnnounceHealthChange(int CurrentHP,int MaxHP)
    {
        _HealthChange?.Invoke(CurrentHP,MaxHP);
    }


    #region ----- Annouce TurnBased End-Switch ------ 
    /*example:
     * PlayerBehaviour(Player) --(!_IsEnoughAP)--> AnnounceNotEnoughAP -----> SwitchTurn(GameController) if current == Player: AnnouceSwitch -> Enemis
     *                                                                                                      current == Enemis: AnnouceSwitch -> Player
     *                                                                                                      
     * Player/Enemis get Annouce -> Reset AP.
     * 
     */

    public static event Action<TurnBased> _EndTurn;
    public static void AnnouceNotEnoughAP_EndTurn(TurnBased turn) // current turn
    {
        _EndTurn?.Invoke(turn);
    }


    public static event Action<TurnBased> _SwitchTurn;
    public static void AnnouceSwitchTurn(TurnBased turn) // next turn
    {
        _SwitchTurn?.Invoke(turn);
    }
    #endregion
}
