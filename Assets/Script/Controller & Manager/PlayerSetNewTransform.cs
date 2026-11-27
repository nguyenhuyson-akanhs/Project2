using UnityEngine;

public class PlayerSetNewTransform : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        LoadSceneManger._SetPlayerNewTransform += SetTranform;
    }
    protected virtual void OnDisable()
    {
        LoadSceneManger._SetPlayerNewTransform -= SetTranform;
    }

    protected virtual void SetTranform(Transform newTransform)
    {
        transform.localPosition = newTransform.localPosition;
        transform.localRotation = newTransform.localRotation;
        transform.localScale = newTransform.localScale;
    }
}
