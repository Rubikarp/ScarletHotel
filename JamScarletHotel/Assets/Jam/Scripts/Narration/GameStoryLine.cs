using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine;
using System;

public class GameStoryLine : MonoBehaviour
{
    [field: SerializeField, InlineEditor(Expanded = true)] public StoryLineData storyLineData { get; private set; }
    [field: SerializeField, InlineEditor(Expanded = true)] public StoryBlocData CurrentStoryBloc {  get; private set; }
    public int currentStoryblockIndex = 0;
    [Space]
    public StoryBlocUI storyWindow;
    public UnityEvent onStoryLineEnd;

    private void Awake()
    {
        storyWindow.onBlocEnd.AddListener(OnBlockFinnished);
    }

    private void OnBlockFinnished(StoryBlocData nextStory)
    {
        if(nextStory == null)
        {
            //Storyline Ended
            onStoryLineEnd?.Invoke();
            Destroy(gameObject);
            return;
        }

        currentStoryblockIndex = Array.IndexOf(storyLineData.storyBlocs, nextStory);
        StartStoryLine();
    }

    public void LoadStoryLine(StoryLineData storyLine)
    {
        storyLineData = storyLine;
        currentStoryblockIndex = 0;
    }
    [Button]
    public void StartStoryLine()
    {
        CurrentStoryBloc = storyLineData.storyBlocs[currentStoryblockIndex];
        storyWindow.LaunchStoryBloc(CurrentStoryBloc);
    }
}
