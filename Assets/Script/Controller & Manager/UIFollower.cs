using UnityEngine;

[ExecuteAlways]
public class UIFollower : MonoBehaviour
{
    [SerializeField] protected Transform _TransformTarget;
    [SerializeField] protected Vector3 _Offset;

    protected virtual void LateUpdate()
    {
        transform.position = _TransformTarget.position + _Offset;
    }
}
