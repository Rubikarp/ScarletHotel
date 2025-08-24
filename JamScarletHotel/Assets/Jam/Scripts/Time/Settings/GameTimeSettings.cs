using SettingsTools;
using UnityEngine;

[GameSettings(typeof(GameTimeSettings))]
public class GameTimeSettings : SingletonSCO<GameTimeSettings>
{
    [field: SerializeField, Range(10, 600)] public float DayDuration { get; private set; } = 30f;
    [field: SerializeField, Range(5, 30)] public float SeasonDuration { get; private set; } = 14f;
    [field: SerializeField, Range(5, 15)] public float WeekDuration { get; private set; } = 7f;
}

