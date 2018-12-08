using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootByTime : MonoBehaviour {

	public GameObject bullet;
	public Transform initPos;
	[Range(0,10)]
	public float shootTime = 3.0f;
	// Use this for initialization
	void Start () {
		StartCoroutine(Shoot());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Shoot()
	{
		GameObject gm = Instantiate(bullet,initPos.position,Quaternion.identity);
		gm.GetComponent<Rigidbody>().AddForce(new Vector3(-0.5f,0,0));
		yield return new WaitForSeconds(shootTime);
	}
}
