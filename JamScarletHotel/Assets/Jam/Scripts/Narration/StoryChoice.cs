using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class StoryChoice
{
    [Header("Text")] 
    public string actionName = "Do something";
    public Dialogue response;

    [Header("Conditions")]
    public bool NeedSpecificCard = false;
    [ShowIf("NeedSpecificCard")]
    public ECardType CardTypeNeeded = ECardType.Any;
    [ShowIf("NeedSpecificCard")]
    public EInfluence InfluenceNeeded = EInfluence.None;

    [Header("Conclusion")] 
    public StoryBlocData NextBloc;
    [SerializeField, Required]
    private BaseCardData[] lootCards;
    public ICardData[] LootCards => lootCards.OfType<ICardData>().ToArray();
}