using NaughtyAttributes;
using UnityEngine;

public class ClientGameCard : BaseGameCard
{
    public ClientCardData Data => (ClientCardData)currentData;

    [Header("Client Info")]
    [SerializeField, Range(0, 10)]
    public int CurrentSatisfaction;
    [Button("Reset to default state")]
    protected override void LoadData()
    {
        CurrentSatisfaction = Data.DefaultSatisfaction;
    }
    protected override bool IsValidCardData()
    {
        if (currentData == null)
        {
            Debug.LogWarning("CardManipulation data is not set on " + gameObject.name, this);
            return false;
        }

        if ((CardData.CardType & ECardType.Client) == 0)
        {
            Debug.LogWarning("ObjectGameCard requires an ObjectCardData ", this);
            currentData = null;
            return false;
        }

        return true;
    }
    protected override void OnTimerEnd()
    {
    }
}