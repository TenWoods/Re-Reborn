using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryByRock : MonoBehaviour {
	public GameObject trigger1;
	public GameObject trigger2;
	/// <summary>
	/// OnCollisionEnter is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionEnter(Collision other)
	{
		if(other.transform.tag == "Rock")
		{
			Destroy(other.gameObject);
			Destroy(this.gameObject);
			trigger1.SetActive(false);
			trigger2.SetActive(false);
		}
	}
}
