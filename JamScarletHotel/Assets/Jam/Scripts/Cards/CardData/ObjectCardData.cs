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
    private EObjectState defaultState;
    public EObjectState DefaultState => defaultState;

    protected override string GetScoName() => $"ObjectCard_{Title.Replace(" ", "_").Replace("/", "_")}";
}
