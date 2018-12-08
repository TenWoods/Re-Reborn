using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatement : MonoBehaviour 
{
	private bool isAlive = true;

	public void Dead()
	{
		Debug.Log("Die");
		isAlive = false;
	}

	public bool IsAlive
	{
		get
		{
			return isAlive;
		}
	}
}
