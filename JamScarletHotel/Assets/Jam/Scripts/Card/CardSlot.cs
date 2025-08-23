using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using NaughtyAttributes;

public class CardSlot : MonoBehaviour
{    
    private RectTransform rectTransform;

    [Header("Info")]
    [field: SerializeField, ReadOnly] public Card CurrentCard { get; private set; } = null;
    [field: SerializeField] public ECardType AcceptedType { get; private set; } = ECardType.Any;
    [field: SerializeField] public EInfluence RequiredInfluences { get; private set; } = 0;
    public bool IsOccupied => CurrentCard != null;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public bool CanSlotCard(Card card)
    {
        if (IsOccupied) return false;
        if (card == null) return false;
        if(card.CardData == null) return false;

        var data = card.CardData;
        // Check if the card type matches the accepted type
        if ((data.CardType & AcceptedType) == 0) return false;

        // Check if the card's influence meets the required influence
        if ((data.Influence & RequiredInfluences) != RequiredInfluences) return false;

        return true;
    }

    public void ReceivedCard(Card card)
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
    
    public void SwapWith(CardSlot otherSlot)
    {
        if(otherSlot == null) return;

        Card tempCard = CurrentCard;
        CurrentCard = otherSlot.CurrentCard;
        otherSlot.CurrentCard = tempCard;
    }
}