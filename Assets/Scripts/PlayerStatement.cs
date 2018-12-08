using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatement : MonoBehaviour 
{
	[SerializeField]
	private bool isAlive = true;
	[SerializeField]
	private float deathoffset;
	private Animator animator;
	public GameObject deathPrefab;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	private void Update() 
	{
		if (isAlive)
		{
			return;
		}
		gameObject.GetComponent<Move>().enabled = false;
		animator.SetFloat("Blendy", -1.0f, 0.5f, Time.deltaTime);
		if (Mathf.Abs(animator.GetFloat("Blendy") + 1.0f) <= 0.05f)
		{
			DeathInit();
		}
	}

	public void Dead()
	{
		Debug.Log("Die");
		isAlive = false;
	}

	public void DeathInit()
	{
		Debug.Log(transform.rotation.eulerAngles.y);
		if (transform.rotation.eulerAngles.y > 85 && transform.rotation.eulerAngles.y < 265)
		{
			Instantiate(deathPrefab, transform.position + new Vector3(0, deathoffset, 0), Quaternion.Euler(90, 90, 0));
		}
		else
		{
			Instantiate(deathPrefab, transform.position + new Vector3(0, deathoffset, 0), Quaternion.Euler(90, 270, 0));
		}
		Destroy(this.gameObject);
	}

	public bool IsAlive
	{
		get
		{
			return isAlive;
		}
	}
}
