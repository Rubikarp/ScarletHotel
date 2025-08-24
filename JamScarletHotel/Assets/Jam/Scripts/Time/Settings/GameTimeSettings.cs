using SettingsTools;
using UnityEngine;

[GameSettings(typeof(GameTimeSettings))]
public class GameTimeSettings : SingletonSCO<GameTimeSettings>
{
    [field: SerializeField, Range(5, 30), Tooltip("SeasonDuration in count in Week")] 
    public int SeasonDuration { get; private set; } = 14;
    [field: SerializeField, Range(5, 15), Tooltip("SeasonDuration in count in Days")] 
    public int WeekDuration { get; private set; } = 7;
    [field: SerializeField, Range(10, 600), Tooltip("SeasonDuration in count in Seconds")] 
    public float DayDuration { get; private set; } = 30f;
}

