using System.Linq;
# if UNITY_EDITOR
using UnityEditor;
#endif

namespace MornGlobal
{
    internal static class MornGlobalUtil
    {
#if UNITY_EDITOR
        [MenuItem("Tools/PreloadAssetsの最適化")]
        private static void OptimizePreloadAsset()
        {
            var preloadedAssets = PlayerSettings.GetPreloadedAssets().ToList();
            preloadedAssets.RemoveAll(x => x == null);
            PlayerSettings.SetPreloadedAssets(preloadedAssets.ToArray());
        }
#endif
    }
}