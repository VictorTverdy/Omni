using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Omni.GameState;

public class WirelineUIController : MonoBehaviour {

	public CanvasRenderer VideoPanel;
	public CanvasRenderer StatictisPanel;

	public void Awake()
	{
		ShowScreen(-1);
	}

	public void ShowScreen(int screen)
	{
		VideoPanel.gameObject.SetActive (false);
		StatictisPanel.gameObject.SetActive (false);

		switch (screen) 
		{
			case 0:
				VideoPanel.gameObject.SetActive (true);
				break;
			case 1:
				StatictisPanel.gameObject.SetActive (true);
				break;
		}
	}
}
