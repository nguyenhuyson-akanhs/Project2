using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private RectTransform _rectTransform;
    private Canvas _parentCanvas;
    private Camera _uiCamera;

    private void Awake()
    {
        
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _rectTransform = GetComponent<RectTransform>();
        _parentCanvas = GetComponentInParent<Canvas>();

        if (_parentCanvas != null)
        {
            _uiCamera = _parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay
                ? null
                : _parentCanvas.worldCamera != null ? _parentCanvas.worldCamera : Camera.main;
        }
    }

    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (_particleSystem.isPlaying) return;

            Vector2 mousePos = Mouse.current.position.ReadValue();

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _parentCanvas.transform as RectTransform,
                    mousePos,
                    _uiCamera,
                    out Vector2 localPoint))
            {
                _rectTransform.anchoredPosition = localPoint;

                _particleSystem.Play();
            }
        }
    }
}