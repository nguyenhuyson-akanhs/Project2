using UnityEngine;

public class LoadSceneCanvasFix : MonoBehaviour
{
    protected Canvas _Canvas;
    protected virtual void Awake()
    {
        _Canvas = GetComponent<Canvas>();
    }

    protected virtual void Start()
    {
        _Canvas.worldCamera = Camera.main;
    }
}
