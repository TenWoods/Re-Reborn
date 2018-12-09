using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Review : MonoBehaviour 
{
	[SerializeField]
	private List<GameObject> deaths;
	[SerializeField]
	private bool startReview = false;
	private int count = 0;
	[SerializeField]
	private float moveSpeed;
	public Camera mainCamera;
	public GameObject rocks;
	

	private void Start() 
	{
		deaths = new List<GameObject>();
	}

	private void Update() 
	{
		Debug.Log(deaths.Count);
		if (!startReview)
		{
			return;
		}
		if (count >= deaths.Count)
		{
			this.enabled = false;
			mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = true;
			GameObject.FindWithTag("Player").GetComponent<PlayerStatement>().gameEnd = false;
			GameObject.FindWithTag("Player").GetComponent<Move>().enabled = true;
			rocks.SetActive(true);
			return;
		}
		float x = mainCamera.transform.position.x;
		x = Mathf.Lerp(x, deaths[count].transform.position.x, Time.deltaTime * moveSpeed);
		
		if (Mathf.Abs(x - deaths[count].transform.position.x) <= 0.1f)
		{
			mainCamera.transform.position = new Vector3(deaths[count].transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
			deaths[count].GetComponentInChildren<BurnController>().burnStart = true;
			if (deaths[count].GetComponentInChildren<BurnController>().burnfinish)
			{
				count++;
			}
		}
		else
		{
			mainCamera.transform.position = new Vector3(x, mainCamera.transform.position.y, mainCamera.transform.position.z);
		}
	}

	public void AddDeath(GameObject death)
	{
		deaths.Add(death);
	}

	public bool StartReview 
	{
		set
		{
			startReview = value;
		}
	}
}
