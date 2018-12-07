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
	[SerializeField]
	private State playerState;
	[SerializeField]
	private float switchTime;
	private Rigidbody rb;
	private bool isGround;
	private Animator animator;
	//步行动画的切换
	private float walkAnimate = 0.66f;
	//跳跃计时器
	private float jumpTimer;

	private void Start() 
	{
		playerState = State.Idle;
		rb = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		isGround = true;
	}

	private void Update() 
	{
		if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
		{
			if (Input.GetKey(KeyCode.A))
			{
				transform.rotation = Quaternion.Euler(0, -90, 0);
				Walk(-1);
			}
			else
			{
				transform.rotation = Quaternion.Euler(0, 90, 0);
				Walk(1);
			}
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
		float velocity_y = rb.velocity.y;
		rb.velocity = new Vector3(dir, velocity_y, 0) * speed;
		
	}

	private void Jump()
	{
		if (playerState != State.Jump && isGround)
		{
			animator.SetFloat("Blend", 0.33f);
			rb.AddForce(new Vector3(0, jumpForce, 0));
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
			if (playerState == State.Jump)
			{
				playerState = State.Idle;
				animator.SetFloat("Blend", 0.33f);
			}
		}
	}

	public void GlueSet(float mag)	
	{
		walkAnimate = 1;
		jumpForce /= mag;
	}

	public void GlueReset(float mag)
	{
		walkAnimate = 0.66f;
		jumpForce *= mag;
	}
}
