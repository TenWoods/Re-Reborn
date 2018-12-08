using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
	Jump,
	Walk,
	Idle,
	Fall
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
	private float walkAnimate = 0.66f;
	//跳跃计时器
	private float jumpTimer;
	//胶水处影响
	[SerializeField]
	private float jumpGlue;
	[SerializeField]
	private float speedGlue;

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
		if (Input.GetKey(KeyCode.A))
		{
			transform.rotation = Quaternion.Euler(0, -90, 0);
			Walk(-1);
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.rotation = Quaternion.Euler(0, 90, 0);
			Walk(1);
		}
		if (Input.GetKey(KeyCode.W))
		{
			Jump();
		}
		if (!Input.anyKey)
		{
			if (playerState != State.Jump)
			{
				rb.velocity = Vector3.zero;
			}
			if (isGround)
			{
				animator.SetFloat("Blend", 0.33f);
			}
		}
	}

	private void Walk(int dir)
	{
		if (playerState != State.Jump)
		{
			playerState = State.Walk;
			animator.SetFloat("Blend", walkAnimate, switchTime, Time.deltaTime);
		}
		else if (playerState == State.Walk)
		{
			animator.SetFloat("Blend", walkAnimate);
		}
		rb.velocity = new Vector3(dir * m_speed, rb.velocity.y, 0);
		
	}

	private void Jump()
	{
		if (isGround)
		{
			animator.SetFloat("Blend", 0.33f);
			rb.AddForce(Vector3.up * m_jumpForce);
			isGround = false;
			animator.SetFloat("Blend", 0);
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
				animator.SetFloat("Blend", 0.33f);
			}
		}
	}

	public void GlueSet()	
	{
		walkAnimate = 1;
		m_jumpForce = jumpGlue;
		m_speed = speedGlue;
	}

	public void GlueReset()
	{
		walkAnimate = 0.66f;
		m_jumpForce = jumpForce;
		m_speed = speed;
	}
}
