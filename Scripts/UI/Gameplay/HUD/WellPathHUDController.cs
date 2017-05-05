using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Omni.CameraCtrl;
using Omni.Events;
using Omni.Utilities.EventHandlers;
using Omni.GameState;

namespace Omni.UI.Gameplay.HUD
{
	public class WellPathHUDController : MonoBehaviour 
	{
		public void BackToDrillTower()
		{
			GameStateManager.Instance.PopState ();
		}
	}
}
