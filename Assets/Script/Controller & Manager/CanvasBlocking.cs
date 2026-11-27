using UnityEngine;
using UnityEngine.UI;

public class CanvasBlocking : MonoBehaviour
{
    public static CanvasBlocking Instance;
    public Image _Image;
    private void Awake()
    {
        Instance = this;
        _Image = GetComponent<Image>();
    }
    private void Start()
    {
        _Image.raycastTarget = false;
    }
}
