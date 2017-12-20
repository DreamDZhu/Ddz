using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色属性
/// </summary>
[System.Serializable]
public class PlayerData
{
    public int id;
    public string name;
    public float hp;
    public float mp;
}


public class Attribute:MonoBehaviour
{
    
    public PlayerData playerData;


    private void Start()
    {
        playerData = new PlayerData();
        playerData.id = 0;
        playerData.hp = 1000;
        playerData.mp = 1000;
        playerData.name = "Fucker";
    }

}
