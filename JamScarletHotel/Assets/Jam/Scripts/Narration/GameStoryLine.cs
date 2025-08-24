using UnityEngine;
using NaughtyAttributes;

public class GameStoryLine : MonoBehaviour
{
    [field: SerializeField, ReadOnly] public StoryLineData storyLineData { get; private set; }
    [field: SerializeField, ReadOnly] public StoryBlocData CurrentStoryBloc {  get; private set; }

    public void LoadStoryLine(StoryLineData storyLine)
    {
        storyLine = storyLineData;
    }
    public void StartStoryLine()
    {
        CurrentStoryBloc = storyLineData.storyBlocs[0];

    }


}
