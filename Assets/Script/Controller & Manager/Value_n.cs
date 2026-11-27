using TMPro;
using UnityEngine;

public class Value_n : MonoBehaviour
{
    [HideInInspector] public int _Value { get; private set; } = 0;
    protected TextMeshProUGUI _Text;

    protected virtual void Awake()
    {
        _Text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ClickUp()
    {
        _Value++;
        _Value %= 10;
        _Text.text = _Value.ToString();
    }
    public void ClickDown()
    {
        if (_Value == 0) _Value = 10;
        _Value--;
        _Text.text = _Value.ToString();
    }

    protected virtual void OnEnable()
    {
        _Value = 0;
        _Text.text = _Value.ToString();
    }
}
