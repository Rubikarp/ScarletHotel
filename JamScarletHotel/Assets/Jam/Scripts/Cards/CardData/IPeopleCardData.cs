public interface IPeopleCardData
{
    string Name { get; }
    EPeopleRace DefaultRace { get; }
    EPeopleState DefaultState { get; }
    EPeopleApparence DefaultApparence { get; }
}

public enum EPeopleRace
{
    Human = 0, 
    Ghost = 1,
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