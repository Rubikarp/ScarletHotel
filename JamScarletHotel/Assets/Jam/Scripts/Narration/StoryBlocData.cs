using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "SB_New_Story_Bloc", menuName = "Narration/StoryBlocData")]
public class StoryBlocData : ScriptableObject
{
    public string blocName = "New Story Bloc";
    [TextArea(1,5)]
    public string situation = "Lorem ipsum tkt";

    [ListDrawerSettings(Expanded = true)]
    public StoryChoice[] aswerPossibles;

    protected virtual string GetScoName() => $"SB_{blocName.Replace(" ", "_").Replace("/", "_")}";

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
