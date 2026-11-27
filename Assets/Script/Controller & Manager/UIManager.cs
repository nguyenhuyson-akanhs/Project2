using UnityEngine;

public class UIManager : MonoBehaviour
{
    protected CanvasGroup _CanvasGroup;
    private void Awake()
    {
        _CanvasGroup = GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        _CanvasGroup.alpha = 1.0f;
    }
}
