using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;

public class StoryManager : Singleton<StoryManager>
{
    private TimeManager timeManager;

    [Header("StoryLineData")]
    [SerializeField] private GameStoryLine gameStoryLinePrefab;
    [SerializeField] private RectTransform gameStoryContainer;

    [Header("StoryLineData")]
    public List<StoryLineData> storyLinesRemaining;
    public List<StoryLineData> storyLinesTriggered;
    public List<StoryLineData> storyLinesFinished;

    [Header("StoryLineData")]
    public List<StoryBlocData> StoryBlocTriggered;
    public List<StoryBlocData> StoryBlocFinished;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        timeManager = TimeManager.Instance;
    }
    private void Update()
    {
        var seasonStoryLine = storyLinesRemaining.Where(sl => sl.seasonStoryStart == timeManager.SeasonIndex).ToArray();
        for (int i = 0; i < seasonStoryLine.Length; i++)
        {
            var storyLine = seasonStoryLine[i];
            if (storyLine.seasonTriggerTime < timeManager.SeasonProgress)
            {
                storyLinesRemaining.Remove(storyLine);
                LaunchStoryLine(storyLine);
            }
        } 
    }

    public void LaunchStoryLine(StoryLineData storyLine)
    {
        storyLinesTriggered.Add(storyLine);
        if(storyLine.storyBlocs.Length <= 0)
        {
            Debug.LogError($"StoryLineData {storyLine.StoryName} doesn't contain any StoryBlocData", this);
        }

        var gameStory = Instantiate(gameStoryLinePrefab, gameStoryContainer);
        gameStory.name = storyLine.name;
        gameStory.LoadStoryLine(storyLine);
        gameStory.StartStoryLine();
    }

    [Button]
    public void LoadAllStoryLine()
    {
        storyLinesRemaining = Resources.FindObjectsOfTypeAll<StoryLineData>().ToList();
        OrderStoryLine();
    }
    public void OrderStoryLine()
    {
        storyLinesRemaining.OrderBy(sl => sl.seasonStoryStart + sl.seasonTriggerTime).ToList();
    }
}
