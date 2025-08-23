using System;
using UnityEngine;

public interface ICardData
{
    Sprite Artwork { get; }
    string Title { get; }
    string Description { get; }
    EInfluence Influence { get; }

    ECardType CardType { get; }
    public bool IsPeopleCard(ECardType cardType) => (ECardType.People & cardType) != 0;
}
public enum ECardType
{
    Object = 1 << 0, // 1
    Client = 1 << 1, // 2
    Employee = 1 << 2, // 4

    People = Client | Employee, // 6
}

[Flags]
public enum EInfluence
{
    Brutality = 1 << 0, // 1
    Seduction = 1 << 1, // 2
    Richness = 1 << 2, // 4
    Occult = 1 << 3, // 8
    Addiction = 1 << 4, // 16
    Artistic = 1 << 5, // 32
}