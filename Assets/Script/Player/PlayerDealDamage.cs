using UnityEngine;

public class PlayerDealDamage : DealDamage
{
    [SerializeField] protected PlayerBehaviour _Behaviour;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        int id = collision.GetInstanceID();

        DamageDeal = _Behaviour._SkillDamageDeal;
        if (!_HashSetDamageContacts.Contains(id) && collision.gameObject.CompareTag("Enemis") && collision.TryGetComponent<IDamageable>(out var component))
        {
            _HashSetDamageContacts.Add(id);

            component.TakeDamgage(DamageDeal);
        }
        DamageDeal = 0;

    }
}
