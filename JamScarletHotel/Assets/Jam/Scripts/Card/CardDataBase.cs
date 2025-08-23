using NaughtyAttributes;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public abstract class CardDataBase : ScriptableObject
{
    [Header("Base Info")]
    [SerializeField] private string title = string.Empty;
    [SerializeField, ShowAssetPreview(256,256)] private Sprite artwork;
    [SerializeField, Multiline(3)] private string description;
    [SerializeField] private EInfluence influence;

    public Sprite Artwork => artwork;
    public string Title => title;
    public string Description => description;
    public EInfluence Influence => influence;

    protected virtual string GetScoName() => $"Card_{title.Replace(" ", "_").Replace("/", "_")}";

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
