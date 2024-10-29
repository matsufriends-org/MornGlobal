using UnityEditor;
using UnityEngine;

namespace MornGlobal
{
    [InitializeOnLoad]
    public static class MornGlobalPreload
    {
        static MornGlobalPreload()
        {
            var assets = PlayerSettings.GetPreloadedAssets();
            foreach (var asset in assets)
            {
                if (asset is ScriptableObject scriptableObject)
                {
                    scriptableObject.hideFlags = HideFlags.None;
                    EditorUtility.SetDirty(scriptableObject);
                }
            }
        }
    }
}