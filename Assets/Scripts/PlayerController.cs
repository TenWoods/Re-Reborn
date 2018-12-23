using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  Player
{
    public class PlayerController : MonoBehaviour
    {
        private enum State
        {
	        Jump,
	        Walk,
	        Idle,
	        OnRope
        }

        [SerializeField]
	    private State playerState;        //player状态
        [SerializeField]
        private float walkSpeed;          //player移动速度
        [SerializeField]
        private float jumpForce;          //跳跃力度
        private bool isGround;            //是否在地面上
        [SerializeField]
        private float jumpColdTime;       //跳跃冷却时间
        private float jumpColdTimer;      //跳跃冷却时间计数器
        [SerializeField]
        private float jumpTime;           //跳跃时间
        private float jumpTimer;          //跳跃时间计数器
        [SerializeField]//debug
        private bool isOnRope;            //在绳子上
        [SerializeField]
        private float ropeSpeed;          //绳子上的速度
        private Rigidbody m_rigidbody;
        private Animator m_animator;
        private ConstantForce m_constantForce;

        private void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_animator = GetComponent<Animator>();
            m_constantForce = GetComponent<ConstantForce>();
            isGround = true;
            isOnRope = false;
            playerState = State.Idle;
            jumpColdTimer = jumpColdTime + 1;
        }

        private void Update() 
        {
            Handle();
            Move();
            SkillColdTimer();
        }

        //人物状态转换
        private void Handle()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if (playerState == State.Idle)//walk的前置条件
                {
                    playerState = State.Walk;
                }
            }
            if (Input.GetKey(KeyCode.W))
            {
                if (playerState == State.Idle || playerState == State.Walk)//jump前置条件
                {
                    if (isGround && jumpColdTimer >= jumpColdTime)
                    {
                        playerState = State.Jump;
                        isGround = false;
                        jumpColdTimer = 0;
                        jumpTimer = 0;
                    }
                }  
            }
            if (isOnRope)
            {
                playerState = State.OnRope;
            }
            if (!Input.anyKey)
            {
                if (!isOnRope)
                {
                    playerState = State.Idle;
                } 
            }
        }

        //人物移动
        private void Move()
        {
            switch(playerState)
            {
                case State.Idle : Idle(); break;
                case State.Walk : Walk(); break;
                case State.Jump : Jump(); break;
                case State.OnRope : OnRope(); break;
            }
        }

        //冷却计数
        private void SkillColdTimer()
        {
            if (jumpColdTimer < jumpColdTime && isGround)
            {
                jumpColdTimer += Time.deltaTime;
            }
        }

        private void SetAnimate(float x, float y, float time)
        {
            m_animator.SetFloat("Blendx", x, time, Time.deltaTime);
            m_animator.SetFloat("Blendy", y, time, Time.deltaTime);
        }

        #region 人物状态
        private void Walk()
        {
            float h = Input.GetAxis("Horizontal");
            Vector3 velocity = new Vector3(h, 0, 0) * walkSpeed;
            velocity.y = m_rigidbody.velocity.y;
            m_rigidbody.velocity = velocity;
            //移动时人物朝向
            if (h >= 0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            //设置动画状态
            if (isGround)
            {
                SetAnimate(1, 0, 0.1f);
            }
        }

        private void Jump()
        {
            Debug.Log("Jump");
            jumpTimer += Time.deltaTime;
            if (jumpTimer < jumpTime)
            {
                m_rigidbody.AddForce(new Vector3(0, jumpForce, 0));
                SetAnimate(0, 1, jumpTime - 0.1f);
            }
            else
            {
                //跳跃结束回到静止状态
                playerState = State.Idle;
            }
        }

        private void Idle()
        {
            Vector3 velocity = new Vector3(0, 0, 0);
            velocity.y = m_rigidbody.velocity.y;
            m_rigidbody.velocity = velocity;
            SetAnimate(0, 0, 0.1f);
        }

        private void OnRope()
        {
            float v = Input.GetAxis("Vertical");
            transform.position += new Vector3(0, v, 0) * ropeSpeed * Time.deltaTime;
            m_rigidbody.velocity = Vector3.zero;
            SetAnimate(-0.5f, 0.5f, 0);
        }

        #endregion

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.tag == "Floor")
            {
                
                isGround = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "RopeStart")
            {
                //在绳子始端切换状态
                Debug.Log("Change");
                if (isOnRope)
                {
                    m_constantForce.force = new Vector3(0, -9.8f, 0);
                    transform.parent = null;
                }
                else
                {
                    m_constantForce.force = new Vector3(0, 0, 0);
                    transform.parent = other.transform.parent;
                    m_rigidbody.velocity = Vector3.zero;
                }
                isOnRope = !isOnRope;
            }
            if (other.transform.tag == "RopeEnd")
            {
                isOnRope = false;
                m_constantForce.force = new Vector3(0, -9.8f, 0);
            }
        }
    }
}