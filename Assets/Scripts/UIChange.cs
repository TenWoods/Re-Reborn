using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class UIChange : MonoBehaviour 
{
	[SerializeField]
	private List<string> uiStrings;
	private bool changeBackground;
	public float targetColor_a;
	[SerializeField]
	private float changeSpeed;
	public Text uiText;
	public Image background;
	public GameObject rebornButton;
	public GameObject giveUpButton;
	public GameController gc;

	private void Update() 
	{
		if (changeBackground)
		{
			float a = background.color.a;
			a = Mathf.Lerp(a, targetColor_a, Time.deltaTime * changeSpeed);
			if (Mathf.Abs(a - targetColor_a) <= 0.1f)
			{
				a = targetColor_a;
				changeBackground = false;
			}
			background.color = new Color(background.color.r, background.color.g, background.color.b, a);
		}
	}

	public void SetUIText(int i)
	{
		uiText.gameObject.SetActive(true);
		rebornButton.SetActive(true);
		giveUpButton.SetActive(true);
		if (i >= uiStrings.Count)
			return;
		uiText.text = uiStrings[i];
	}

	public void CloseUI()
	{
		uiText.gameObject.SetActive(false);
		rebornButton.SetActive(false);
		giveUpButton.SetActive(false);
	}

	public void RebornButton()
	{
		changeBackground = true;
		targetColor_a = 0;
		GameObject g = Instantiate(gc.player,gc.startPos,Quaternion.Euler(0,0,0));
		gc.player = g;
		CloseUI();
	}

	public void GiveUpButton()
	{
		EditorSceneManager.LoadScene(1);
	}

	public bool ChangeBackground 
	{
		set
		{
			changeBackground = value;
		}
	}

}
