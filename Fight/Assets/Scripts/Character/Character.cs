using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 角色控制器
/// </summary>
//[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(CapsuleCollider))]
//[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour,IDamageable {


    public GameObject weaponPos;

    protected Weapon currentWeapon; //当前装备的武器

   
    protected Dictionary<int, Weapon> weaponDict = new Dictionary<int, Weapon>();

    public float health = 0f; //血量
    public float stamin = 0f;
    public float moveSpeed = 0f; //移动速度

    //状态
    protected bool isCrouching = false;
    protected bool isGrounded = false;
    protected bool isDead = false;
    protected bool isMoving = false;
    protected bool canControl = true;

    //组件
    protected Rigidbody rbody;
    protected Animator anim;
    protected CapsuleCollider capsule;



 


    protected Vector3 move;
    protected Vector3 moveRaw;
    protected Vector3 vecMove;
    protected Vector3 groundNormal;
    protected float defaultCapsuleHeight;
    protected Vector3 defaultCapsuleCenter;

  

    protected bool isTurnBase = false;
    public delegate void MoveDelegate(); //角色移动委托
    public static Character Instance;
    public static MoveDelegate moveStart;
    public static MoveDelegate moveEnd;
    protected Transform _mTransform;
    public Joystick _mJoystick;

    protected float angle;

    protected virtual void Awake()
    {
        Instance = this;
        moveStart += OnMoveStart;
        moveEnd += OnMoveEnd;
    }

    void OnMoveStart()
    {
        isTurnBase = true;
    }

    void OnMoveEnd()
    {
        isTurnBase = false;
    }

    protected virtual void Start()
    {
        //if (currentWeapon!=null)
        //{
        //    currentWeapon.owner = GetComponent<Character>();
        //}
     
        _mTransform = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        if (capsule!=null)
        {
            defaultCapsuleHeight = capsule.height;
            defaultCapsuleCenter = capsule.center;
        }




    }

    protected virtual void FixedUpdate()
    {
        UpdateInput();
        if (!isDead && canControl)
        {
            UpdateControl();
        }
       
    }


    /// <summary>
    /// 更新输入
    /// </summary>
    protected virtual void UpdateInput()
    {
      
    }

    /// <summary>
    /// 更新控制
    /// </summary>
    protected virtual void UpdateControl()
    {

    }

    /// <summary>
    /// 近战攻击
    /// </summary>
    protected void Melee()
    {
        if (currentWeapon as MeleeWeapon)
        {
            anim.SetTrigger("Attack");
        }
    }

  


    /// <summary>
    /// 设置攻击的武器
    /// </summary>
    /// <param name="active"></param>
    public virtual void SetActiveAttack(bool active)
    {
        
        if (currentWeapon as MeleeWeapon == null) return;

        (currentWeapon as MeleeWeapon).SetActiveDamage(active);
       
        
    
    }

    /// <summary>
    /// 设置攻击的速度
    /// </summary>
    /// <param name="speed"></param>
    public virtual void SetActiveSpeed(bool active)
    {
        if (active)
        {
            canControl = false;
        }
        else
        {
            canControl = true;
        }
    }

    /// <summary>
    /// 移动方向
    /// </summary>
    /// <param name="move"></param>
    private void Movement(Vector3 move)
    {
        //当模大于1时，要进行归一化，防止在斜方向移动时，移动速度加快
        if (move.magnitude > 1f)
        {
            move.Normalize();
        }
        //存下原本的移动输入
        moveRaw = move;
        //将move从世界空间转向本地空间
        move = transform.InverseTransformDirection(move);
        //将move投影在地板的2D平面上
        move = Vector3.ProjectOnPlane(move, groundNormal);
        //返回值为x轴和一个（在零点起始，在(x, y)结束）的2D向量的之间夹角
        // var  = Mathf.Atan2(move.x, move.z);

    }

    protected virtual void LateUpdate()
    {
        UndateAnimator();
    }

    /// <summary>
    /// 更新动画状态机
    /// </summary>
    protected virtual void UndateAnimator()
    {

        #region 废弃
        // anim.SetBool("InjuredIdle", Horizontal != 0 || Vertical != 0 ? true : false);
        //anim.SetFloat("Forward", forwardAmount, 0.01f, Time.deltaTime);
        //anim.SetFloat("Right", rightAmount, 0.01f, Time.deltaTime);
        //anim.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
        //anim.SetFloat("Speed", velocity.magnitude, 0.01f, Time.deltaTime);
        //anim.SetBool("Crouch", isCrouching);
        //anim.SetBool("OnGround", isGrounded);
        //  anim.SetFloat("WeaponID", currentWeapon.id);
        #endregion

    }

    #region Interface

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="damageData"></param>
    public void TakeDamage(DamageEventData damageData)
    {
        if (damageData==null)
        {
            return;
        }
        health = GetComponent<Attribute>().playerData.hp;
        if (health>Mathf.Abs(damageData.delta)||damageData.delta>0)
        {
            var Dir = (_mTransform.position - damageData.attacker.transform.position).normalized;

            health += damageData.delta;
            rbody.AddForceAtPosition(Dir * damageData.hitImpulse, damageData.hitPoint, ForceMode.Impulse);
            Debug.Log(this.gameObject.name + "受到了伤害:" + damageData.delta);
        }
        else
        {
            if (!isDead)
            {
                Debug.Log("Dead");
            }
        }
    }



    #endregion


}
