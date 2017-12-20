using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 相机控制器
/// </summary>
public class CameraCtrl : MonoBehaviour {

    private Transform _mTransform;
    private Transform _mPlayerTransform;




    [SerializeField]
    private float x; //鼠标X
    [SerializeField]
    private float y;//鼠标Y

    private void Awake()
    {
        
    }

    private void Start()
    {
        _mTransform = this.transform;
        _mPlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        if (_mPlayerTransform!=null)
        {
            _mTransform.position = _mPlayerTransform.position;
        }
      
    }

    void FirstCamera()
    {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");
        Vector3 camAng = Vector3.zero;
        camAng.y += x;
        _mTransform.eulerAngles = camAng;
    }

}
