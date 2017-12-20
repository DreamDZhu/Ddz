using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 相机控制器
/// </summary>
public class CameraCtrl : MonoBehaviour {

    private Transform _mTransform;
    private Transform _mPlayerTransform;

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

}
