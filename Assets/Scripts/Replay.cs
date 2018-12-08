using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Replay : MonoBehaviour 
{
	//数据读取相关
	[SerializeField]
	private int dir;
	[SerializeField]
	private Vector3 startPosition;
	[SerializeField]
	private Vector3 endPosition;
	[SerializeField]
	private Vector3 jumpPosition;
	[SerializeField]
    private int replayNo;
	private StreamReader sr1;
	private StreamReader sr2;
	//重放相关
	private Rigidbody rb;
	[SerializeField]
	private float replaySpeed;
	[SerializeField]
	private float replayjumpForce;

	private void Start() 
	{
		rb = GetComponent<Rigidbody>();
		if (!File.Exists(Application.dataPath + "/walk" + replayNo +".txt"))
		{
			Debug.Log ("文件未找到");
            return;
		}
        sr1 = File.OpenText (Application.dataPath + "/walk" + replayNo +".txt");
		if (!File.Exists(Application.dataPath + "/jump" + replayNo +".txt"))
		{
			Debug.Log ("文件未找到");
            return;
		}
        sr2 = File.OpenText (Application.dataPath + "/walk" + replayNo +".txt");
		ReadJumpFile();
		endPosition = transform.position;
	}

	private void Update() 
	{
		if (Mathf.Abs(transform.position.x - endPosition.x) >= 0.05f)
		{
			rb.velocity = new Vector3(dir * replaySpeed, rb.velocity.y, 0);
		}
		else if (!sr1.EndOfStream)
		{
			ReadWalkFile();
			transform.position = startPosition;
		}
		else
		{
			transform.position = endPosition;
			rb.velocity = new Vector3(0, rb.velocity.y, 0);
		}
		if (Mathf.Abs(transform.position.x - jumpPosition.x) <= 0.05f && !sr2.EndOfStream)
		{
			rb.AddForce(Vector3.up * replayjumpForce);
			ReadJumpFile();
		}
	}

	private void ReadWalkFile()
	{
		string str = "";
		if (!sr1.EndOfStream)
		{
			str = sr1.ReadLine();
			dir = int.Parse(str);
			str = sr1.ReadLine();
			startPosition = StringToVector(str);
			str = sr1.ReadLine();
			endPosition = StringToVector(str);
		}
	}

	private void ReadJumpFile()
	{
		string str = "";
		if (!sr1.EndOfStream)
		{
			str = sr2.ReadLine();
			jumpPosition = StringToVector(str);
		}
	}

	private Vector3 StringToVector(string str)
	{
		Debug.Log(str);
		str = str.Replace("(", "");
		str = str.Replace(")", "");
		string[] s = str.Split(',');
		return new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
	}
}
