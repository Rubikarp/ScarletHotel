using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using AYellowpaper;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CardManipulation))]
public abstract class BaseGameCard: MonoBehaviour
{
    private CardHandler cardHandler;
    private CardManipulation card;

    [Header("Components")]
    public GameTimer Timer;
    public CardVisual Visual;

    [SerializeField, ReadOnly]
    protected CardSlot currentSlot;
    protected CardSlot lastSlotBuffer;
    public CardSlot CurrentSlot
    {
        get => currentSlot;
        set
        {
            lastSlotBuffer = currentSlot = value;
        }
    }

    [Header("Data")]
    [SerializeField, RequireInterface(typeof(ICardData))]
    protected ScriptableObject currentData;
    private ScriptableObject previousData;
    public ICardData CardData => (ICardData)currentData;

    public void TryLoadData(ICardData newData)
    {
        currentData = (ScriptableObject)newData;
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
        CurrentSlot = null;

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
            lastSlotBuffer?.ReceivedCard(this);
        }
    }
}
