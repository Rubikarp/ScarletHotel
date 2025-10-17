using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotHolder : MonoBehaviour, ICardSlot
{
    [field: Header("Component")]
    [field: SerializeField] public CardSlot CardSlotPrefab { get; private set; }
    [field: SerializeField] public Transform CardSlotParent { get; private set; }

    [field: Header("Settings")]
    [field: SerializeField] public ECardType AcceptedType { get; private set; } = ECardType.Any;
    [field: SerializeField] public EInfluence RequiredInfluences { get; private set; } = EInfluence.None;
    [field: SerializeField, Tooltip("Negative = Infinite")] public int SlotLimit { get; private set; } = -1;
    [field: SerializeField] public List<CardSlot> cardSlots { get; private set; } = new List<CardSlot>(8);

    public bool HasNoLimit => SlotLimit <= 0;

    [Header("Event")]
    public CardEvent onCardDataChange;
    public CardEvent OnCardDataChange => onCardDataChange;

    public bool CanSlotCard(BaseGameCard card)
    {
        if(!HasNoLimit) return true;

        return cardSlots.Count(slot => slot.IsOccupied == true) >= SlotLimit;
    }

    public void ReceivedCard(BaseGameCard card)
    {
        if (!CanSlotCard(card))
        {
            Debug.LogError("Cannot slot card: no slot available slot", this);
            return;
        }

        var slot = cardSlots.Where(slot => !slot.IsOccupied).FirstOrDefault();
        if (slot == null) 
        {
            slot = AddNewSlot();
        }
        slot.ReceivedCard(card);
        onCardDataChange?.Invoke(card);
    }
    public void ReleaseCard(BaseGameCard card)
    {
        var slot = cardSlots.Where(sloat => sloat.CurrentCard == card).FirstOrDefault();

        if (slot != null)
        {
            Debug.LogError($"Cannot find {card.name} in any slot", this);
            return;
        }

        slot.ReleaseCard(card);
        onCardDataChange?.Invoke(card);
    }


    private CardSlot AddNewSlot()
    {
        var slot = Instantiate(CardSlotPrefab, CardSlotParent);
        slot.SetRequirement(AcceptedType, RequiredInfluences);
        cardSlots.Add(slot);
        return slot;
    }

    private void Start()
    {
        if (!HasNoLimit)
        {
            if(cardSlots.Count > SlotLimit)
            {
                //Remove excess slot
                var keep = cardSlots.Take(SlotLimit);
                var excess = cardSlots.Skip(SlotLimit);
                int nbrOfSlotToRemove = excess.Count();
                for (int i = nbrOfSlotToRemove - 1; i >= 0; i--)
                {
                    Destroy(excess.Last().gameObject);
                }
                cardSlots = keep.ToList();
            }
            else
            {
                //Add missing Slot
                int nbrOfSlotToAdd = cardSlots.Count - SlotLimit;
                for (int i = 0; i < nbrOfSlotToAdd; i++)
                {
                    AddNewSlot();
                }
            }
        }
        //Force all slot to respect condition
        foreach (var slot in cardSlots)
        {
            slot.SetRequirement(AcceptedType, RequiredInfluences);
        }
    }

}
