using System.Linq;
using System.Collections.Generic;
using NaughtyAttributes;
using AYellowpaper;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "SL_New_Story_Line", menuName = "Narration/StoryLine")]
public class StoryLine : ScriptableObject
{
    public string storyName = "New Story Line";

    [Header("Trigger")]
    [Min(0)] public int seasonStoryStart = 0;
    [Range(0, 1)] public float seasonTriggerTime = 0;

    [Header("Info"), Expandable]
    public StoryBloc[] storyBlocs;

    protected virtual string GetScoName() => $"SL_{storyName.Replace(" ", "_").Replace("/", "_")}";

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
