using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character {

    float Horizontal = float.Epsilon;
    float Vertical = float.Epsilon;


    protected override void Start()
    {
        base.Start();

        rbody.constraints = RigidbodyConstraints.FreezeRotation;
        rbody.isKinematic = true;
        rbody.drag = 0;
        rbody.angularDrag = 0;
        rbody.mass = 100;

        

        //把武器数组加到玩家的字典中
        if (weaponPos != null)
        {
            for (int i = 0; i < weaponPos.transform.childCount; i++)
            {
                int id = weaponPos.transform.GetChild(i).GetComponent<Weapon>().id;
                weaponPos.transform.GetChild(i).GetComponent<Weapon>().owner = this;
                weaponDict.Add(id, weaponPos.transform.GetChild(i).GetComponent<Weapon>());
            }
            currentWeapon = weaponDict[0];
            ChangeWeapon(0);
        }


    }

    protected override void UpdateInput()
    {
        base.UpdateInput();
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.H))
        {
            Melee();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeWeapon(2);
        }
    }

    protected override void UpdateControl()
    {
        base.UpdateControl();

        if (canControl)
        {
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
      

    }


    /// <summary>
    /// 改变武器
    /// </summary>
    private void ChangeWeapon(int weaponId)
    {
        if (weaponDict.ContainsKey(weaponId))
        {
            if (weaponDict[weaponId] != null)
            {
                foreach (KeyValuePair<int, Weapon> item in weaponDict)
                {
                    item.Value.gameObject.SetActive(false);
                }
                currentWeapon = weaponDict[weaponId];
                currentWeapon.gameObject.SetActive(true);
                anim.SetInteger("WeaponId", weaponId);
            }
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
