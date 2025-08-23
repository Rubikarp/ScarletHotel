public interface IPeopleCardData
{
    string Name { get; }
    EPeopleRace Race { get; }
    EPeopleState State { get; }
    EPeopleApparence Apparence { get; }
}

public enum EPeopleRace
{
    Human, //0
    Ghost, //1
    Vampire, //2
    Werewolf //3
}

public enum EPeopleState
{
    WellBeing = 0, 
    Sick = 1,
    Dying = 2,
    Dead = 3,
}
public enum EPeopleApparence
{
    Normal = 0,
    Dirty = 1,
    Bloody = 2,
    WellDressed = 3,
}
public enum EPeopleInfluence
{
    Brutality = 1 << 0, // 1
    Seduction = 1 << 1, // 2
    Richness = 1 << 2, // 4
    Occult = 1 << 3, // 8
    Addiction = 1 << 4, // 16
    Artistic = 1 << 5, // 32
}