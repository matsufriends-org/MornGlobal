using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MornGlobal
{
    public abstract class MornGlobalBase<T> : ScriptableObject where T : MornGlobalBase<T>
    {
        public static T I { get; private set; }

        private void OnEnable()
        {
            I = (T)this;
            Log("Global Settings Loaded");
#if UNITY_EDITOR
            var preloadedAssets = PlayerSettings.GetPreloadedAssets().ToList();
            if (preloadedAssets.Contains(I) && preloadedAssets.Count(x => x is T) == 1)
            {
                return;
            }

            preloadedAssets.RemoveAll(x => x is T);
            preloadedAssets.Add(I);
            PlayerSettings.SetPreloadedAssets(preloadedAssets.ToArray());
            Log("Global Settings Added to Preloaded Assets!");
#endif
        }

        private void OnDisable()
        {
            I = null;
            Log("Global Settings Unloaded");
        }

        protected abstract bool ShowLog { get; }
        protected abstract string ModuleName { get; }
        protected virtual Color ModuleColor => Color.green;
        private string Prefix => $"[<color=#{ColorUtility.ToHtmlStringRGB(ModuleColor)}>{ModuleName}</color>] ";

        public void Log(string message)
        {
            if (ShowLog)
            {
                Debug.Log($"{Prefix} {message}");
            }
        }

        public void LogError(string message)
        {
            if (ShowLog)
            {
                Debug.LogError($"{Prefix} {message}");
            }
        }

        public void LogWarning(string message)
        {
            if (ShowLog)
            {
                Debug.LogWarning($"{Prefix} {message}");
            }
        }
    }
}