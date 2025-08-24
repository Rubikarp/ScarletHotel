using NaughtyAttributes;
using UnityEngine.Events;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    private RectTransform rectTransform;

    [Header("Info")]
    [field: SerializeField, ReadOnly] public BaseGameCard CurrentCard { get; private set; } = null;
    [field: SerializeField] public ECardType AcceptedType { get; private set; } = ECardType.Any;
    [field: SerializeField] public EInfluence RequiredInfluences { get; private set; } = 0;
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
            Debug.LogError("Card data is null.");
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
        card.transform.SetParent(this.transform, false);
        card.transform.localPosition = Vector3.zero;
    }
    public void ReleaseCard()
    {
        CurrentCard = null;
    }

    public void SetRequirement(ECardType requiredType = ECardType.Any, EInfluence requiredInfluence = 0)
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