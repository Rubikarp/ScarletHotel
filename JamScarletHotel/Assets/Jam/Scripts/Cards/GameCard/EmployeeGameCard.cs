using Sirenix.OdinInspector;
using UnityEngine;

public class EmployeeGameCard : BaseGameCard
{
    public EmployeeCardData Data => (EmployeeCardData)currentData;

    [Header("Job Info")]
    [SerializeField]
    public EJob CurrentJob;
    [SerializeField]
    public int CurrentSalary;

    [Button("Reset to default state")]
    protected override void LoadData()
    {
        CurrentJob = Data.DefaultJob;
        CurrentSalary = Data.DefaultSalary;
    }
    protected override bool IsValidCardData()
    {
        if (currentData == null)
        {
            Debug.LogWarning("CardManipulation data is not set on " + gameObject.name, this);
            return false;
        }

        if ((CardData.CardType & ECardType.Employee) == 0)
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
