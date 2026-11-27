using UnityEngine;

public class EnemisDealDamage : DealDamage
{
    [SerializeField] protected EnemisBehaviour _Behaviour;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) Debug.Log("hit player");
        int id = collision.GetInstanceID();

        DamageDeal = _Behaviour._SkillDamageDeal;
        if (!_HashSetDamageContacts.Contains(id) && collision.gameObject.CompareTag("Player") && collision.TryGetComponent<IDamageable>(out var component))
        {
            _HashSetDamageContacts.Add(id);

            component.TakeDamgage(DamageDeal); 
        }
        DamageDeal = 0;
    }
}