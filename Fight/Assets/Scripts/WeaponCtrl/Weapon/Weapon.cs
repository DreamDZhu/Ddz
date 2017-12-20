using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器物品属性
/// </summary>
public class ItemData
{
    public int id;
    public string name;
    public string icon;
    public string atlas;
}


/// <summary>
/// 武器属性类
/// </summary>
public abstract class Weapon : MonoBehaviour {

    //public Transform leftHandIK; //左手IK
    //public Transform rightHandIK; //右手IK
    public int id; //武器ID

    /// <summary>
    /// 处理伤害
    /// </summary>
    [SerializeField]
    protected HitImpact hitImpact; 

    public ItemData data;
    public Character owner { get;  set; } //所有人
    public bool isEquiped { get; private set; } //是否装备
    public bool isInInventory { get; private set; } //是否是存货

    private void Awake()
    {
        //获取武器属性
        data = new ItemData();
        switch (id)
        {
            case 0:
                data.name = "QuanTao";
                data.id = 0;
                break;
            case 1:
                data.name = "2handJian";
                data.id = 1;
                break;

        }

    }

   

    /// <summary>
    /// 装备
    /// </summary>
    public virtual void OnEquip()
    {
        isEquiped = true;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 卸载
    /// </summary>
    public virtual void OnUnEquip()
    {
        isEquiped = false;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 进入背包
    /// </summary>
    /// <param name="newOwner"></param>
    public void OnEnterInventory(Character newOwner)
    {
        //如果武器所有人改变
        if (owner!=newOwner)
        {
            owner = newOwner;
            gameObject.SetActive(false);
            isInInventory = true;
            GetComponent<Collider>().enabled = false;
        }
    }

    /// <summary>
    /// 移除背包
    /// </summary>
    public void OnLeaveInventory()
    {
        owner = null;
        gameObject.SetActive(true);
        isInInventory = false;
        GetComponent<Collider>().enabled = true;
    }

}
