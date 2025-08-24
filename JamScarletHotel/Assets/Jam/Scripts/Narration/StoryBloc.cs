using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "SB_New_Story_Bloc\"", menuName = "Narration/StoryBloc")]
public class StoryBloc : ScriptableObject
{
    public string blocName = "New Story Bloc";
    public List<StoryChoice> aswerPossibles = new List<StoryChoice>(4);

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
