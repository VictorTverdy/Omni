using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	public CanvasRenderer MainMenuPanel;
	public CanvasRenderer NavigationListPanel;

	public void Start(){
		MainMenuPanel.gameObject.SetActive (true);
		NavigationListPanel.gameObject.SetActive (false);
	}

	public void ShowListOfNavigation(bool showNavigation)
	{
		MainMenuPanel.gameObject.SetActive (!showNavigation);
		NavigationListPanel.gameObject.SetActive (showNavigation);
	}

}
