using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineUIController : MonoBehaviour {

	public CanvasRenderer ButtonPanel;
	public CanvasRenderer StatictisPanel;


	public void Awake(){
		ShowStatictis (false);
	}

	public void ShowStatictis(bool showStatisticsPanel)
	{
		StatictisPanel.gameObject.SetActive (showStatisticsPanel);
		ButtonPanel.gameObject.SetActive (!showStatisticsPanel);
	}
}
