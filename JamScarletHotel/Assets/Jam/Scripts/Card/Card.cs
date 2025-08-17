using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;

[RequireComponent(typeof(CanvasGroup))]
public class Card : MonoBehaviour,
    IPointerUpHandler, IPointerDownHandler,
    IPointerEnterHandler, IPointerExitHandler,
    IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private CardHandler cardHandler;

    [Header("Info")]
    [SerializeField, ReadOnly] private Vector3 dragOffset;
    public CardSlot currentSlot;

    [field: SerializeField, ReadOnly] public bool isHovering { get; private set; }
    [field: SerializeField, ReadOnly] public bool isDragging { get; private set; }


    [Header("Events")]
    public UnityEvent<Card> EnterHoverEvent = new UnityEvent<Card>();
    public UnityEvent<Card> ExitHoverEvent = new UnityEvent<Card>();
    [Space]
    public UnityEvent<Card> MouseDownEvent = new UnityEvent<Card>();
    public UnityEvent<Card> MouseUpEvent = new UnityEvent<Card>();
    [Space]
    public UnityEvent<Card> BeginDragEvent = new UnityEvent<Card>();
    public UnityEvent<Card> EndDragEvent = new UnityEvent<Card>();

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        cardHandler = CardHandler.Instance;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        BeginDragEvent.Invoke(this);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        dragOffset = mousePosition - (Vector2)transform.position;

        isDragging = true;

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        currentSlot?.ReleaseCard();
        transform.SetParent(cardHandler.gameObject.transform, false);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - dragOffset;
        transform.position = targetPosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        EndDragEvent.Invoke(this);
        isDragging = false;

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        // Check if released over a CardSlot
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Mouse.current.position.ReadValue()
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, raycastResults);

        var slot = raycastResults
            .Select(result => result.gameObject.GetComponent<CardSlot>())
            .Where(slot => slot != null)
            .LastOrDefault();

        if (slot != null && slot.CanSlotCard(this))
        {
            currentSlot = slot;
            slot.ReceivedCard(this);
        }
        else
        {
            currentSlot?.ReceivedCard(this);
        }
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
