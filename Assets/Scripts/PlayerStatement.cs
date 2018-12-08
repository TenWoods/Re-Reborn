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
			GameObject.Find("GameSystem").GetComponent<GameController>().SendMessage("FollowPlayer");
			DeathInit();
		}
	}

	public void Dead()
	{
		Debug.Log("Die");
		this.GetComponent<Rigidbody>().velocity = Vector3.zero;
		isAlive = false;
	}

	public void KneelDown()
	{
		transform.rotation = Quaternion.Euler(0, -90, 0);
		animator.SetFloat("Blendx", -0.5f);
		animator.SetFloat("Blendy", 0.5f);
	}

	public void DeathInit()
	{
		Debug.Log(transform.rotation.eulerAngles.y);
		Review r = GameObject.Find("GameSystem").GetComponent<Review>();
		if (transform.rotation.eulerAngles.y > 85 && transform.rotation.eulerAngles.y < 265)
		{
			r.AddDeath(Instantiate(deathPrefab, transform.position + new Vector3(0, deathoffset, 0), Quaternion.Euler(90, 90, 0)));
		}
		else
		{
			r.AddDeath(Instantiate(deathPrefab, transform.position + new Vector3(0, deathoffset, 0), Quaternion.Euler(90, 270, 0)));
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
