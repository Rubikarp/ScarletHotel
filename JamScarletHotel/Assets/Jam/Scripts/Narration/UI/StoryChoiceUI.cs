using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class StoryChoiceUI : MonoBehaviour
{
    [Header("Choices")]
    [SerializeField] private StoryChoice defaultChoice;
    [SerializeField] private StoryChoice[] otherChoiceAvailable;
    [field: SerializeField, ReadOnly] public StoryChoice CurrentChoice { get; private set; }

    [SerializeField] private TextMeshProUGUI situationTextSlot;
    [SerializeField] private SimpleButton defaultChoiceButton;

    [Header("Card Slot")]
    [SerializeField] private CardSlot CardSlotPrefabs;
    [SerializeField] private RectTransform CardSlotContainer;
    [field: SerializeField] public List<CardSlot> CardSlots { get; private set; } = new List<CardSlot>();

    [Header("Choice Button")]
    [SerializeField] private SimpleButton answerButtonPrefabs;
    [SerializeField] private RectTransform answerButtonContainer;
    [field: SerializeField] public List<SimpleButton> answerButtons { get; private set; } = new List<SimpleButton>();

    [Header("Events")]
    public UnityEvent<StoryChoice> onChoiceMade;

    public void SetupChoices(StoryChoice defaultChoice, StoryChoice[] otherChoices, string situation = null)
    {
        this.defaultChoice = defaultChoice;
        otherChoiceAvailable = otherChoices;
        CurrentChoice = defaultChoice;

        if(situation != null) situationTextSlot.text = situation;
        else situationTextSlot.text = string.Empty;

        answerButtonContainer.DeleteChildrens();
        answerButtons.Clear();

        //default choice set up
        defaultChoiceButton = Instantiate(answerButtonPrefabs, answerButtonContainer);
        defaultChoiceButton.onClick.AddListener(OnValidateChoice);
        defaultChoiceButton.UpdateText(defaultChoice.description);
        answerButtons.Add(defaultChoiceButton);

        //other choices set up
        var pureChoices = otherChoices.Where(x => !x.NeedSpecificCard).ToList();
        for (int i = 0; i < pureChoices.Count; i++)
        {
            var button = Instantiate(answerButtonPrefabs, answerButtonContainer);
            int index = i; // Capture the current index
            button.onClick.AddListener(() =>
            {
                CurrentChoice = pureChoices[index];
                OnValidateChoice();
            });
            button.UpdateText(pureChoices[i].description);
            answerButtons.Add(button);
        }

        //Card slots set up
        var distinctRequirements = otherChoices
            .Where(x => x.NeedSpecificCard)
            .SelectMany(x => x.requirements)
            .Distinct()
            .ToList();
        int slotsNeeded = distinctRequirements.Count;
        CardSlotContainer.DeleteChildrens();
        CardSlots.Clear();
        CardSlots = new List<CardSlot>(slotsNeeded);
        for (int i = 0; i < slotsNeeded; i++)
        {
            var slot = Instantiate(CardSlotPrefabs, CardSlotContainer);
            slot.SetRequirement(distinctRequirements[i]);
            slot.onCardReceived.AddListener(CheckChoice);
            slot.onCardReleased.AddListener(CheckChoice);
            CardSlots.Add(slot);
        }
    }
    private void CheckChoice(BaseGameCard cardSloted)
    {
        foreach (var choice in otherChoiceAvailable)
        {
            bool allRequirementsMet = true;
            foreach (var requirement in choice.requirements)
            {
                if (!CardSlots.Any(slot => slot.Requirement.Equals(requirement) && slot.IsOccupied))
                {
                    allRequirementsMet = false;
                    break;
                }
            }
            if (allRequirementsMet)
            {
                CurrentChoice = choice;
                defaultChoiceButton.UpdateText(CurrentChoice.description);
                return;
            }
        }

        CurrentChoice = defaultChoice;
        defaultChoiceButton.UpdateText(CurrentChoice.description);
    }

    public void OnValidateChoice()
    {
        onChoiceMade?.Invoke(CurrentChoice);
    }
}
