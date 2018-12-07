using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerThing : MonoBehaviour {
	public GameController gameSystem;
	public GameObject myCanvas;

	protected bool isFirstTrig = false;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		//canvas = GameObject.Find("")
	}

	public void OnCallGameSystem()
	{
		gameSystem.SendMessage("AddDeadCount");
	}
}
