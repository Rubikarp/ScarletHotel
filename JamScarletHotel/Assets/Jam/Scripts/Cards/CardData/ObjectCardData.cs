using UnityEngine;

public enum  EObjectState
{
    Clean,
    Dirty,
    Bloody,
    Burned,
    Broken,
}

[CreateAssetMenu(fileName = "OC_New_Object", menuName = "Card/ObjectCardData")]
public class ObjectCardData : BaseCardData, ICardData
{
    public ECardType CardType => ECardType.Object;

    [Header("Object Info")]
    [SerializeField]
    private EObjectState defaultState;
    public EObjectState DefaultState => defaultState;

    protected override string GetScoName() => $"OC_{Title.Replace(" ", "_").Replace("/", "_")}";
}
