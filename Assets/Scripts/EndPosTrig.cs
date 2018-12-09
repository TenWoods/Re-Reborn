using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CineMachine.Examples;
public class EndPosTrig : MonoBehaviour 
{
	public AudioSource audioSource;
	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Player")
		{
			GameObject gm = GameObject.Find("GameSystem");
			GameObject player = GameObject.FindWithTag("Player");
			player.GetComponent<PlayerStatement>().KneelDown();
			gm.GetComponent<Review>().mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
			this.GetComponent<Collider>().enabled = false;
			audioSource.Play();
		}
	}
}
