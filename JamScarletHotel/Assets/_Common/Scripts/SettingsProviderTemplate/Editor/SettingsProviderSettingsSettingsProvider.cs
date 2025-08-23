using UnityEditor;
using UnityEngine;

namespace SettingsTools
{
    public class SettingsProviderSettingsSettingsProvider : SettingProviderBase<SettingsProviderSettings>
    {
        public SettingsProviderSettingsSettingsProvider(string path, SettingsScope scope) : base(path, scope){}

        [SettingsProvider]
        public static SettingsProvider GetSettingsProvider() => CreateProviderForProjectSettings();

    }
}
