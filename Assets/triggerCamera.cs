using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CineMachine.Examples;
public class triggerCamera : MonoBehaviour {

	public GameObject cinemachine;
	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Player")
		{
			cinemachine.SetActive(true);
			other.GetComponent<Move>().enabled = false;
		}
	}
}
