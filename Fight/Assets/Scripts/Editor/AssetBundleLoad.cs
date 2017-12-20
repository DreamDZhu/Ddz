using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  AssetBundle的加载，采用字典存贮依赖项，避免频繁地加载和卸载AssetBundle
/// </summary>
public class AssetBundleLoad : MonoBehaviour {

    // Manifest for all the AssetBundles in the build.
    private static AssetBundleManifest manifest = null;
    //存储AssetBundle字典
    private static Dictionary<string, AssetBundle> assBundleDic = new Dictionary<string, AssetBundle>();
    private void OnGUI()
    {
        if (GUILayout.Button("Load as"))
        {
            
        }
    }

    /// <summary>
    /// 读取AssetBundle
    /// </summary>
    /// <param name="Url"></param>
    /// <returns></returns>
    public AssetBundle LoadAssetBundle(string Url)
    {
        return null;
    }

}
