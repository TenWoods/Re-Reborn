using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour 
{
	[SerializeField]
	private float shootColodTime;
	private float timer = 0;
	[SerializeField]
	private float bulletSpeed;
	public GameObject bulletPrefab;

	private void Start() 
	{
		timer = shootColodTime;
	}

	private void Update() 
	{
		if (timer < shootColodTime)
		{
			timer += Time.deltaTime;
		}
		else
		{
			timer = 0;
			GameObject g = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
			g.GetComponent<Rigidbody>().velocity = g.transform.forward.normalized * bulletSpeed;
		}

	}
}
