using System.IO;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Luban.Editor
{
    public partial class LubanExportConfig
    {
        public static LubanExportConfig Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = GetOrCreate();
                }

                return _instance;
            }
        }

        private static LubanExportConfig _instance;

        public static LubanExportConfig GetOrCreate()
        {
            var guids = AssetDatabase.FindAssets("t:LubanExportConfig");

            if(guids.Length > 0)
            {
                Debug.LogWarning("Found multiple Luban assets, using the first one");
            }

            switch(guids.Length)
            {
                case 0:
                    var setting = CreateInstance<LubanExportConfig>();

                    if(!Directory.Exists("Assets/Editor"))
                    {
                        Directory.CreateDirectory("Assets/Editor");
                    }

                    AssetDatabase.CreateAsset(setting, "Assets/Editor/LubanExportConfig.asset");
                    return setting;

                default:
                    var path = AssetDatabase.GUIDToAssetPath(guids[0]);
                    return AssetDatabase.LoadAssetAtPath<LubanExportConfig>(path);
            }
        }
    }

    static class LubanExportConfig_SettingsRegister
    {
        private static PropertyTree _PROPERTY;

        [SettingsProvider]
        public static SettingsProvider Create()
        {
            var provider = new SettingsProvider("Project/Luban", SettingsScope.Project)
            {
                label = "Luban",
                guiHandler = _ =>
                {
                    if(_PROPERTY is null)
                    {
                        var setting = LubanExportConfig.GetOrCreate();
                        var so      = new SerializedObject(setting);
                        _PROPERTY = PropertyTree.Create(so);
                    }

                    _PROPERTY.Draw();
                }
            };

            return provider;
        }
    }
}