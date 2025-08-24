using NaughtyAttributes;
using UnityEngine.Events;
using UnityEngine;

public interface ICardSlot
{
    public bool CanSlotCard(BaseGameCard card);
    public void ReceivedCard(BaseGameCard card);
    public void ReleaseCard(BaseGameCard card);

}

public class CardSlot : MonoBehaviour, ICardSlot
{
    private RectTransform rectTransform;

    [Header("Info")]
    [field: SerializeField, ReadOnly] public BaseGameCard CurrentCard { get; private set; } = null;
    [field: SerializeField] public ECardType AcceptedType { get; private set; } = ECardType.Any;
    [field: SerializeField] public EInfluence RequiredInfluences { get; private set; } = EInfluence.None;
    public bool IsOccupied => CurrentCard != null;

    [Header("Event")]
    public UnityEvent<ICardData> OnCardDataChange { get; private set; }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
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
        if ((data.CardType & AcceptedType) == 0) return false;

        // Check if the card's influence meets the required influence
        if ((data.Influence & RequiredInfluences) != RequiredInfluences) return false;

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
        if (!CanSlotCard(card))
        {
            Debug.LogError("Cannot slot card: slot is already occupied or card does not meet conditions.");
            return;
        }

        CurrentCard = card;
        CurrentCard.CurrentSlot = this;

        card.transform.SetParent(this.transform, false);
        card.transform.localPosition = Vector3.zero;
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
            Debug.LogError($"This Slot doesn't contain {card.name}",this);
            return;
        }

        CurrentCard.CurrentSlot = null;
        CurrentCard = null;
    }

    public void SetRequirement(ECardType requiredType = ECardType.Any, EInfluence requiredInfluence = EInfluence.None)
    {
        AcceptedType = requiredType;
        RequiredInfluences = requiredInfluence;

        if(CurrentCard == null) return ;
        if (!CanSlotCard(CurrentCard))
        {
            ReleaseCard();
        }
    }
}