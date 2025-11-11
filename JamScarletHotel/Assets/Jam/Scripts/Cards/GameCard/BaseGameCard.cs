using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(DragElement))]
public abstract class BaseGameCard: MonoBehaviour
{
    private CardHandler cardHandler;
    public DragElement dragElement;

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
        Visual.transform.SetParent(slot.transform, false);
        transform.localPosition = Vector3.zero;
    }
    public void ReleaseFromSlot()
    {
        Visual.transform.SetParent(transform, false);
        _lastSlotBuffer = _currentSlot;
        _currentSlot = null;
    }

    [Header("Data")]
    [OdinSerialize]
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
        dragElement = GetComponent<DragElement>();

        dragElement.onDragBegin.AddListener(OnDragStart);
        dragElement.onDragEnd.AddListener(OnDragRelease);

        Timer.OnTimerEnd.AddListener(OnTimerEnd);
    }

    protected void OnDragStart(DragElement card, PointerEventData pointerData)
    {
        CurrentSlot?.ReleaseCard(this);
        transform.SetParent(cardHandler.CardHandlingContainer, false);
    }
    protected void OnDragRelease(DragElement card, PointerEventData pointerData)
    {
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
            if(_lastSlotBuffer != null)
                _lastSlotBuffer?.ReceivedCard(this);
            else
                CardHandler.Instance.Inventory.ReceivedCard(this);
        }
    }
}
