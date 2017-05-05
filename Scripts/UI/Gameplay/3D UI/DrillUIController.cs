using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillUIController : MonoBehaviour {

	public CanvasRenderer MenuPanel;
	public CanvasRenderer InfoPanel;


	public void Start()
	{
		MenuPanel.gameObject.SetActive (true);
		InfoPanel.gameObject.SetActive (false);
	}

	public void ChangeView(bool showInfo)
	{
		MenuPanel.gameObject.SetActive (!showInfo);
		InfoPanel.gameObject.SetActive (showInfo);
	}
}
