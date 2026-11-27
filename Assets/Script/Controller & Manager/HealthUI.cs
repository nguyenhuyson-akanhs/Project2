using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] protected HealthPoint _HealthPoint;
    [SerializeField] protected TextMeshProUGUI _HealthText;

    protected virtual void Awake()
    {
        _HealthText = GetComponent<TextMeshProUGUI>();
    }

    protected virtual void OnEnable()
    {
        _HealthPoint._UpdateLocalUIHP += UpdateUI;
    }

    protected virtual void OnDisable()
    {
        _HealthPoint._UpdateLocalUIHP -= UpdateUI;
    }

    protected virtual void UpdateUI(int CurrentHP, int MaxHP)
    {
        _HealthText.text = $"{CurrentHP} / {MaxHP}";
    }

    // TODO: add HealthUI for Player and Enemis, add collider for attack to trigger Damage and TakeDamage
}
