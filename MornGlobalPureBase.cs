using UnityEngine;

namespace MornGlobal
{
    public abstract class MornGlobalPureBase<T> : IMornGlobal where T : new()
    {
        private static T _instance;
        public static T I => _instance ??= new T();
        private MornGlobalHelper _helper;
        private MornGlobalHelper Helper => _helper ??= new MornGlobalHelper(this);
        string IMornGlobal.ModuleName => ModuleName;
        string IMornGlobal.Prefix => Prefix;
        Color IMornGlobal.ModuleColor => ModuleColor;
        protected abstract string ModuleName { get; }
        protected virtual string Prefix => "";
        protected virtual Color ModuleColor => Color.green;

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