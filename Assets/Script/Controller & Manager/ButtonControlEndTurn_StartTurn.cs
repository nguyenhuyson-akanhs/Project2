using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonControlEndTurn_StartTurn : MonoBehaviour
{
    private Button _Button;
    [Header("SkillListButton")]
    public List<Button> _ListSkillButton = new();

    protected virtual void Awake()
    {
        _Button = GetComponent<Button>();
    }

    protected virtual void Start()
    {
        _Button.interactable = false;
    }

    public virtual void OnInteract()
    {
        if (TurnBasedGameController.CurrentTurn != TurnBased.Player) return;

        _Button.interactable = false;
        foreach (var item in _ListSkillButton)
        {
            item.interactable = false;
        }

        MainEventChannel.AnnouceNotEnoughAP_EndTurn(TurnBased.Player);
    }

    protected virtual void ResetButtonForPlayerTurn(TurnBased turn)
    {
        if (turn == TurnBased.Enemis) return; 

        _Button.interactable = true;
        foreach (var item in _ListSkillButton)
        {
            item.interactable = true;
        }
    }

    protected virtual void OnEnable()
    {
        MainEventChannel._SwitchTurn += ResetButtonForPlayerTurn;
    }

    protected virtual void OnDisable()
    {
        MainEventChannel._SwitchTurn -= ResetButtonForPlayerTurn;
    }
}
