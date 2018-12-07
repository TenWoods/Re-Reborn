using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapInTime : TriggerThing {

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			
			if(!isFirstTrig)
			{
				//init a UI
				Debug.Log("Step in Trap");

			}
			other.gameObject.GetComponent<PlayerStatement>().Dead();
		}


	}
}
