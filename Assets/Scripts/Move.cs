using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
	Jump,
	Walk,
	Idle,
	OnRope
}

public class Move : MonoBehaviour 
{
	[SerializeField]
	private float speed;
	[SerializeField]
	private float jumpForce;
	private	float m_speed;
	private float m_jumpForce;
	[SerializeField]
	private State playerState;
	[SerializeField]
	private float switchTime;
	private Rigidbody rb;
	[SerializeField]
	private bool isGround;
	private Animator animator;
	//步行动画的切换
	private float walkAnimate_x = -1.0f;
	private float walkAnimate_y = 0.0f;
	//跳跃计时器
	private float jumpTimer;
	//胶水处影响
	[SerializeField]
	private float jumpGlue;
	[SerializeField]
	private float speedGlue;
	//绳子的节点数
	[SerializeField]
	private float ropeSpeed;
	private int ropeCount;
	[SerializeField]
	public GameObject[] ropeNode;

	private void Start() 
	{
		m_speed = speed;
		m_jumpForce = jumpForce;
		playerState = State.Idle;
		rb = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		isGround = true;
	}

	private void Update() 
	{
		if (!this.GetComponent<PlayerStatement>().IsAlive)
		{
			return;
		}
		if (Input.GetKey(KeyCode.A) && playerState != State.OnRope)
		{
			transform.rotation = Quaternion.Euler(0, -90, 0);
			Walk(-1);
		}
		if (Input.GetKey(KeyCode.D) && playerState != State.OnRope)
		{
			transform.rotation = Quaternion.Euler(0, 90, 0);
			Walk(1);
		}
		if (Input.GetKey(KeyCode.W))
		{
			if (playerState != State.OnRope)
			{
				Jump();
			}
			else
			{
				OnRope();
			}
		}
		if (!Input.anyKey)
		{
			if (playerState != State.Jump)
			{
				rb.velocity = Vector3.zero;
			}
			if (isGround)
			{
				animator.SetFloat("Blendx", 0.0f);
				animator.SetFloat("Blendy", 0.0f);
			}
		}
	}

	private void Walk(int dir)
	{
		if (playerState != State.Jump)
		{
			playerState = State.Walk;
			animator.SetFloat("Blendx", walkAnimate_x, switchTime, Time.deltaTime);
			animator.SetFloat("Blendy", walkAnimate_y);
		}
		else if (playerState == State.Walk)
		{
			animator.SetFloat("Blendx", walkAnimate_x);
			animator.SetFloat("Blendy", walkAnimate_y);
		}
		rb.velocity = new Vector3(dir * m_speed, rb.velocity.y, 0);
	}

	private void OnRope()
	{
		animator.SetFloat("Blendx", -0.5f);
		animator.SetFloat("Blendy", -0.5f);
		rb.velocity = new Vector3(rb.velocity.x, 0, 0);
		transform.localPosition += new Vector3(0, ropeSpeed * Time.deltaTime, 0);
	}

	private void Jump()
	{
		if (isGround)
		{
			animator.SetFloat("Blendx", 0.0f);
			animator.SetFloat("Blendy", 0.0f);
			rb.AddForce(Vector3.up * m_jumpForce);
			isGround = false;
			animator.SetFloat("Blendy", 1.0f);
			playerState = State.Jump;
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.transform.tag == "Floor")
		{
			isGround = true;
			if (playerState != State.Walk)
			{
				playerState = State.Idle;
				animator.SetFloat("Blendx", 0.0f);
				animator.SetFloat("Blendy", 0.0f);
			}
		}
		if(other.transform.tag == "Rock")
		{
			Debug.Log("add force");
			Vector3 dir = other.gameObject.GetComponent<Rigidbody>().velocity.normalized;
			rb.AddForce(dir*jumpForce*2);
		}
	}

    private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Rope" && playerState != State.OnRope)
		{
			playerState = State.OnRope;
			rb.velocity = Vector3.zero;
			transform.parent = other.transform;
			transform.localPosition = new Vector3(-1.6f, 0, -2.5f);
			GetComponent<ConstantForce>().force = Vector3.zero;
		}
		if(other.transform.tag == "Rope" && other.name == "RopeParent" && playerState == State.OnRope)
		{
			playerState = State.Idle;
			transform.parent = null;
			transform.position = other.transform.position + new Vector3(0.5f, 0, 0);
			GetComponent<ConstantForce>().force = new Vector3(0, -9.8f, 0);
		}
	}

	public void GlueSet()	
	{
		walkAnimate_x = 1.0f;
		m_jumpForce = jumpGlue;
		m_speed = speedGlue;
	}

	public void GlueReset()
	{
		walkAnimate_x = -1.0f;
		m_jumpForce = jumpForce;
		m_speed = speed;
	}
}
