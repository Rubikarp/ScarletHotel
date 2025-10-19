using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class StoryBlocUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI Description;
    [SerializeField] private TextMeshProUGUI Reaction;
    [Space]
    [SerializeField] private Button validationButton;
    [SerializeField] private TextMeshProUGUI validationTextSlot;

    [Header("Data")]
    [SerializeField] private StoryBlocData currentData;
    [SerializeField] private StoryChoice currentChoice;

    [Header("Card Slot")]
    [SerializeField] private CardSlot CardSlotPrefabs;
    [SerializeField] private RectTransform CardSlotContainer;
    [field: SerializeField] public List<CardSlot> CardSlots { get; private set; } = new List<CardSlot>();

    public UnityEvent<StoryBlocData> onBlocEnd;

    private void Awake()
    {
        validationButton.onClick.AddListener(OnValidateChoice);
    }

    public void LaunchStoryBloc(StoryBlocData data)
    {
        currentData = data;
        CardSlotContainer.DeleteChildren();
        CardSlots.Clear();

        CardSlots = new List<CardSlot>(data.otherChoiceAvailable.Count(x => x.NeedSpecificCard));
        foreach (var storyChoices in data.otherChoiceAvailable)
        {
            var slot = Instantiate(CardSlotPrefabs, CardSlotContainer);
            slot.SetRequirement(storyChoices.CardTypeNeeded, storyChoices.InfluenceNeeded);
            CardSlots.Add(slot);
        }

        Title.text = data.blocName;
        Description.text = data.situation;
    }

    private void LateUpdate()
    {
        currentChoice = null;
        string action = string.Empty;
        string reaction = string.Empty;
        if (currentData != null)
        {
            for (int i = 0; i < currentData.otherChoiceAvailable.Length; i++)
            {
                var possibility = currentData.otherChoiceAvailable[i];

                var slots = CardSlots.FindAll(c => c.AcceptedType == possibility.CardTypeNeeded && c.RequiredInfluences == possibility.InfluenceNeeded);
                if (slots.Any(c => c.IsOccupied))
                {
                    currentChoice = possibility;
                }
            }
            if(currentChoice == null)
            {
                currentChoice = currentData.defaultChoice;
            }
        }

        if (currentChoice == null)
        {
            validationButton.interactable = false;
            Reaction.text = "Waiting ...";
            validationTextSlot.text = "Object Needed";
        }
        else
        {
            validationButton.interactable = true;
            Reaction.text = currentChoice.response;
            validationTextSlot.text = currentChoice.actionName;
        }
    }

    private void OnValidateChoice()
    {
        if (currentChoice.LootCards.Length > 0)
        {
            for (int i = 0; i < currentChoice.LootCards.Length; i++)
            {
                CardHandler.Instance.SpawnCardInHand(currentChoice.LootCards[i]);
            }
        }
        onBlocEnd?.Invoke(currentChoice.NextBloc);
    }
}
