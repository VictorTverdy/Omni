using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Omni.Events;
using Omni.Entities;
using Omni.Utilities;
using Omni.CameraCtrl;
using Omni.UI.AssetHandler;
using Omni.Utilities.EventHandlers;

namespace Omni.Asset
{
	public class InsideAssetHandler : AssetHandler {

		public AssetHandlerPanel m_assetHandlerPanel;

		private bool m_assetIsSelected;

		public override void Start()
		{
			base.Start ();
			m_insideFieldLocation = InsideFieldLocation.SelectedInsideAsset;

			EventManager.instance.addEventListener (HUDEvent.ON_BACK_TO_OTHER_VIEW, this.gameObject, "OnBackToPreviousView");
		}

		public void Update() {

			if (Camera.main) {
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit, 1)) {
					if (hit.transform.gameObject == this.gameObject) {
						m_assetHandlerPanel.ShowIcoPanel ();

					} else {
						if (!m_assetIsSelected) {
							m_assetHandlerPanel.HideAllPanel ();
						}
					}
				} else {
					if (!m_assetIsSelected) {
						m_assetHandlerPanel.HideAllPanel ();
					}
				}
			}
		}

		public override void OnMouseDown() {
			base.OnMouseDown ();
			m_assetIsSelected = true;
			m_assetHandlerPanel.HideAllPanel ();
		}

		protected override void OnUserEnterToFieldEvent(AssetUIEvent evt)
		{
			AssetHandler dispatcherClass = (AssetHandler)evt.arguments ["currentGameObject"];
			if(this == dispatcherClass)
			{
				m_currentCollider.enabled = false;
			}
		}

		private void OnBackToPreviousView(){
			m_assetIsSelected = false;
			OnEnabledCollider ();
		}
	}
}