using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackActionState : StateMachineBehaviour {

    

    [Tooltip("开始产生伤害的时间（normalizedTime）")]
    public float startDamageTime = 0.05f;
    [Tooltip("结束产生伤害的时间（normalizedTime）")]
    public float endDamageTime = 0.9f;
    [Tooltip("退出状态时，是否清空输入池")]
    public bool resetTrigger;

    private bool isActive; //是否激活当前状态


    /// <summary>
    /// 当状态更新时执行
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime%1>=startDamageTime&&stateInfo.normalizedTime%1<=endDamageTime&&!isActive)
        {
            isActive = true;
            ActiveDamage(animator, true);
        }
        else if (stateInfo.normalizedTime%1>endDamageTime&&isActive)
        {
            isActive = false;
            ActiveDamage(animator, false);
        }
    }

    /// <summary>
    /// 状态退出时执行
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isActive)
        {
            isActive = false;
            ActiveDamage(animator, false);
        }
        if (resetTrigger)
        {
            animator.ResetTrigger("Attack");
        }
    }

    /// <summary>
    /// 激活伤害
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="value"></param>
    void ActiveDamage(Animator animator,bool value)
    {
        var attack = animator.GetComponent<Character>();
        if (attack)
        {
            attack.SetActiveAttack(value);
            attack.SetActiveSpeed(value);
        }
    }

}
