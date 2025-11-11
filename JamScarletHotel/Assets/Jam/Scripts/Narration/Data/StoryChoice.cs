using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class StoryChoice
{
    [Header("Text")] 
    public string description = "Do something";
    [TextArea(3, 10)] public string resolution;

    [Header("Conditions")]
    public bool NeedSpecificCard => requirements.Length > 0;
    public SlotRequirement[] requirements = new SlotRequirement[0];

    [Header("Conclusion")] 
    public StoryBlocData NextBloc;
    [SerializeField, Required]
    private BaseCardData[] lootCards;
    public ICardData[] LootCards => lootCards.OfType<ICardData>().ToArray();
}

[System.Serializable]
public struct SlotRequirement
{
    public ECardType CardType;
    public EInfluence Influence;
    public SlotRequirement(ECardType cardType, EInfluence influence)
    {
        CardType = cardType;
        Influence = influence;
    }
    

    public static bool operator ==(SlotRequirement a, SlotRequirement b)
    {
        return a.CardType == b.CardType && a.Influence == b.Influence;
    }
    public static bool operator !=(SlotRequirement a, SlotRequirement b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        if (obj is SlotRequirement other)
        {
            return this == other;
        }
        return false;
    }
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + CardType.GetHashCode();
            hash = hash * 23 + Influence.GetHashCode();
            return hash;
        }
    }
}