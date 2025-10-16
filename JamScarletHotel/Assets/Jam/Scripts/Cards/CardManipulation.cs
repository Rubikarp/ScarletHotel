using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(BaseGameCard))]
public class CardManipulation : MonoBehaviour,
    IPointerUpHandler, IPointerDownHandler,
    IPointerEnterHandler, IPointerExitHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;

    [field: Header("Info")]
    [field: SerializeField, ReadOnly] public bool isHovering { get; private set; }
    [field: SerializeField, ReadOnly] public bool isDragging { get; private set; }
    [SerializeField, ReadOnly] private Vector3 dragOffset;

    [Header("Events")]
    public UnityEvent<CardManipulation> EnterHoverEvent = new UnityEvent<CardManipulation>();
    public UnityEvent<CardManipulation> ExitHoverEvent = new UnityEvent<CardManipulation>();
    [Space]
    public UnityEvent<CardManipulation> MouseDownEvent = new UnityEvent<CardManipulation>();
    public UnityEvent<CardManipulation> MouseUpEvent = new UnityEvent<CardManipulation>();
    [Space]
    public UnityEvent<CardManipulation> BeginDragEvent = new UnityEvent<CardManipulation>();
    public UnityEvent<CardManipulation> EndDragEvent = new UnityEvent<CardManipulation>();

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        dragOffset = mousePosition - (Vector2)transform.position;

        isDragging = true;

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        BeginDragEvent.Invoke(this);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - dragOffset;
        transform.position = targetPosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        EndDragEvent.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EnterHoverEvent.Invoke(this);
        isHovering = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ExitHoverEvent.Invoke(this);
        isHovering = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MouseDownEvent.Invoke(this);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        MouseUpEvent.Invoke(this);
    }
}
