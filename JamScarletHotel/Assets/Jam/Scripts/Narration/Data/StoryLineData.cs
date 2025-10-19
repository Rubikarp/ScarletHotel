using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "SL_New_Story_Line", menuName = "Narration/StoryLineData")]
public class StoryLineData : ScriptableObject
{
    [field: SerializeField]
    public string StoryName { get; private set; } = "New Story Line";

    [Header("Trigger")]
    [MinValue(0)] public int seasonStoryStart = 0;
    [PropertyRange(0, 1)] public float seasonTriggerTime = 0;

    [Header("Info"), PropertyOrder(2), PropertySpace(8)]
    [ListDrawerSettings(Expanded = true)]
    public StoryBlocData[] storyBlocs;

    protected virtual string GetScoName() => $"SL_{StoryName.Replace(" ", "_").Replace("/", "_")}";

#if UNITY_EDITOR
    private void OnValidate()
    {
        string assetPath = AssetDatabase.GetAssetPath(this);
        string currentName = System.IO.Path.GetFileNameWithoutExtension(assetPath);

        if (currentName != GetScoName())
        {
            AssetDatabase.RenameAsset(assetPath, GetScoName());
            AssetDatabase.SaveAssets();
        }
    }
#endif
}
