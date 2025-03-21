using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MornGlobal
{
    internal sealed class MornGlobalHelper
    {
        private readonly IMornGlobal _mornGlobal;

        public MornGlobalHelper(IMornGlobal mornGlobal)
        {
            _mornGlobal = mornGlobal;
        }

#if UNITY_EDITOR
        private bool ShowLog => EditorPrefs.GetBool($"{_mornGlobal.ModuleName}_ShowLog", true);
        private bool ShowLogWarning => EditorPrefs.GetBool($"{_mornGlobal.ModuleName}_ShowLogWarning", true);
        private bool ShowLogError => EditorPrefs.GetBool($"{_mornGlobal.ModuleName}_ShowLogError", true);
#else
        private bool ShowLog => Debug.isDebugBuild;
        private bool ShowLogWarning => Debug.isDebugBuild;
        private bool ShowLogError => Debug.isDebugBuild;
#endif
        private string Prefix => !string.IsNullOrEmpty(_mornGlobal.Prefix) ? _mornGlobal.Prefix
            : $"[<color=#{ColorUtility.ToHtmlStringRGB(_mornGlobal.ModuleColor)}>{_mornGlobal.ModuleName}</color>] ";

        public void LogInternal(string message)
        {
            if (ShowLog)
            {
                Debug.Log($"{Prefix} {message}");
            }
        }

        public void LogErrorInternal(string message)
        {
            if (ShowLogWarning)
            {
                Debug.LogError($"{Prefix} {message}");
            }
        }

        public void LogWarningInternal(string message)
        {
            if (ShowLogError)
            {
                Debug.LogWarning($"{Prefix} {message}");
            }
        }
    }
}