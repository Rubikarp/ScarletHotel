using System.Linq;
using NaughtyAttributes;
using AYellowpaper;
using UnityEngine;

[System.Serializable]
public class StoryChoice
{
    [Header("Text")] 
    public string actionName = "Do something";
    public Dialogue response;

    [Header("Conditions")]
    public bool NeedSpecificCard = false;
    [ShowIf("NeedSpecificCard")] public ECardType CardTypeNeeded = ECardType.Any;
    [ShowIf("NeedSpecificCard")] public EInfluence InfluenceNeeded = 0;

    [Header("Conclusion")] 
    public StoryBlocData NextBloc;
    [SerializeField, RequireInterface(typeof(ICardData))] private BaseCardData[] lootCards;
    public ICardData[] LootCards => lootCards.OfType<ICardData>().ToArray();
}