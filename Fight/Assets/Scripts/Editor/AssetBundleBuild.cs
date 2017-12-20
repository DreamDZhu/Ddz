using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// assetBundle打包
/// </summary>
public class AssetBundleBuild : MonoBehaviour {

    [MenuItem("AssetBundle Editor/AssetBundleBuild")]
    static void AssetBundlesBuild()
    {
        BuildPipeline.BuildAssetBundles(AssetBundleConfig.ASSETBUNDLE_PATH.Substring(AssetBundleConfig.PROJECT_PATH.Length), BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
    }

}
