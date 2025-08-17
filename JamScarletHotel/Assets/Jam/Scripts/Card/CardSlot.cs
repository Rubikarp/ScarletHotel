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

    public bool IsOccupied => CurrentCard != null;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public bool CanSlotCard(Card card)
    {
        //TODO: Check if the card can be slotted in this slot via conditions
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