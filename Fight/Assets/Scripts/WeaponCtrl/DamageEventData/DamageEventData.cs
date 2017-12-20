using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamageable
{
    void TakeDamage(DamageEventData damageData);
}

/// <summary>
/// 伤害数据事件
/// </summary>
public class DamageEventData  {

    /// <summary>
    /// 伤害值
    /// </summary>
	public float delta { get; set; }

    /// <summary>
    /// 攻击者
    /// </summary>
    public Character attacker { get; private set; }

    /// <summary>
    /// 攻击点
    /// </summary>
    public Vector3 hitPoint { get; private set; }

    /// <summary>
    /// 攻击距离
    /// </summary>
    public Vector3 hitDirection { get; private set; }

    /// <summary>
    /// 冲击力
    /// </summary>
    public float hitImpulse { get; private set; }

    /// <summary>
    /// 伤害数据包
    /// </summary>
    /// <param name="_delta">伤害</param>
    /// <param name="_attacker">攻击者</param>
    /// <param name="_hitpoint">伤害点</param>
    /// <param name="_hitDirection">伤害距离</param>
    /// <param name="_hitImpulse">冲击力</param>
    public DamageEventData(float _delta, Character _attacker=null,Vector3 _hitpoint=default(Vector3),Vector3 _hitDirection=default(Vector3),float _hitImpulse=0f)
    {
        delta = _delta;
        attacker = _attacker;
        hitPoint = _hitpoint;
        hitDirection = _hitDirection;
        hitImpulse = _hitImpulse;
    }

}
