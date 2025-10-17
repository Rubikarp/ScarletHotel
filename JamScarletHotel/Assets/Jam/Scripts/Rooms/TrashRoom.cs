using UnityEngine;

public class TrashCan : CardSlot
{
    protected override void Awake()
    {
        base.Awake();
        onCardReceived.AddListener(OnCradToTrash);
    }

    protected override void OnValidate()
    {
        SetRequirement(ECardType.Any, EInfluence.None);
    }

    private void OnCradToTrash(BaseGameCard card)
    {
        card.DestroyCard();
    }
}