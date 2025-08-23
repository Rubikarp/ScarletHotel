using UnityEngine;

public enum  EObjectState
{
    Clean,
    Dirty,
    Bloody,
    Burned,
    Broken,
}

[CreateAssetMenu(fileName = "NewObject", menuName = "Card/ObjectCardData")]
public class ObjectCardData : CardDataBase, ICardData
{
    public ECardType CardType => ECardType.Object;

    [Header("Object Info")]
    [SerializeField]
    private EObjectState state;
    public EObjectState State => state;

    protected override string GetScoName() => $"ObjectCard_{Title.Replace(" ", "_").Replace("/", "_")}";
}
