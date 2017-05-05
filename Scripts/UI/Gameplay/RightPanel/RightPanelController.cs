using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPanelController : MonoBehaviour {

	public Animator m_animator;

	private bool m_isPanelDisplayed;


	public void ShowPanel()
	{
		m_isPanelDisplayed = !m_isPanelDisplayed;

		m_animator.SetBool ("ShowPanel", m_isPanelDisplayed);
	}
}
