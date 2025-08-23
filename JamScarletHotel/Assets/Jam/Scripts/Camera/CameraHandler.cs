using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class CameraHandler : MonoBehaviour
{
    private Camera cam;
    private bool _isDragging;
    private Vector2 _lastMousePosition;

    [Header("Settings")]
    [SerializeField, MinMaxSlider(5, 50)] private Vector2 zoomLimit = new Vector2(1, 8);
    [SerializeField, ReadOnly]
    private float _zoomLvl;
    [SerializeField, Range(0, 1)]
    private float zoomSensitivity = 0.5f;
    [SerializeField, Range(0, 1)]
    private float moveSensitivity = 0.5f;

    public float MinZoom => zoomLimit.x;
    public float MaxZoom => zoomLimit.y;
    public float ZoomLvl
    {
        get => _zoomLvl;
        private set
        {
            _zoomLvl = value;
            cam.orthographicSize = _zoomLvl * 0.5f;
        }
    }

    private void Awake() => cam = Camera.main;

    private void Start()
    {
        _isDragging = false;
        _lastMousePosition = Mouse.current.position.ReadValue();
    }

    private void Update()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;
        if (Mathf.Abs(scroll) > 0.01f)
        {
            Zoom(scroll * zoomSensitivity);
        }

        Vector2 mousePos = Mouse.current.position.ReadValue();
        if (Mouse.current.middleButton.wasPressedThisFrame) _isDragging = true;
        if (Mouse.current.middleButton.wasReleasedThisFrame) _isDragging = false;
        if (_isDragging)
        {
            Vector3 mouseDelta = mousePos - _lastMousePosition;
            Vector2 worldDelta = cam.ScreenToWorldPoint(_lastMousePosition) - cam.ScreenToWorldPoint(mousePos);
            Move(worldDelta);
        }
        _lastMousePosition = mousePos;
    }
    public void Zoom(float delta) => ZoomLvl = Mathf.Clamp(ZoomLvl - delta, MinZoom, MaxZoom);
    public void Move(Vector2 deltaMove)
    {
        cam.transform.position += (Vector3)deltaMove * (moveSensitivity + Mathf.InverseLerp(MinZoom, MaxZoom, ZoomLvl));
    }
}