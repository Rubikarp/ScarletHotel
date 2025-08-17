using UnityEditor;
using UnityEngine;

namespace SettingsTools
{
    public class SettingsGeneratorSettingsProvider : SettingProviderBase<SettingsGenerator>
    {
        public SettingsGeneratorSettingsProvider(string path, SettingsScope scope) : base(path, scope){}

        [SettingsProvider]
        public static SettingsProvider GetSettingsProvider() => CreateProviderForProjectSettings();

    }
}
