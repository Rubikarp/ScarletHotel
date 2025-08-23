using UnityEngine;
using VInspector;

[CreateAssetMenu(fileName = "NewClient", menuName = "Card/ClientCardData")]
public class ClientCardData : CardDataBase, ICardData, IPeopleCardData
{
    //Fixed Properties for Employee Card Data
    public string Name => Title;
    public ECardType CardType => ECardType.Client;

    [Header("People Info")]
    [SerializeField]
    private EPeopleRace race;
    [SerializeField]
    private EPeopleState state;
    [SerializeField]
    private EPeopleApparence apparence;

    public EPeopleRace Race => race;
    public EPeopleState State => state;
    public EPeopleApparence Apparence => apparence;

    [Header("Client Info")]
    [field: SerializeField]
    public bool IsEmployable { get; private set; } = false;
    [SerializeField, ShowIf("IsEmployable")]
    public EmployeeCardData employeeCard;
}