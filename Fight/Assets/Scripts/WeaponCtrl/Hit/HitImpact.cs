using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于处理伤害修正，冲击修正， 受击效果
/// </summary>
[System.Serializable]
public class HitImpact  {

    /// <summary>
    /// 伤害最大值
    /// </summary>
    [Range(0f, 1000f)]
    [SerializeField]
    private float damageMax = 15f;

    /// <summary>
    /// 冲击力最大值
    /// </summary>
    [Range(0f, 1000f)]
    [SerializeField]
    private float impulseMax = 15f;

    /// <summary>
    /// 距离曲线
    /// </summary>
    [SerializeField]
    private AnimationCurve distanceCurve = new AnimationCurve(
       new Keyframe(0f, 1f),
       new Keyframe(0.8f, 0.5f),
       new Keyframe(1f, 0f));

    /// <summary>
    /// 金属板被击特效
    /// </summary>
    [SerializeField]
    public GameObject metalHitEffect;
    /// <summary>
    /// 沙地被击特效
    /// </summary>
    [SerializeField]
    public GameObject sandHitEffect;
    /// <summary>
    /// 石头被击特效
    /// </summary>
    [SerializeField]
    public GameObject stoneHitEffect;
    /// <summary>
    /// 水面被击特效
    /// </summary>
    [SerializeField]
    public GameObject waterLeakEffect;
    /// <summary>
    /// 木板被击特效
    /// </summary>
    [SerializeField]
    public GameObject woodHitEffect;
    /// <summary>
    /// 肉体被击特效
    /// </summary>
    [SerializeField]
    public GameObject[] fleshHitEffects;


    [SerializeField]
    public AudioClip[] defaultHitSound; //默认被击声音

    /// <summary>
    /// 获取被击声音
    /// </summary>
    /// <param name="pm"></param>
    /// <returns></returns>
    public AudioClip GetHitSound(PhysicMaterial pm)
    {
        if (pm != null)
        {
            string materialName = pm.name;
            switch (materialName)
            {
                case "Metal":
                    return defaultHitSound[UnityEngine.Random.Range(0, defaultHitSound.Length)];
                case "Sand":
                    return defaultHitSound[UnityEngine.Random.Range(0, defaultHitSound.Length)];
                case "Stone":
                    return defaultHitSound[UnityEngine.Random.Range(0, defaultHitSound.Length)];
                case "WaterLeak":
                    return defaultHitSound[UnityEngine.Random.Range(0, defaultHitSound.Length)];
                case "Wood":
                    return defaultHitSound[UnityEngine.Random.Range(0, defaultHitSound.Length)];
                case "Meat":
                    return defaultHitSound[UnityEngine.Random.Range(0, defaultHitSound.Length)];
                case "Character":
                    return defaultHitSound[UnityEngine.Random.Range(0, defaultHitSound.Length)];
            }
        }
        return null;
    }

    /// <summary>
    /// 获取被击声音
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
    public AudioClip GetHitSound(RaycastHit hit)
    {
        return GetHitSound(hit.collider.sharedMaterial);
    }

    /// <summary>
    /// 获取被击特效
    /// </summary>
    /// <param name="pm"></param>
    /// <returns></returns>
    public GameObject GetHitEffect(PhysicMaterial pm)
    {
        if (pm != null)
        {
            string materialName = pm.name;
            switch (materialName)
            {
                case "Metal":
                    return metalHitEffect;
                case "Sand":
                    return sandHitEffect;
                case "Stone":
                    return stoneHitEffect;
                case "WaterLeak":
                    return waterLeakEffect;
                case "Wood":
                    return woodHitEffect;
                case "Meat":
                    return fleshHitEffects[UnityEngine.Random.Range(0, fleshHitEffects.Length)];
                case "Character":
                    return fleshHitEffects[UnityEngine.Random.Range(0, fleshHitEffects.Length)];
            }
        }
        return null;
    }

    /// <summary>
    /// 获取被击特效
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
    public GameObject GetHitEffect(RaycastHit hit)
    {
        return GetHitEffect(hit.collider.sharedMaterial);
    }

    /// <summary>
    /// 获取伤害值
    /// </summary>
    /// <returns></returns>
    public float GetDamage()
    {
        return damageMax;
    }

    /// <summary>
    /// 获取冲击力
    /// </summary>
    /// <returns></returns>
    public float GetImpulse()
    {
        return impulseMax;
    }

   /// <summary>
   /// 根据距离获取伤害值
   /// </summary>
   /// <param name="distance"></param>
   /// <param name="maxDistance"></param>
   /// <returns></returns>
    public float GetDamageAtDistance(float distance, float maxDistance)
    {
        return ApplyCurveToValue(damageMax, distance, maxDistance);
    }

    /// <summary>
    /// 根据距离获取冲击力
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="maxDistance"></param>
    /// <returns></returns>
    public float GetImpulseAtDistance(float distance, float maxDistance)
    {
        return ApplyCurveToValue(impulseMax, distance, maxDistance);
    }

    /// <summary>
    /// 根据曲线计算伤害值
    /// </summary>
    /// <param name="value"></param>
    /// <param name="distance"></param>
    /// <param name="maxDistance"></param>
    /// <returns></returns>
    private float ApplyCurveToValue(float value, float distance, float maxDistance)
    {
        float maxDistanceAbsolute = Mathf.Abs(maxDistance);
        float distanceClamped = Mathf.Clamp(distance, 0f, maxDistanceAbsolute);

        return value * distanceCurve.Evaluate(distanceClamped / maxDistanceAbsolute);
    }

}
