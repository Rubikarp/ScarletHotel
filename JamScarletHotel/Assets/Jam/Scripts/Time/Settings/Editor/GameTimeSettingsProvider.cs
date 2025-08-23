
using UnityEditor;

public class GameTimeSettingsProvider : SettingProviderBase<GameTimeSettings>
{
    public GameTimeSettingsProvider(string path, SettingsScope scope) : base(path, scope) { }

    [SettingsProvider]
    public static SettingsProvider GetSettingsProvider() => CreateProviderForProjectSettings();
}
