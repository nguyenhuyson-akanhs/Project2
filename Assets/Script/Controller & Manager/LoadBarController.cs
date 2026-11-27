using UnityEngine;
using UnityEngine.UI;

public class LoadBarController : MonoBehaviour
{
    protected Slider _Slider;

    private void Awake()
    {
        _Slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _Slider.value = 0;
    }

    public virtual void OnInteract()
    {
        if (_Slider.value != 1) return;
        _Slider.value = 0;
        gameObject.SetActive(false);
    }
}
