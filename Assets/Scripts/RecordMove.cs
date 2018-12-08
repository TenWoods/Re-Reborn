using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RecordMove : MonoBehaviour 
{
	private PlayerStatement playerStatement;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private int dir;
	private Vector3 jumpPosition;
	[SerializeField]
	private int recordNo;

	private void Start() 
	{
		File.Delete(Application.dataPath + "/walk" + recordNo +".txt");
		File.Delete(Application.dataPath + "/jump" + recordNo +".txt");
		playerStatement = GetComponent<PlayerStatement>();
	}

	private void Update() 
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			dir = -1;
			startPosition = transform.position;
			startPosition.y = 0;
		}
		if (Input.GetKeyUp(KeyCode.A))
		{
			endPosition = transform.position;
			endPosition.y = 0;
			WriteWalkFile();
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			dir = 1;
			startPosition = transform.position;
		}
		if (Input.GetKeyUp(KeyCode.D))
		{
			endPosition = transform.position;
			WriteWalkFile();
		}
		if (Input.GetKeyDown(KeyCode.W))
		{
			jumpPosition = transform.position;
			WriteJumpFile();
		}
		if (!playerStatement.IsAlive)
		{
			endPosition = transform.position;
			WriteWalkFile();
			this.enabled = false;
		}
	}

	private void WriteWalkFile()
	{
		StreamWriter sw;
		FileInfo fi = new FileInfo(Application.dataPath + "/walk" + recordNo +".txt");
		if (!fi.Exists)
		{
			sw = fi.CreateText();
		}
		else
		{
			sw = fi.AppendText();
		}
		Debug.Log(Application.dataPath + "/walk" + recordNo +".txt");
		sw.WriteLine(dir);
		sw.WriteLine(startPosition);
		sw.WriteLine(endPosition);
		sw.Close();
		sw.Dispose();
	}

	private void WriteJumpFile()
	{
		StreamWriter sw;
		FileInfo fi = new FileInfo(Application.dataPath + "/jump" + recordNo +".txt");
		if (!fi.Exists)
		{
			sw = fi.CreateText();
		}
		else
		{
			sw = fi.AppendText();
		}
		sw.WriteLine(jumpPosition);
		sw.Close();
		sw.Dispose();
	}
}
