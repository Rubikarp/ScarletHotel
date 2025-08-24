using UnityEngine;

[CreateAssetMenu(fileName = "CE_New_Employee", menuName = "CardManipulation/EmployeeCardData")]
public class EmployeeCardData : BaseCardData, ICardData, IPeopleCardData
{
    //Fixed Properties for Employee Card Data
    public string CharacterName => Title;
    public ECardType CardType => ECardType.Employee;

    [Header("People Info")]
    [SerializeField]
    private EPeopleRace defaultRace;
    public EPeopleRace DefaultRace => defaultRace;

    [SerializeField]
    private EPeopleState defaultState;
    public EPeopleState DefaultState =>defaultState;

    [SerializeField]
    private EPeopleApparence defaultApparence;
    public EPeopleApparence DefaultApparence => defaultApparence;

    [field: Header("Job Info")]
    [field: SerializeField]
    public EJob DefaultJob { get; private set; }
    [field: SerializeField]
    public int DefaultSalary { get; private set; }

    protected override string GetScoName() => $"CE_{Title.Replace(" ", "_").Replace("/", "_")}";
}

public enum EJob
{
    Manager,
    HandyMan,
    SexWorker,
}
