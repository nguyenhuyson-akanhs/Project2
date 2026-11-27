using System;
using TMPro;
using UnityEngine;

public class PolarText : MonoBehaviour
{
    public static PolarText instance;
    protected TextMeshProUGUI _Text;
    public event Action _RemoveItem;
    protected virtual void Awake()
    {
        instance = this;

        _Text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        CanvasBlocking.Instance._Image.raycastTarget = true;
    }

    protected virtual void Start()
    {
        transform.parent.gameObject.GetComponent<CanvasGroup>().enabled = false;
        transform.parent.gameObject.SetActive(false);
    }

    public virtual void SetText(in string NameType,in string Name)
    {
        _Text.text = $"{NameType}: {Name} ?";
    }

    public virtual void YesButton()
    {
        // remove item here
        _RemoveItem?.Invoke();
        _RemoveItem = null;
        transform.parent.gameObject.SetActive(false);
        CanvasBlocking.Instance._Image.raycastTarget = false;
    }
    public virtual void NoButton()
    {
        _RemoveItem = null;
        transform.parent.gameObject.SetActive(false);
        if (TotalValue.instance.gameObject.activeInHierarchy) TotalValue.instance.transform.parent.parent.gameObject.SetActive(false);
        CanvasBlocking.Instance._Image.raycastTarget = false;
        // close
    }
}

