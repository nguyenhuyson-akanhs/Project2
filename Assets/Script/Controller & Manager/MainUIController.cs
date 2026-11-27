using TMPro;
using UnityEngine;

public class MainUIController : MonoBehaviour
{
    [Header("Asset Saved")]
    public PlayerStatusUI _PlayerStatusUI;
    public MainEventChannel _EventChannel;

    [Header("Player Health Points")]
    public TextMeshProUGUI _HealthPointText;

    #region ----- OnEnable & Disable ----- 
    private void OnEnable()
    {
        _EventChannel._HealthChange += OnChangeValueOfHealth;
    }

    private void OnDisable()
    {
        _EventChannel._HealthChange -= OnChangeValueOfHealth;
    }
    #endregion

    private void Start()
    {
        FirstUpdateUI();
    }

    #region ----- Enter game, first update UI -----
    private void FirstUpdateUI()
    {
        OnChangeValueOfHealth(_PlayerStatusUI._CurrentHP,_PlayerStatusUI._MaxHP);
    }
    #endregion

    #region ----- Update HP value when changing -----
    public void OnChangeValueOfHealth(int CurrentHP,int MaxHP)
    {
        _PlayerStatusUI._MaxHP = MaxHP;
        _PlayerStatusUI._CurrentHP = CurrentHP;

        _HealthPointText.text = $"{_PlayerStatusUI._CurrentHP} / {_PlayerStatusUI._MaxHP} HP";
    }
    #endregion
}
