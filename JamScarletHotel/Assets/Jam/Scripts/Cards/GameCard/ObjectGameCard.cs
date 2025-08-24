using UnityEngine;
using NaughtyAttributes;

public class ObjectGameCard : BaseGameCard
{
    public ObjectCardData Data => (ObjectCardData)currentData;
    [Header("Object Info")]
    [SerializeField]
    public EObjectState CurrentState;


    [Button("Reset to default state")]
    protected override void LoadData()
    {
        CurrentState = Data.DefaultState;
    }
    protected override bool IsValidCardData()
    {
        if (currentData == null)
        {
            Debug.LogWarning("CardManipulation data is not set on " + gameObject.name, this);
            return false;
        }

        if ((CardData.CardType & ECardType.Object) == 0)
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
