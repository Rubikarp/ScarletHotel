using System;
using UnityEngine;

public enum  EObjectState
{
    Clean,
    Dirty,
    Bloody,
    Burnt,
    Broken,
}
[Flags]
public enum EObjectType
{
    Weapon = 1 <<0 ,
    Drug = 1 << 2,
    Contract = 1 << 3,
    Tool = 1 << 4,
}

[CreateAssetMenu(fileName = "NewObject", menuName = "Card/ObjectCardData")]
public class ObjectCardData : BaseCardData, ICardData
{
    public ECardType CardType => ECardType.Object;

    [Header("Object Info")]
    [SerializeField]
    private EObjectState defaultState;
    public EObjectState DefaultState => defaultState;

    [SerializeField]
    private EObjectType defaultType;
    public EObjectType DefaultType => defaultType;

    protected override string GetScoName() => $"ObjectCard_{Title.Replace(" ", "_").Replace("/", "_")}";
}
