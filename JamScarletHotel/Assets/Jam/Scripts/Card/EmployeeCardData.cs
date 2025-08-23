using UnityEngine;

[CreateAssetMenu(fileName = "NewEmployee", menuName = "Card/EmployeeCardData")]
public class EmployeeCardData : CardDataBase, ICardData, IPeopleCardData
{
    //Fixed Properties for Employee Card Data
    public string Name => Title;
    public ECardType CardType => ECardType.Employee;

    [Header("People Info")]
    [SerializeField]
    private EPeopleRace race;
    public EPeopleRace Race => race;

    [SerializeField]
    private EPeopleState state;
    public EPeopleState State =>state;

    [SerializeField]
    private EPeopleApparence apparence;
    public EPeopleApparence Apparence => apparence;

    [Header("Job Info")]
    [SerializeField]
    public EJob job;
    [SerializeField]
    public int salary;

    protected override string GetScoName() => $"EmployeeCard_{Title.Replace(" ", "_").Replace("/", "_")}";
}

public enum EJob
{
    Manager,
    HandyMan,
    SexWorker,
}
