using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnController : MonoBehaviour 
{
	[SerializeField]
	private float burnSpeed;
	private Material burnMaterial;
	private float burnAmount = 0;
	public bool burnfinish = false;
	public bool burnStart = false;

	private void Start() 
	{
		burnMaterial = GetComponent<Renderer>().material;
		if (burnMaterial == null)
		{
			this.enabled = false;
		}
	}

	private void Update() 
	{
		if (!burnStart)
		{
			return;
		}
		burnAmount = Mathf.Lerp(burnAmount, 1, Time.deltaTime * burnSpeed);
		burnMaterial.SetFloat("_BurnAmount", burnAmount);
		if (Mathf.Abs(burnAmount - 1) <= 0.1f)
		{
			burnfinish = true;
			this.enabled = false;
		}
	}
}
