using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatusUI", menuName = "ScriptableObject/PlayerStatusUI")]
public class PlayerStatusUI : ScriptableObject
{
    [Header("Normal Status")]
    public int _MaxHP;
    public int _CurrentHP;

    public void ResetHP()
    {
        _CurrentHP = _MaxHP;
    }
}
