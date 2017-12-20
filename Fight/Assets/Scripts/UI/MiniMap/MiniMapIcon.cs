using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 小地图图标
/// 根据该物体的配置属性，显示小地图上的图标
/// </summary>
public class MiniMapIcon : MonoBehaviour {

    public MiniMapObject target;//目标
    private Image image; //要显示的图片
    public RectTransform rectTransform; //显示的位置


    private void OnEnable()
    {
        if (target==null)
        {
            gameObject.SetActive(false);
            return;
        }
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        //如果有图标就设置图标
        if (target.config.icon)
        {
            image.sprite = target.config.icon;
        }
        //设置颜色
        image.color = target.config.color;
    }

}
