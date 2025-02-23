using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MornGlobal
{
    public abstract class MornGlobalBase<T> : ScriptableObject where T : MornGlobalBase<T>
    {
        public static T I { get; private set; }

        private void OnEnable()
        {
            I = (T)this;
            LogInternal("Global Settings Loaded");
#if UNITY_EDITOR
            var preloadedAssets = PlayerSettings.GetPreloadedAssets().ToList();
            if (preloadedAssets.Contains(I) && preloadedAssets.Count(x => x is T) == 1)
            {
                return;
            }

            preloadedAssets.RemoveAll(x => x is T);
            preloadedAssets.Add(I);
            PlayerSettings.SetPreloadedAssets(preloadedAssets.ToArray());
            LogInternal("Global Settings Added to Preloaded Assets!");
#endif
        }

        private void OnDisable()
        {
            I = null;
            LogInternal("Global Settings Unloaded");
        }
#if UNITY_EDITOR
        protected virtual bool ShowLog => EditorPrefs.GetBool($"{ModuleName}_ShowLog", true);
#else
        protected virtual bool ShowLog { get; }
#endif
        protected abstract string ModuleName { get; }
        protected virtual Color ModuleColor => Color.green;
        protected virtual string Prefix => $"[<color=#{ColorUtility.ToHtmlStringRGB(ModuleColor)}>{ModuleName}</color>] ";
        
        protected void LogInternal(string message)
        {
            if (ShowLog)
            {
                Debug.Log($"{Prefix} {message}");
            }
        }

        protected void LogErrorInternal(string message)
        {
            if (ShowLog)
            {
                Debug.LogError($"{Prefix} {message}");
            }
        }

        protected void LogWarningInternal(string message)
        {
            if (ShowLog)
            {
                Debug.LogWarning($"{Prefix} {message}");
            }
        }
    }
}