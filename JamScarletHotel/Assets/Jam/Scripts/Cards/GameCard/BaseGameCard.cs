using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CardManipulation))]
public abstract class BaseGameCard: MonoBehaviour
{
    private CardHandler cardHandler;
    private CardManipulation card;

    [Header("Components")]
    public GameTimer Timer;
    public CardVisual Visual;

    [SerializeField, ReadOnly]
    protected CardSlot _currentSlot;
    protected CardSlot _lastSlotBuffer;
    public CardSlot CurrentSlot => _currentSlot;
    public void FitIntoSlot(CardSlot slot)
    {
        if (_currentSlot == slot) return;

        _lastSlotBuffer = _currentSlot;
        _currentSlot = slot;

        transform.SetParent(slot.transform, false);
        transform.localPosition = Vector3.zero;
    }
    public void ReleaseFromSlot()
    {
        _lastSlotBuffer = _currentSlot;
        _currentSlot = null;
    }

    [Header("Data")]
    [SerializeField]
    protected ICardData currentData;
    private ICardData previousData;
    public ICardData CardData => currentData;

    public void TryLoadData(ICardData newData)
    {
        currentData = newData;
        if (IsValidCardData())
        {
            LoadData();
        }
        else
        {
            Debug.LogWarning("Invalid card data loaded on " + gameObject.name, this);
            currentData = null;
        }
    }
    protected abstract bool IsValidCardData();
    protected abstract void LoadData();
    protected abstract void OnTimerEnd();
    public void DestroyCard()
    {
        Destroy(Visual.gameObject);
        Destroy(gameObject);
    }

    protected void OnValidate()
    {
        if (!IsValidCardData())
        {
            Debug.LogWarning("Invalid card data loaded on " + gameObject.name, this);
            currentData = null;
            return;
        }

        // Check if same scriptable object
        if (!object.ReferenceEquals(previousData, currentData))
        {
            previousData = currentData;
            LoadData();
        }
    }

    protected virtual void Start()
    {
        cardHandler = CardHandler.Instance;
        card = GetComponent<CardManipulation>();

        card.BeginDragEvent.AddListener(OnDragStart);
        card.EndDragEvent.AddListener(OnDragRelease);

        Timer.OnTimerEnd.AddListener(OnTimerEnd);
    }

    protected void OnDragStart(CardManipulation card)
    {
        CurrentSlot?.ReleaseCard(this);
        transform.SetParent(cardHandler.CardHandlingContainer, false);
    }
    protected void OnDragRelease(CardManipulation card)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Mouse.current.position.ReadValue()
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, raycastResults);

        ICardSlot slot = raycastResults
            .Select(result => result.gameObject.GetComponent<ICardSlot>())
            .Where(slot => slot != null)
            .FirstOrDefault();

        if (slot != null && slot.CanSlotCard(this))
        {
            slot.ReceivedCard(this);
        }
        else
        {
            _lastSlotBuffer?.ReceivedCard(this);
        }
    }
}
