using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Omni.GameState;

public class EquipmentInfoUIController : MonoBehaviour {

	public CanvasRenderer[] m_panelArray;

	public void Awake()
	{
		ShowScreen(-1);
	}

	public void ShowScreen(int screen)
	{
		for (int i = 0; i < m_panelArray.Length; i++) {
			m_panelArray[i].gameObject.SetActive (false);
		}

		if (screen >= 0) {
			m_panelArray[screen].gameObject.SetActive (true);
		}
	}
}
