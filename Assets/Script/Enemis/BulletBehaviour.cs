using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class BulletBehaviour : MonoBehaviour
{
    [Header("Asset Saved")]
    public BulletGeneralStatus _Status;


    private BoxCollider2D _Collider;

    protected virtual void Awake()
    {
        _Collider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Start()
    {
        _Collider.isTrigger = true;
    }

    protected virtual void FixedUpdate()
    {
        Move(Vector3.left);
    }

    protected virtual void Move(Vector3 Direction)
    {
        transform.Translate(Direction.normalized * _Status._MoveSpeed * Time.fixedDeltaTime, Space.World);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var component))
        {
            component.TakeDamgage(_Status._DamageDeal);
        }

        Destroy(gameObject);
    }

    protected virtual void OnEnable()
    {
        CancelInvoke();
        Destroy(gameObject, _Status._LifeSpan);
    }

    protected virtual void OnDisable()
    {
        CancelInvoke();
    }
}
