using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrapInitUI : TriggerThing {
	public GameObject canvas;
	private Text thisText;
	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			if(!isFirstTrig)
			{
				Debug.Log("should init a UI");
				canvas.SetActive(true);
			}
		}
	}
	/// <summary>
	/// OnTriggerExit is called when the Collider other has stopped touching the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))		
		{
			if(!isFirstTrig)
			{
				//Debug.Log("should init a UI");
				canvas.SetActive(false);
			}
		}
	}
	
}
