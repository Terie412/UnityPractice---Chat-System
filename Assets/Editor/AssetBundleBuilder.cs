using UnityEditor;
using System.IO;
using UnityEngine;

public class AssetBundleBuilder
{
    [MenuItem("Tools/构建 AssetBundle")]
    static void BuildAllAssetBundles()
    {
        string dir = Application.streamingAssetsPath;
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
    }
}