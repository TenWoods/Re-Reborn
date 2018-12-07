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
	private Rigidbody rb;
	private bool isGround;

	private void Start() 
	{
		playerState = State.Idle;
		rb = GetComponent<Rigidbody>();
		isGround = true;
	}

	private void Update() 
	{
		if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
		{
			if (playerState != State.Jump)
			{
				playerState = State.Walk;
			}
			if (Input.GetKey(KeyCode.A))
			{
				Walk(-1);
			}
			else
			{
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
		}
	}

	private void Walk(int dir)
	{
		float velocity_y = rb.velocity.y;
		rb.velocity = new Vector3(dir, velocity_y, 0) * speed;
	}

	private void Jump()
	{
		if (playerState != State.Jump && isGround)
		{
			rb.AddForce(new Vector3(0, jumpForce, 0));
			isGround = false;
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
			}
		}
	}	
}
