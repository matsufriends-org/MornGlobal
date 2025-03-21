using UnityEngine;

namespace MornGlobal
{
    public interface IMornGlobal
    {
        string ModuleName { get; }
        string Prefix { get; }
        Color ModuleColor { get; }
    }
}