using UnityEngine;
using VInspector;

[CreateAssetMenu(fileName = "CC_New_Client", menuName = "Card/ClientCardData")]
public class ClientCardData : BaseCardData, ICardData, IPeopleCardData
{
    //Fixed Properties for Employee Card Data
    public string CharacterName => Title;
    public ECardType CardType => ECardType.Client;

    [Header("People Info")]
    [SerializeField]
    private EPeopleRace defaultRace;
    public EPeopleRace DefaultRace => defaultRace;

    [SerializeField]
    private EPeopleState defaultState;
    public EPeopleState DefaultState => defaultState;

    [SerializeField]
    private EPeopleApparence defaultApparence;
    public EPeopleApparence DefaultApparence => defaultApparence;

    [field: Header("Client Info")]
    [field: SerializeField, Range(0, 10)]
    public int DefaultSatisfaction { get; private set; } = 5;
    [SerializeField]
    public bool IsEmployable = false;
    [field: SerializeField, ShowIf("IsEmployable")]
    public EmployeeCardData employeeCard { get; private set; }

    protected override string GetScoName() => $"CC_{Title.Replace(" ", "_").Replace("/", "_")}";
}