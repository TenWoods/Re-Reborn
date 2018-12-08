using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapInTime : TriggerThing {

	public UIChange change;
	public int index;
	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			Debug.Log(other.gameObject.name);
			if(!isFirstTrig)
			{
				//init a UI
				Debug.Log("Step in Trap");

			}
			other.gameObject.GetComponent<PlayerStatement>().Dead();
			change.SetUIText(index);
			OnCallGameSystem();
		}


	}
}
