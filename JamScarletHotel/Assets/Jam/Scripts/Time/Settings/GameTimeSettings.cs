using SettingsTools;
using UnityEngine;

[GameSettings(typeof(GameTimeSettings))]
public class GameTimeSettings : SingletonSCO<GameTimeSettings>
{
    [field: SerializeField, Range(10, 600)] public float SeasonDuration { get; private set; } = 300f;
    [field: SerializeField, Range(10, 600)] public float NightDuration { get; private set; } = 30f;
}
