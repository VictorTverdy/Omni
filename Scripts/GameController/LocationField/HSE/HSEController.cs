using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Omni.Utilities.EventHandlers;
using Omni.Events;

namespace Omni.Game.LocationField.HSE
{
	public class HSEController : MonoBehaviour {

		[SerializeField] private Image m_screenPanel;
		[SerializeField] private Sprite[] m_screenList;
					
		// Use this for initialization
		void Start () {
			EventManager.instance.addEventListener (HUDEvent.ON_SGE_CHANGE_SCREEN, this.gameObject, "OnChangeScreen");

			m_screenPanel.sprite = m_screenList [0];
		}

		private void OnChangeScreen(HUDEvent evt)
		{
			int screenToShow = (int)evt.arguments ["screenToShow"];
			m_screenPanel.sprite = m_screenList [screenToShow];
		}
	}
}