using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ReceptionUI : MonoBehaviour
{
    [Header("Data")]
    [SerializeField, InlineEditor] private StoryBlocData currentBloc;
    [SerializeField] private StoryChoice currentChoice;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI titleTextSlot;
    [SerializeField] private StoryChoiceUI choiceUI;
    [SerializeField] private LootUI lootUI;

    public UnityEvent<StoryBlocData> onStoryBlocEnd = new UnityEvent<StoryBlocData>();

    private void Awake()
    {
        choiceUI.onChoiceMade.AddListener(OnValidateChoice);
        lootUI.onLootCollected.AddListener(OnLootCollected);
    }
    private void OnValidate()
    {
        if (currentBloc != null)
        {
            currentChoice = currentBloc.defaultChoice;
        }
    }

    [Button] private void DEBUG_LaunchCurrentBloc() => LaunchStoryBloc(currentBloc);
    public void LaunchStoryBloc(StoryBlocData data)
    {
        currentBloc = data;
        currentChoice = data.defaultChoice;

        gameObject.SetActive(true);
        lootUI.gameObject.SetActive(false);
        choiceUI.gameObject.SetActive(true);

        choiceUI.SetupChoices(data.defaultChoice, data.otherChoiceAvailable, data.situation);

        titleTextSlot.text = data.blocName;
    }

    private void OnValidateChoice(StoryChoice choiceMade)
    {
        currentChoice = choiceMade;

        lootUI.gameObject.SetActive(true);
        choiceUI.gameObject.SetActive(false);
        lootUI.SetupCardSlots(currentChoice);
    }
    private void OnLootCollected()
    {
        gameObject.SetActive(false);
        lootUI.gameObject.SetActive(false);
        onStoryBlocEnd?.Invoke(currentChoice.NextBloc);
        if (currentChoice.NextBloc != null)
        {
            LaunchStoryBloc(currentChoice.NextBloc);
        }
    }
}
