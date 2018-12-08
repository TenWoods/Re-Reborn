using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapInSticky : TriggerThing {

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		// TODO 调用一个使玩家减速的函数
		if(other.gameObject.CompareTag("Player"))
		{
			other.gameObject.GetComponent<Move>().GlueSet();
			//Debug.Log("Slow down");
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
			other.gameObject.GetComponent<Move>().GlueReset();
			//Debug.Log("restart");
		}
	}
}
