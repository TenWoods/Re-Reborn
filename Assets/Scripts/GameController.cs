﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// 用于游戏内循环大逻辑，重置玩家节点以及调用问题显示
public class GameController : MonoBehaviour {

	[SerializeField]
	private int deadCount = 0;
	[SerializeField]
	private bool isGetEnding = false;

	public List<GameObject> texts = new List<GameObject>();
	public Canvas canvas;

	// 当玩家死了对应次数出现对应问题UI
	void InitQuestion(int count)
	{
		texts[count].SetActive(true);
		// 暂停游戏，等待触发按钮事件
	}
	// Use this for initialization
	void Start () {
		for(int i = 0;i<canvas.transform.GetChildCount();i++)
		{
			texts.Add(canvas.transform.GetChild(i).gameObject);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q))
		{
			InitQuestion(1);
		}
	}

	void CheckEnding()
	{
		// load the next scene
		if(isGetEnding);
	}

	public void AddDeadCount()
	{
		InitQuestion(deadCount);
		deadCount++;
	}
}