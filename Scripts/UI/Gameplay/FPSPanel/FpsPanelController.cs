using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Omni.Utilities.EventHandlers;
using Omni.Events;
using UnityEngine.UI;

public class FpsPanelController : MonoBehaviour {

	public Sprite m_cursorLock;
	public Sprite m_cursorRelease;
	public Image m_mouseLockedImage;

	void Start () {
		EventManager.instance.addEventListener (HUDEvent.ON_SHOW_CURSOR, this.gameObject, "OnShowCursor");
	}

	public void ShowCursorLock()
	{
		m_mouseLockedImage.sprite = m_cursorLock;
	}

	private void OnShowCursor(HUDEvent evt){
		bool mouseIsLocked = (bool)evt.arguments ["cursorIsLocked"];
		if (mouseIsLocked) {
			m_mouseLockedImage.sprite = m_cursorLock;
		} else {
			m_mouseLockedImage.sprite = m_cursorRelease;
		}
	}
}
