using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 小地图配置
/// </summary>
[System.Serializable]
public class IconConfig
{
    public Sprite icon;
    public Color color = Color.white;
}

/// <summary>
/// 小地图标识
/// 挂在在需要在小地图上显示的物体
/// </summary>
public class MiniMapObject :MonoBehaviour {

    [SerializeField]
    private IconConfig mIconConfig = new IconConfig();

    /// <summary>
    /// 小地图图标配置
    /// </summary>
    public IconConfig config
    {
        get { return mIconConfig; }
    }

    //启动物体的时候显示图标
    protected virtual void OnEnable()
    {
        //显示该图标
        MiniMapSystem.Instance.RegisterMMObject(this);
        
    }

    //隐藏物体的时候卸载图标
    protected virtual void OnDisable()
    {
        //卸载该图标
        MiniMapSystem.Instance.UnRegisterMMObject(this);
    }


}
