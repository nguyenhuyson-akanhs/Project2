using UnityEngine;

public class BagButton : MonoBehaviour
{
    [SerializeField] protected GameObject _CloseButton;
    [SerializeField] protected CanvasGroup _BagCanvasGroup;
    [HideInInspector] public bool IsEnable { get; private set; }

    protected virtual void Start()
    {
        _CloseButton.SetActive(false);

        IsEnable = false;

        _BagCanvasGroup.alpha = 0;
        _BagCanvasGroup.interactable = false;
        _BagCanvasGroup.blocksRaycasts = false;
    }
    public virtual void OnInteract()
    {
        IsEnable = !IsEnable;

        if (IsEnable)
        {
            OpenBag();
            _CloseButton.SetActive(true);
        }
        else
        {
            CloseBag();
            _CloseButton.SetActive(false);
        }
    }
    public virtual void OpenBag()
    {
        _BagCanvasGroup.alpha = 1;
        _BagCanvasGroup.interactable = true;
        _BagCanvasGroup.blocksRaycasts = true;
    }

    public virtual void CloseBag()
    {
        _BagCanvasGroup.alpha = 0;
        _BagCanvasGroup.interactable = false;
        _BagCanvasGroup.blocksRaycasts = false;
    }
}
