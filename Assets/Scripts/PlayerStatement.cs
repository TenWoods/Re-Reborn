using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatement : MonoBehaviour 
{
	private bool isAlive = true;

	public void Dead()
	{
		isAlive = false;
	}
}
