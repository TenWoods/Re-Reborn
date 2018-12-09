using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapByStep : TriggerThing {

	public GameObject rock;

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Player")
		{
			if(rock.GetComponent<HingeJoint>()!=null)
				rock.GetComponent<HingeJoint>().breakForce = 0.1f;
		}
	}
}
