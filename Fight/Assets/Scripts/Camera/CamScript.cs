using Boo.Lang;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;



/// <summary>
/// 摄像机控制
/// </summary>
[Serializable]
public class CamScript : MonoBehaviour
{
    public LayerMask layermask;
    public bool canMove;
    public float smoothSpeed;
    private float sensitivity;
    private float invertVertical;
    private float invertHorizontal;
    private Vector3 lastPos;
    private Vector3 charPos;
    public Transform followTarget;
    public Transform lockTarget;

    private float height;
    public float addHeight;
    public float addX;
    private float distance;
    public float y;
    public float x;
    public float realignOffset;


    public CamScript()
    {
        this.smoothSpeed = (float)15;
        this.sensitivity = (float)10;
        this.invertVertical = (float)1;
        this.invertHorizontal = (float)1;
        this.distance = (float)-7;
        this.realignOffset = -0.54f;
    }



    public void LateUpdate()
    {
        if (this.followTarget)
        {

            if (!this.lockTarget)
            {
                this.height = 0.75f;
                this.x += Input.GetAxisRaw("Horizontal");
                if (this.canMove)
                {
                    this.x = this.x + Input.GetAxis("Mouse X") * this.sensitivity * this.invertHorizontal;
                    this.y = this.y + Input.GetAxis("Mouse Y") * -this.sensitivity * this.invertVertical;
                    //this.x = this.x + Input.GetAxis("Cam. X") * this.sensitivity * 0.25f * this.invertHorizontal;
                    //this.y = this.y + Input.GetAxis("Cam. Y") * this.sensitivity * 0.1f * this.invertVertical;
                }
            }
            else if (this.canMove)
            {
                this.x = this.x + Input.GetAxis("Mouse X") * this.sensitivity * 0.1f * this.invertHorizontal;
                this.y = this.y + Input.GetAxis("Mouse Y") * -this.sensitivity * this.invertVertical;
                //this.x = this.x + Input.GetAxis("Cam. X") * this.sensitivity * 0.1f * this.invertHorizontal;
                //this.y = this.y + Input.GetAxis("Cam. Y") * this.sensitivity * 0.05f * this.invertVertical;
            }
            if (this.y > 33.8f)
            {
                this.y = 33.9f;
            }
            else if (this.y < (float)-10)
            {
                this.y = (float)-10;
            }

            Vector3 vector3 = this.lockTarget.GetComponent<Collider>().bounds.center;
            Quaternion quaternion = Quaternion.Euler(this.y, this.x + this.addX, (float)0);
            Vector3 vector31 = (quaternion * new Vector3((float)0, this.height + this.addHeight, this.distance)) + vector3;
            if (!this.lockTarget)
            {
                this.lastPos = this.transform.position - (quaternion * new Vector3((float)0, this.height, this.distance));
                this.smoothSpeed = Mathf.LerpAngle(this.smoothSpeed, (float)15, Time.deltaTime);
            }
            else
            {
                Vector3 vector32 = this.followTarget.eulerAngles;
                Vector3 vector33 = this.transform.eulerAngles;
                this.x = Mathf.Lerp(vector33.y, vector32.y, (float)0);
                Vector3 vector34 = this.lockTarget.GetComponent<Collider>().bounds.center;
                Vector3 vector35 = ((vector34 - vector3) * 0.5f) + vector3;
                if (this.charPos == this.followTarget.position)
                {
                    vector31 = (quaternion * new Vector3((float)0, this.height, this.distance)) + this.lastPos;
                }
                else
                {
                    this.lastPos = this.transform.position - (quaternion * new Vector3((float)0, this.height, this.distance));
                    this.charPos = vector3;
                }
                quaternion = Quaternion.LookRotation(vector35 - this.transform.position);
                this.smoothSpeed = (float)0;
            }
            Vector3 vector36 = (quaternion * new Vector3((float)0, this.height, this.distance)) + vector3;
            Vector3 vector37 = new Vector3();
            this.realignOffset = -0.58f;
            vector37 = vector3;
            RaycastHit raycastHit = new RaycastHit();
            if (Physics.Linecast(vector37, vector36, out raycastHit, this.layermask))
            {
                float single = Vector3.Distance(vector37, raycastHit.point) + this.realignOffset;
                Quaternion quaternion1 = Quaternion.Euler(this.y, this.x + this.addX, (float)0);
                if (this.lockTarget)
                {
                    vector31 = (quaternion1 * new Vector3((float)0, this.height + this.addHeight, -single)) + vector3;
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, quaternion, (float)30 * Time.deltaTime);
                    this.transform.position = Vector3.Slerp(this.transform.position, vector31, (float)30 * Time.deltaTime);
                }
                else
                {
                    vector31 = (quaternion * new Vector3((float)0, this.height + this.addHeight, -single)) + vector3;
                }
            }
            if (!this.lockTarget)
            {
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, quaternion, this.smoothSpeed * Time.deltaTime);
                this.transform.position = Vector3.Slerp(this.transform.position, vector31, this.smoothSpeed * Time.deltaTime);
            }
            else
            {
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, quaternion, (float)5 * Time.deltaTime);
                this.transform.position = Vector3.Slerp(this.transform.position, vector31, (float)5 * Time.deltaTime);
            }
        }
    }

}
