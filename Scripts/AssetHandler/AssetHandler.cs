using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Omni.Events;
using Omni.Entities;
using Omni.Utilities;
using Omni.CameraCtrl;
using Omni.Utilities.EventHandlers;

namespace Omni.Asset
{
	[RequireComponent (typeof (Collider))]
	public class AssetHandler : MonoBehaviour {
		
		public Transform m_viewPointTransform;
		public InsideFieldLocation m_insideFieldLocation;

		protected AssetList m_assetList;
		protected BoxCollider m_currentCollider;
		protected Material m_transparentMaterial;

		public virtual void Awake () 
		{
			m_transparentMaterial = Resources.Load ("Materials/TransparentMaterial") as Material;
		}

		public virtual void Start()
		{
			m_currentCollider = this.GetComponent<BoxCollider> ();
			EventManager.instance.addEventListener (AssetUIEvent.ON_ENABLE_COLLIDER, this.gameObject, "OnEnabledCollider");
			EventManager.instance.addEventListener (AssetUIEvent.ON_CHANGE_TO_INSIDE_VIEW, this.gameObject, "OnUserEnterToFieldEvent");
		}

		public void ConfigureAsset(AssetList asset)
		{
			m_assetList = asset;

			switch (m_assetList.Status) {
				case (int)AssetStatus.Invisible:
					this.gameObject.SetActive(false);
					break;
				case (int)AssetStatus.PendingToArrive:
					Renderer[] renderers = this.gameObject.GetComponentsInChildren<Renderer> ();
					if (renderers != null) {
						for (int j = 0; j < renderers.Length; j++) {
							Renderer rend = renderers [j];
							rend.material = m_transparentMaterial;
						}								
					} else {
						Renderer rend = this.gameObject.GetComponent<Renderer>();
						if (rend != null){
							rend.material = m_transparentMaterial;
						}
					}
					break;
			}
		}

        //this is not executing
		public void ShowInfoOfSelectedAsset()
		{
			HUDEvent evt = new HUDEvent (HUDEvent.ON_SHOW_SELECTED_ASSET_INFO);
			evt.arguments ["selectedAsset"] = 0;
			EventManager.instance.dispatchEvent (evt);
		}

        //called when box collider is clicked
		public virtual void OnMouseDown() { 
			if (GetComponent<DisplayImgOnThisTransform> ()){
				GetComponent<DisplayImgOnThisTransform> ().Hide ();
			}

			NavigateToViewPoint ();
		}

        //called when ui panel is clicked
        public void ListenerClickUIPanel() {   
			if (GetComponent<DisplayImgOnThisTransform> ()) {
				GetComponent<DisplayImgOnThisTransform> ().Hide ();
			}
			NavigateToViewPoint ();
        }

		protected virtual void NavigateToViewPoint()
		{
			AssetUIEvent assetUIEvent = new AssetUIEvent (AssetUIEvent.ON_CHANGE_TO_INSIDE_VIEW);
			assetUIEvent.arguments ["insideFieldLocation"] = m_insideFieldLocation;
			assetUIEvent.arguments ["currentGameObject"] = this;
			assetUIEvent.arguments ["navigationPoint"] = m_viewPointTransform;
			EventManager.instance.dispatchEvent (assetUIEvent);
		}

		protected virtual void OnEnabledCollider()
		{
			m_currentCollider.enabled = true;
		}

		protected virtual void OnUserEnterToFieldEvent(AssetUIEvent evt)
		{
			m_currentCollider.enabled = false;
		}
	}
}