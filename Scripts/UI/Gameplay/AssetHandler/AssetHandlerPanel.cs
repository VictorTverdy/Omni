using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Omni.UI.AssetHandler
{
	public class AssetHandlerPanel : MonoBehaviour {

		[SerializeField] protected CanvasRenderer m_icoPanel = null;
		[SerializeField] protected CanvasRenderer m_infoPanel = null;

		public virtual void ShowIcoPanel()
		{
			HideAllPanel ();
			m_icoPanel.gameObject.SetActive (true);
		}

		public virtual void HideAllPanel()
		{
			m_icoPanel.gameObject.SetActive (false);
		}

		public void LateUpdate()
		{
			if (Camera.main != null && m_icoPanel.gameObject.activeSelf) {
				m_icoPanel.transform.LookAt (m_icoPanel.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
			}
		}
	}
}
