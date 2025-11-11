using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LootUI : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private StoryChoice currentChoice;

    [Header("Card Slot")]
    [SerializeField] private TextMeshProUGUI resultatTextSlot;
    [SerializeField] private CardSlot CardSlotPrefabs;
    [SerializeField] private RectTransform CardSlotContainer;
    [field: SerializeField] public List<CardSlot> CardSlots { get; private set; } = new List<CardSlot>();

    [Header("Buttons")]
    public SimpleButton concludeButton;

    [Header("Events")]
    public UnityEvent onLootCollected;

    private void Awake()
    {
        concludeButton.onClick.AddListener(OnCollectAll);
    }
    public void SetupCardSlots(StoryChoice choiceMade)
    {
        currentChoice = choiceMade;
        resultatTextSlot.text = currentChoice.resolution;

        CardSlotContainer.DeleteChildrens();
        CardSlots.Clear();
        CardSlots = new List<CardSlot>(currentChoice.LootCards.Length);
        for (int i = 0; i < currentChoice.LootCards.Length; i++)
        {
            var slot = Instantiate(CardSlotPrefabs, CardSlotContainer);
            CardSlots.Add(slot);

            var card = CardHandler.Instance.SpawnCard(choiceMade.LootCards[i], slot);
        }

        UpdateConcludeButtonText();
    }
    public void OnCollectAll()
    {
        for (int i = CardSlots.Count - 1; i >= 0; i--)
        {
            var slot = CardSlots[i];
            if (slot.CurrentCard != null)
            {
                var card = slot.CurrentCard;
                CardHandler.Instance.Inventory.ReceivedCard(slot.CurrentCard);
                slot.ReleaseCard(slot.CurrentCard);
            }
            CardSlots.Remove(slot);
            Destroy(slot.gameObject);
        }

        UpdateConcludeButtonText();
        onLootCollected?.Invoke();
    }

    private void UpdateConcludeButtonText()
    {
        if (CardSlots.Count<CardSlot>(x => x.CurrentCard != null) > 0)
        {
            concludeButton.UpdateText("Tout collecter");
        }
        else
        {
            if (currentChoice.NextBloc != null)
                concludeButton.UpdateText("Continuer");
            else
                concludeButton.UpdateText("Finir l'événement");
        }
    }
}
