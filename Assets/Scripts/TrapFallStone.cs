using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFallStone : TriggerThing {

	/// <summary>
	/// OnCollisionEnter is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionEnter(Collision other)
	{
		if(other.transform.tag == "Player")
		{
			other.gameObject.GetComponent<PlayerStatement>().Dead();
			OnCallGameSystem();
			change.SetUIText(index);
		}
	}
}
