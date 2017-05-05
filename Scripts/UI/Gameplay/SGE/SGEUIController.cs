using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Omni.GameState;
using Omni.Events;
using Omni.Utilities.EventHandlers;

public class SGEUIController : MonoBehaviour {

	public void ShowScreen(int screen)
	{
		HUDEvent evt = new HUDEvent (HUDEvent.ON_SGE_CHANGE_SCREEN);
		evt.arguments ["screenToShow"] = screen;
		EventManager.instance.dispatchEvent (evt);
	}
}
