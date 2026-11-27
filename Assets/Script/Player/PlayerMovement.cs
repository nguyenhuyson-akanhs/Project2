using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody2D _Rigidbody2D;
    public GameObject _UpsidePlatformPoint;
    public GameObject _DownsidePlatformPoint;
    public float _JumpForce;
    public float _SecondWaitToNextJumb;
    public float _GravityBase;
    public float _GravityMulti;
    private bool _Jumpable = true;
    private PlatformSide _Platformside = PlatformSide.Up;

    private void Awake()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        SetGravity(_GravityBase);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && _Jumpable)
        {
            float _moveY = context.ReadValue<float>();

            // jumb current platform
            if (_Platformside == PlatformSide.Up && _moveY > 0)
            {
                _Rigidbody2D.linearVelocity = new Vector2(0, _moveY * _JumpForce);
                _ = ResetJump(_SecondWaitToNextJumb);

            }

            else if (_Platformside == PlatformSide.Down && _moveY < 0)
            {
                _Rigidbody2D.linearVelocity = new Vector2(0, _moveY * _JumpForce);
                _ = ResetJump(_SecondWaitToNextJumb);

            }


            // switch platform
            else if (_Platformside == PlatformSide.Up && _moveY < 0) // to down
            {
                _Platformside = PlatformSide.Down;

                transform.SetParent(_DownsidePlatformPoint.transform, false);

                Vector3 scale = transform.localScale;
                transform.localScale = new Vector3(scale.x, scale.y * -1, scale.z);

                _GravityBase *= -1;
                SetGravity(_GravityBase);
                _ = ResetJump(0.1f);

            }

            else if (_Platformside == PlatformSide.Down && _moveY > 0) // to up
            {
                _Platformside = PlatformSide.Up;

                transform.SetParent(_UpsidePlatformPoint.transform, false);

                Vector3 scale = transform.localScale;
                transform.localScale = new Vector3(scale.x, scale.y * -1, scale.z);

                _GravityBase *= -1;
                SetGravity(_GravityBase);
                _ = ResetJump(0.1f);

            }

        }
    }

    private async Awaitable ResetJump(float sec)
    {
        _Jumpable = false;

        _ = GravityEffectJump();
        await Awaitable.WaitForSecondsAsync(sec);
        _Jumpable = true;
    }

    private async Awaitable GravityEffectJump()
    {
        float tempGravity = _GravityBase;

        while (Mathf.Abs(_Rigidbody2D.linearVelocityY) > 0.01f)
        {
            tempGravity *= _GravityMulti;
            SetGravity(tempGravity);

            await Awaitable.WaitForSecondsAsync(0.1f);
        }
        SetGravity(_GravityBase);
    }

    private void SetGravity(float gravity)
    {
        _Rigidbody2D.gravityScale = gravity;
    }


    private void FixedUpdate()
    {
    }
}

public enum PlatformSide { Up, Down }