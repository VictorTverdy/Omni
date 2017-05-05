using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Omni.GameState;

public class TowerInfoUIController : MonoBehaviour {

	public CanvasRenderer VideoPanel;
	public CanvasRenderer StatictisPanel;

	public MovieController m_movieController;

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
			//m_movieController.StreamMovieOil (false);
			VideoPanel.gameObject.SetActive (true);
			break;
		case 1:
			StatictisPanel.gameObject.SetActive (true);
			//m_movieController.StreamMovieOil (true);
			break;
		case 2:
			GameStateManager.Instance.PushState (new WellPathState ());
			break;
		case 3:
			GameStateManager.Instance.PushState (new DrillingToolState ());
			break;
		}
	}
}
