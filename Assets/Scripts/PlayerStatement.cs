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
		animator.SetFloat("Blend", 1);
	}

	public void Dead()
	{
		Debug.Log("Die");
		isAlive = false;
	}

	public void DeathInit()
	{
		Instantiate(deathPrefab, transform.position + new Vector3(0, deathoffset, 0), Quaternion.Euler(90, 90, 0));
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
