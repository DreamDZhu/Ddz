using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 近战武器
/// </summary>
public class MeleeWeapon : Weapon {

    [Tooltip("配置此武器的所有HitBox")]
    private List<HitBox> hitBoxes;

    private Dictionary<HitBox, List<GameObject>> hitObjctCache; //被攻击的单位集合

    private bool canApplyDamage;
  //  public bool isDebug;

    protected virtual void Start()
    {
        hitBoxes = new List<HitBox>();
        hitObjctCache = new Dictionary<HitBox, List<GameObject>>();

        //将所有hitbox加入List
        foreach (Transform hitbox in transform)
        {
            if (hitbox.gameObject.name == "HitBox")
            {
                hitBoxes.Add(hitbox.GetComponent<HitBox>());
            }
        }

        //把所有的攻击碰撞体，加入当期武器
        if (hitBoxes.Count>0)
        {
            foreach (HitBox hitBox in hitBoxes)
            {
                hitBox.weapon = this;
                hitObjctCache.Add(hitBox, new List<GameObject>());
            }
        }
        else
        {
            this.enabled = false;
        }
    }

    private void ClearHitObjCache()
    {
        for (int i = 0; i < hitBoxes.Count; i++)
        {
            if (hitObjctCache != null)
            {
                hitObjctCache[hitBoxes[i]].Clear();
            }
            //激活攻击碰撞
            var hitCollider = hitBoxes[i];
            hitCollider.trigger.enabled = false;
        }
    }

    private void OnDestroy()
    {
        ClearHitObjCache();
        ResetHitBox();
    }

   private void ResetHitBox()
    {
        foreach (var item in hitBoxes)
        {
            item.gameObject.SetActive(false);
            item.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 设置激活后的伤害
    /// </summary>
    /// <param name="value"></param>
    public virtual void SetActiveDamage(bool value)
    {
        canApplyDamage = value;
        for (int i = 0; i < hitBoxes.Count; i++)
        {
            //激活攻击碰撞
            var hitCollider = hitBoxes[i];
            hitCollider.trigger.enabled = value;
            //动画播放结束 取消攻击碰撞
            if (value == false && hitObjctCache != null)
                hitObjctCache[hitCollider].Clear();
        }
    }

    /// <summary>
    /// 设置伤害
    /// </summary>
    /// <param name="hitBox"></param>
    /// <param name="other"></param>
    public virtual void OnHit(HitBox hitBox, Collider other)
    {
        //判断是否可以造成伤害  且缓存的被记者不包含新传入的单位（防止二次伤害） 且被攻击者不是自身
        if (canApplyDamage &&
           !hitObjctCache[hitBox].Contains(other.gameObject) &&
           (owner != null && other.gameObject != owner.gameObject))
        {
            //添加新的被击者进入被击数组
            hitObjctCache[hitBox].Add(other.gameObject);
            //播放特效和音乐
          //  SpawnHitEffect(other);
          //  SpawnHitSound(other);
            //判断被击者身上是否可以接受伤害数据
            var damageable = other.GetComponent<IDamageable>();
            if (damageable!=null)
            {
                Debug.Log(-hitImpact.GetDamage());
                Debug.Log(owner);
                Debug.Log(other.gameObject.transform.position);

                //传递伤害
                var damageData = new DamageEventData(-hitImpact.GetDamage(), owner,other.gameObject.transform.position,default(Vector3),hitImpact.GetImpulse());
                damageable.TakeDamage(damageData);
            }
          
        }
    }

    /// <summary>
    /// 生成被击特效
    /// </summary>
    /// <param name="other"></param>
    protected void SpawnHitEffect(Collider other)
    {
        var effect = hitImpact.GetHitEffect(other.sharedMaterial);
        if (effect)
        {
            var dir = (owner.transform.position - other.transform.position).normalized;
            GameObject spawnedDecal = GameObject.Instantiate(effect, other.transform.position, Quaternion.LookRotation(dir));
            spawnedDecal.transform.SetParent(other.transform);
        }
    }

    /// <summary>
    /// 生成被击音乐
    /// </summary>
    protected void SpawnHitSound(Collider other)
    {
        var sound = hitImpact.GetHitSound(other.sharedMaterial);
        if (sound)
        {
            AudioSource.PlayClipAtPoint(sound, other.transform.position);
        }
    }
}
