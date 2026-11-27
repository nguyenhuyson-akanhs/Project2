using System;
using UnityEngine;

public class CollideEnemisArea : MonoBehaviour
{
    public event Action _EncounterEnemis;
    public static event Action<string> _EnterComabatArea;
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemisArea"))
        {
            _EncounterEnemis?.Invoke();
            _= EnterCombatScene(collision.gameObject.GetComponent<AreaSceneName>()._CombatSceneName);
        }
    }
    
    protected virtual async Awaitable EnterCombatScene(string NameScene)
    {
        await Awaitable.WaitForSecondsAsync(1f);
        _EnterComabatArea?.Invoke(NameScene);
        // load new scene
    }
}
