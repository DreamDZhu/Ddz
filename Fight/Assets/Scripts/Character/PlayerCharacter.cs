using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character {

    float Horizontal = float.Epsilon;
    float Vertical = float.Epsilon;



    protected override void UpdateInput()
    {
        base.UpdateInput();
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.H))
        {
            Melee();
        }
    }

    protected override void UpdateControl()
    {
        base.UpdateControl();
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(Horizontal) > 0.01f || Mathf.Abs(Vertical) > 0.01f)
        {
            isMoving = true;
            vecMove = (Vector3.forward * Vertical + Vector3.right * Horizontal) * Time.deltaTime * moveSpeed;
            //   vecMove = transform.TransformDirection(vecMove);
            _mTransform.localPosition += vecMove;
            //  _mTransform.localPosition += vecMove * Time.fixedDeltaTime;
            //角色移动
            angle = Mathf.Atan2(vecMove.x, vecMove.z) * Mathf.Rad2Deg;
            _mTransform.localRotation = Quaternion.Euler(Vector3.up * angle);
            //   rbody.MovePosition(_mTransform.position + vecMove);
        }
        else
        {
            isMoving = false;
        }

        if (isTurnBase)
        {

            vecMove = _mJoystick.MovePosiNorm * Time.deltaTime;
            _mTransform.position += vecMove;
            //把弧度转换为角度
            angle = Mathf.Atan2(_mJoystick.MovePosiNorm.x, _mJoystick.MovePosiNorm.z) * Mathf.Rad2Deg;
            _mTransform.rotation = Quaternion.Euler(Vector3.up * angle);
            move = _mJoystick.MovePosiNorm;
        }

    }



    protected override void UndateAnimator()
    {
        base.UndateAnimator();
        anim.SetFloat("Velocity X", 0f);
        anim.SetFloat("Velocity Z", 1f);
        anim.SetBool("Moving", isMoving);


    }
}
