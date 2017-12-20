using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 小地图管理器
/// </summary>
public class MiniMapSystem : SingletonBehaviour<MiniMapSystem> {


    protected List<MiniMapIcon> m_iconsPool = new List<MiniMapIcon>();//图标缓存池
    protected Dictionary<MiniMapObject, MiniMapIcon> m_Objs_iconsDict = new Dictionary<MiniMapObject, MiniMapIcon>();

    [SerializeField]
    protected Transform mMapFocus; //小地图玩家位置

    [SerializeField]
    protected RectTransform mIconsRoot;//图标的主节点

    [SerializeField]
    protected GameObject mIconPrefab;//图标预设体

    protected Vector3 minimapCenter
    {
        get { return mIconsRoot.rect.center; }
    }

    [SerializeField]
    protected float maxIconDistance = 100f; //小地图上显示的范围

    [SerializeField]
    protected float scale = 1f;  //显示的图标比例
    
    protected virtual void Update()
    {
        int count = m_iconsPool.Count;
        for (int i = 0; i < count; i++)
        {
            var icon = m_iconsPool[i];
            icon.gameObject.SetActive(CheckVisibility(icon)); //显示是否可见


            icon.rectTransform.anchoredPosition = ConvertPosition(icon.target.transform.position) * scale;//设置位置
        }
    }


    /// <summary>
    /// 注册小地图中的物体
    /// </summary>
    /// <param name="obj"></param>
    internal void RegisterMMObject(MiniMapObject obj)
    {

        var icon = CreateIcon(obj);
        m_iconsPool.Add(icon);
        m_Objs_iconsDict.Add(obj, icon);
    }

    /// <summary>
    /// 卸载小地图中的物体
    /// </summary>
    /// <param name="obj"></param>
    internal void UnRegisterMMObject(MiniMapObject obj)
    {

        MiniMapIcon icon;
        m_Objs_iconsDict.TryGetValue(obj, out icon);
        if (!icon)
        {
            Debug.LogError("Trying to unregister icon that is not registered, how did this happen?");
            return;
        }
        m_iconsPool.Remove(icon);
    }

    protected virtual MiniMapIcon CreateIcon(MiniMapObject mmobj)
    {
        //不存在预设图标
        if (!mIconPrefab)
        {
            Debug.Log("Icon prefab is null, aborting icon construction.");
            return null;
        }

        //实例化一个图标
        mIconPrefab.SetActive(false);
        var go = Instantiate(mIconPrefab, mIconsRoot, false);
        var icon = go.GetComponent<MiniMapIcon>();
        icon.target = mmobj;
        icon.gameObject.SetActive(true);
        mIconPrefab.SetActive(false);
        return icon;
    }

    /// <summary>
    /// 检查图标是否可见
    /// </summary>
    /// <param name="icon"></param>
    /// <returns></returns>
    protected virtual bool CheckVisibility(MiniMapIcon icon)
    {
        return Vector3.Distance(icon.rectTransform.anchoredPosition, Vector3.zero) < maxIconDistance;
    }

    /// <summary>
    /// 获取小地图的大小
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    protected virtual Vector3 ConvertPosition(Vector3 _position)
    {
        //  Vector3 temp = _position + transform.forward;
        
        Vector3 transformed = mMapFocus.transform.InverseTransformVector(_position);
        return new Vector3(transformed.x, transformed.z, 0);
    }

}
