using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 摇杆控制器
/// </summary>
public class Joystick : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {

    public static Joystick Instance;
   
    public float mallowMoveDistance=0.1f;//超过这个距离开始移动

    public float mRadius = 0f; //摇杆移动的半径
    public Vector3 originPosition;//起始坐标
    private Vector3 dir;//移动的方向
    private GameObject player;
    [HideInInspector]
    public Vector3 MovePosiNorm; //标准化的移动距离

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        originPosition = transform.position;
        mRadius = (transform as RectTransform).sizeDelta.x*1.5f ;
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    //绘制
    public  void OnDrag(PointerEventData eventData)
    {
        dir = (Input.mousePosition - originPosition).normalized; //获得移动方向
        transform.position = Input.mousePosition;
        //如果移动距离大于半径 大于可拖动方向，就固定住
        if (Vector3.Distance(originPosition,Input.mousePosition)>mRadius)
        {
            FixedRadius();
        }
        //距离大于激活移动的距离
        if (Vector3.Distance(transform.position, originPosition) > mallowMoveDistance)
        {
            MovePosiNorm = (transform.position - originPosition).normalized;
            MovePosiNorm = Vector3.forward * MovePosiNorm.y + Vector3.right * MovePosiNorm.x;
        }
        else
            MovePosiNorm = Vector3.zero;

        Character.moveStart();
    }

    public void FixedRadius()
    {
        transform.position = originPosition + dir * mRadius;
    }
    
    //松开
    public  void OnEndDrag(PointerEventData eventData)
    {
        transform.position = originPosition;
        Character.moveEnd();
    }

    //按下
    public void OnBeginDrag(PointerEventData eventData)
    {
       
    }
}
