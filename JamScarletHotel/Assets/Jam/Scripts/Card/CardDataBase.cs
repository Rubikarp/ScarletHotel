using NaughtyAttributes;
using UnityEngine;

public abstract class CardDataBase : ScriptableObject
{
    [Header("Base Info")]
    private string title;
    [SerializeField, ShowAssetPreview()]
    private Sprite artwork;
    [SerializeField, Multiline(3)]
    private string description;
    [SerializeField]
    private EInfluence influence;

    public Sprite Artwork => artwork;
    public string Title => title;
    public string Description => description;
    public EInfluence Influence => influence;
}
