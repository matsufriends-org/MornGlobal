using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MornGlobal
{
    public abstract class MornGlobalBase<T> : ScriptableObject, IMornGlobal where T : MornGlobalBase<T>
    {
        public static T I { get; private set; }
        private MornGlobalHelper _helper;
        private MornGlobalHelper Helper => _helper ??= new MornGlobalHelper(this);
        string IMornGlobal.ModuleName => ModuleName;
        string IMornGlobal.Prefix => Prefix;
        Color IMornGlobal.ModuleColor => ModuleColor;
        protected abstract string ModuleName { get; }
        protected virtual string Prefix => "";
        protected virtual Color ModuleColor => Color.green;

        private void OnEnable()
        {
            I = (T)this;
            LogInternal($"{ModuleName}/{typeof(T).Name}を読み込みました。");
#if UNITY_EDITOR
            var preloadedAssets = PlayerSettings.GetPreloadedAssets().ToList();
            if (preloadedAssets.Contains(I) && preloadedAssets.Count(x => x is T) == 1)
            {
                return;
            }

            preloadedAssets.RemoveAll(x => x is T);
            preloadedAssets.Add(I);
            PlayerSettings.SetPreloadedAssets(preloadedAssets.ToArray());
            LogInternal($"{ModuleName}/{typeof(T).Name}をPreloadedAssetsに追加しました。");
#endif
        }

        private void OnDisable()
        {
            I = null;
            LogInternal($"{ModuleName}/{typeof(T).Name}をアンロードしました。");
        }

        protected void LogInternal(string message)
        {
            Helper.LogInternal(message);
        }

        protected void LogErrorInternal(string message)
        {
            Helper.LogErrorInternal(message);
        }

        protected void LogWarningInternal(string message)
        {
            Helper.LogWarningInternal(message);
        }
    }
}