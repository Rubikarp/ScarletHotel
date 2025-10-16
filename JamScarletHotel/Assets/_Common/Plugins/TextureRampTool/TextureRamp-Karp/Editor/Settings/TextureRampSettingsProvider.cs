#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Karprod
{
    public class TextureRampSettingsProvider : SettingsProvider
	{
		public TextureRampSettingsProvider(string path, SettingsScope scope) : base(path, scope) { }

		static Editor editor;

		[SettingsProvider]
		public static SettingsProvider CreateProviderForProjectSettings()
		{
			TextureRampSettingsProvider trsp = new TextureRampSettingsProvider("_KarpProd/TextureRamp", SettingsScope.Project);
			trsp.guiHandler = OnProviderGUI;

			return trsp;
		}
		public static void OnProviderGUI(string context)
		{
			TextureRampSettings trs = Resources.Load("TextureRampSettings") as TextureRampSettings;
			if (trs is null) 
			{ 
				trs = CreateSettingsAsset();
			}
			if (!editor)
			{
				Editor.CreateCachedEditor(trs, null, ref editor);
			}
			editor.OnInspectorGUI();
		}

		public static TextureRampSettings CreateSettingsAsset()
		{
			var path = KarpToolUtilities.FindScriptFolder("TextureRampSettings", true);
			if (!AssetDatabase.IsValidFolder(path + "Resources/"))
			{
				var folder = path.Remove(path.Length-1);
				Debug.Log(folder);
				AssetDatabase.CreateFolder(folder, "Resources");
			}
			path += "Resources/TextureRampSettings.asset";
			Debug.Log(path);
			var trs = ScriptableObject.CreateInstance<TextureRampSettings>();
			AssetDatabase.CreateAsset(trs, path);
			//
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			return trs;
		}
	}
}
#endif