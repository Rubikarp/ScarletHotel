using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine;

public class CardEvent : UnityEvent<BaseGameCard> { }
public interface ICardSlot
{
    public bool CanSlotCard(BaseGameCard card);
    public void ReceivedCard(BaseGameCard card);
    public void ReleaseCard(BaseGameCard card);

    public CardEvent OnCardDataChange { get; }
}

[RequireComponent(typeof(RectTransform))]
public class CardSlot : MonoBehaviour, ICardSlot
{
    public RectTransform rect { get; private set; }

    [Header("Info")]
    [field: SerializeField, ReadOnly]
    public BaseGameCard CurrentCard { get; private set; } = null;
    [field: SerializeField] public SlotRequirement Requirement { get; private set; } = new SlotRequirement(ECardType.Any, EInfluence.None);
    public bool IsOccupied => CurrentCard != null;

    [Header("Event")]
    [field: SerializeField] public CardEvent OnCardDataChange { get; private set; } = new CardEvent();
    [field: SerializeField] public CardEvent onCardReceived { get; private set; } = new CardEvent();
    [field: SerializeField] public CardEvent onCardReleased { get; private set; } = new CardEvent();

    protected virtual void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    protected virtual void OnValidate()
    {
        rect = GetComponent<RectTransform>();
    }

    public bool CanSlotCard(BaseGameCard card)
    {
        //Error checks
        if (card == null)
        {
            Debug.LogError("Cannot slot null card.");
            return false;
        }
        if (card.CardData == null)
        {
            Debug.LogError("CardManipulation data is null.");
            return false;
        }

        if (IsOccupied) return false;
        var data = card.CardData;
        // Check if the card type matches the accepted type
        if ((data.CardType & Requirement.CardType) == 0) return false;

        // Check if the card's influence meets the required influence
        if ((data.Influence & Requirement.Influence) != Requirement.Influence) return false;

        return true;
    }
    public void SwapWith(CardSlot otherSlot)
    {
        if (otherSlot == null) return;

        BaseGameCard tempCard = CurrentCard;
        ReceivedCard(otherSlot.CurrentCard);
        otherSlot.ReceivedCard(tempCard);
    }

    public void ReceivedCard(BaseGameCard card)
    {
        if(card == null) 
        {
            Debug.LogError("Try to receive null Card", this);
            return;
        }
        if (!CanSlotCard(card))
        {
            Debug.LogError("Cannot slot card: slot is already occupied or card does not meet conditions.");
            return;
        }

        CurrentCard = card;
        CurrentCard.FitIntoSlot(this);
        OnCardDataChange?.Invoke(card);
        onCardReceived?.Invoke(card);
    }
    public void ReleaseCard() => ReleaseCard(CurrentCard);
    public void ReleaseCard(BaseGameCard card)
    {
        if (card == null)
        {
            Debug.LogError($"Try release null CardManipulation", this);
            return;
        }
        if (CurrentCard != card)
        {
            Debug.LogError($"This Slot doesn't contain {card.name}", this);
            return;
        }

        CurrentCard.ReleaseFromSlot();
        CurrentCard = null;
        OnCardDataChange?.Invoke(card);
        onCardReleased?.Invoke(card);
    }

    public void SetRequirement(SlotRequirement requirement) => SetRequirement(requirement.CardType, requirement.Influence);
    public void SetRequirement(ECardType requiredType = ECardType.Any, EInfluence requiredInfluence = EInfluence.None)
    {
        Requirement = new SlotRequirement(requiredType, requiredInfluence);

        if (CurrentCard == null) return;
        if (!CanSlotCard(CurrentCard))
        {
            ReleaseCard();
        }
    }
}