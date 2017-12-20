using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestNp : MonoBehaviour {


    private Slider hpSlider;
    private RectTransform rectTrans;

    public Vector2 offsetPos;//偏移
    public Transform target;//目标

    public float value; //当前血
    private float maxValue; //最大血


	// Use this for initialization
	void Start () {

        hpSlider = GetComponent<Slider>();
        rectTrans = GetComponent<RectTransform>();
        value = target.GetComponent<Attribute>().playerData.hp;
        maxValue = value;
    }
	
	// Update is called once per frame
	void Update () {
      
	}

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }
        value = target.GetComponent<Attribute>().playerData.hp;
        hpSlider.value = value / maxValue;
        Vector3 tarpos = target.transform.position;
        Vector2 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, tarpos);
        rectTrans.position = pos + offsetPos;
        
    }


    public void SetTarget(Transform objTr)
    {
        target = objTr;
        maxValue = target.GetComponent<Attribute>().playerData.hp;
        value = maxValue;
    }
}
