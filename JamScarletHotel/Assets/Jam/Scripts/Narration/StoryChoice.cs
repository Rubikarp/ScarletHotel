using System.Linq;
using NaughtyAttributes;
using AYellowpaper;
using UnityEngine;

[System.Serializable]
public class StoryChoice
{
    [Foldout("Text")] public string actionName = "Do something";
    [Foldout("Text")] public Dialogue response;

    [Foldout("Conditions")] public bool NeedSpecificCard = false;
    [Foldout("Conditions"), ShowIf("NeedSpecificCard")] public ECardType CardTypeNeeded = ECardType.Any;
    [Foldout("Conditions"), ShowIf("NeedSpecificCard")] public EInfluence InfluenceNeeded = 0;

    [Foldout("Conclusion")] public StoryBloc NextBloc;
    [Foldout("Conclusion"), SerializeField, RequireInterface(typeof(ICardData))] private ScriptableObject[] lootCards;
    public ICardData[] LootCards => lootCards.OfType<ICardData>().ToArray();
}