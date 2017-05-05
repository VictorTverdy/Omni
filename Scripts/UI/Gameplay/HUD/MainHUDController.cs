using UnityEngine;
using Omni.Events;
using Omni.Utilities.EventHandlers;
using Omni.Utilities;

namespace Omni.UI.Gameplay.HUD
{
	public class MainHUDController : MonoBehaviour {

		private GameViewState m_viewState;
		private InsideFieldLocation m_locationField;

		public GameObject m_sgePanelGO;
		public GameObject m_FPSPanelGO;
		public GameObject m_FieldPanelGO;
		public GameObject m_wirelinePanelGO;
		public GameObject m_wellHeadPanelGO;
		public GameObject m_equipmentPanelGO;
		public GameObject m_assetInfoPanelGO;
		public GameObject m_towerDrillPanelGO;
		public GameObject m_changeToFpsButton;        

        void OnEnable() {      
            Debug.Log("MainHUDController go name: " + gameObject.name); 
            if(gameObject.transform.parent != null)     
            Debug.Log("MainHUDController parent name: " + gameObject.transform.parent.name);      
            WellheadsSpecialCameraAnimationEvents.OnSpecialWellHeadSet +=   OnEnterSpecialWellHeadsEvent;  
        }

		public void Start()
		{
			EventManager.instance.addEventListener (FadeEffectEvent.ON_FADE_IN_ANIMATION_FINISHED, this.gameObject, "OnFadeInEffectFinished");
        }

		public void StartUI(GameViewState viewState, InsideFieldLocation locationField)
		{
			ResetLayout ();

			m_viewState = viewState;
			m_locationField = locationField;
			SetView ();
		}

        //this is just when pressing button to pass to FPS Mode
		public void ListenerGoToFPSMode()
		{
			HUDEvent hudEvent = new HUDEvent (HUDEvent.ON_CHANGE_TO_FPS_VIEW);
			EventManager.instance.dispatchEvent (hudEvent);
		}

		//executed from back button
		public void BackToHeightLevelMap()
		{
			HUDEvent hudEvent = new HUDEvent (HUDEvent.ON_BACK_TO_OTHER_VIEW);
			EventManager.instance.dispatchEvent (hudEvent);
		}
		        
		public void ChangeUIDueToLocationChange(GameViewState viewState, InsideFieldLocation locationField)
		{
			m_viewState = viewState;
			m_locationField = locationField;
		}

        private void OnEnterSpecialWellHeadsEvent()
		{   
            enabled = true;
		    m_wellHeadPanelGO.SetActive (true);  
            //OnFadeInEffectFinished();
		}

		private void OnFadeInEffectFinished()
		{
			ResetLayout ();
			SetView ();
		}
			
        void OnDisable() {
            WellheadsSpecialCameraAnimationEvents.OnSpecialWellHeadSet -=   OnEnterSpecialWellHeadsEvent;
        }

        void OnDestroy() {
            WellheadsSpecialCameraAnimationEvents.OnSpecialWellHeadSet -=   OnEnterSpecialWellHeadsEvent;
        }

        private void SetView()
		{
			if (m_viewState == GameViewState.InAnAsset) {
				m_FieldPanelGO.SetActive (true);
				switch (m_locationField) {
					case InsideFieldLocation.DrillingTower:
						m_towerDrillPanelGO.SetActive (true);
						break;
					case InsideFieldLocation.Hse:
						m_sgePanelGO.SetActive (true);
						break;
					case InsideFieldLocation.Wireline:
						m_wirelinePanelGO.SetActive (true);                      
						break;
					case InsideFieldLocation.WellHeads:
						m_wellHeadPanelGO.SetActive (true);                      
						break;
				}
			} else if (m_viewState == GameViewState.IsInFps) {
				m_FPSPanelGO.SetActive (true);
				m_FPSPanelGO.GetComponent<FpsPanelController> ().ShowCursorLock ();
			} else {
				m_FieldPanelGO.SetActive (true);
				m_changeToFpsButton.SetActive (true);
			}
		}

		private void ResetLayout()
		{			
			m_FPSPanelGO.SetActive(false);
			m_sgePanelGO.SetActive (false);
			m_FieldPanelGO.SetActive (false);
			m_wellHeadPanelGO.SetActive(false);
			m_wirelinePanelGO.SetActive (false);
			m_equipmentPanelGO.SetActive (false);
			m_assetInfoPanelGO.SetActive (false);
			m_changeToFpsButton.SetActive (false);
			m_towerDrillPanelGO.SetActive (false);  
		}
	}
}
