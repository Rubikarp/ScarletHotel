using SettingsTools;
using UnityEngine;

[GameSettings(typeof(GameTimeSettings))]
public class GameTimeSettings : SingletonSCO<GameTimeSettings>
{
    [field: SerializeField, Range(10, 600)] public float SeasonDuration { get; private set; } = 300f;
}
