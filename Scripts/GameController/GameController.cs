using UnityEngine;
using Omni.Entities;
using Omni.Singletons;
using Omni.Asset;
using Omni.Utilities.EventHandlers;
using Omni.Events;
using Omni.Game.LocationField;
using Omni.Utilities;
using System.Collections.Generic;
using Omni.GameState;
using Omni.UI.Gameplay.HUD;

namespace Omni.Game
{
    [RequireComponent(typeof(MainHUDController))]
    public class GameController : MonoBehaviour
    {

        [SerializeField] private Transform m_orbitPoint = null;
        [SerializeField] private Transform m_topDownWaypoint = null;
        [SerializeField] private MainHUDController m_uiController = null;
        [SerializeField] private OilFieldGOsDisplay m_oilFieldGOsDisplay = null;
        [SerializeField] private LocationCameraController m_locationCameraController = null;

        private GenericList<KeyValuePair<GameViewState, LocationAndPosition>> m_currentViewState;

        public void Start()
        {
            EventManager.instance.addEventListener(HUDEvent.ON_CHANGE_TO_FPS_VIEW, this.gameObject, "OnChangeViewToFps");
            EventManager.instance.addEventListener(HUDEvent.ON_BACK_TO_OTHER_VIEW, this.gameObject, "OnBackToPreviousView");
            EventManager.instance.addEventListener(AssetUIEvent.ON_CHANGE_TO_INSIDE_VIEW, this.gameObject, "OnEnterToFieldEvent");

            m_currentViewState = new GenericList<KeyValuePair<GameViewState, LocationAndPosition>>();

            LocationAndPosition locationEntity = new LocationAndPosition(InsideFieldLocation.None, null);
            m_currentViewState.Add(new KeyValuePair<GameViewState, LocationAndPosition>(GameViewState.FieldView, locationEntity));
        }

        public void RunGame()
        {
           
        }

        public void SetupGame()
        {
            m_uiController.StartUI(GameViewState.FieldView, InsideFieldLocation.None);

            //This class hides all elements depending of the selected well type
            m_oilFieldGOsDisplay.ConfigureLocation(GameValues.CurrentWellType);
            ConfigureCameraToTopDownView();
            SetStatusOfGameObject();

            StartCoroutine("WaitNextFrameToActiveCameraRotaion");            
        }

        private void ConfigureCameraToTopDownView()
        {
            m_locationCameraController.SetTopDownViewPoint(m_topDownWaypoint);
        }

        private void SetStatusOfGameObject()
        {
            AssetList[] assetList = UserDataSettings.Instance.AssetList;

            for (int i = 0; i < assetList.Length; i++)
            {
                AssetList asset = assetList[i];

                GameObject assetGO = GameObject.Find(asset.GameObjectName);

                if (assetGO != null)
                {
                    AssetHandler handler = assetGO.GetComponent<AssetHandler>();
                    if (handler != null)
                    {
                        handler.ConfigureAsset(asset);
                    }
                }
            }
        }

        //Go to scpecif location
        private void OnEnterToFieldEvent(AssetUIEvent evt)
        {
            GameValues.isInFPSMode = false;
            GameValues.disableOrbit = true;
            Transform viewPointTransform = (Transform)evt.arguments["navigationPoint"];
            InsideFieldLocation location = (InsideFieldLocation)evt.arguments["insideFieldLocation"];
            LocationAndPosition locationEntity = new LocationAndPosition(location, viewPointTransform);

            m_locationCameraController.GoToSpecificPoint(viewPointTransform, GameViewState.InAnAsset, locationEntity);

            m_uiController.ChangeUIDueToLocationChange(GameViewState.InAnAsset, location);
                        
            m_currentViewState.Add(new KeyValuePair<GameViewState, LocationAndPosition>(GameViewState.InAnAsset, locationEntity));
        }

        private void OnChangeViewToFps()
        {
            GameValues.isInFPSMode = true;
            m_locationCameraController.ChangeToFpsView();
            m_uiController.ChangeUIDueToLocationChange(GameViewState.IsInFps, InsideFieldLocation.None);

            LocationAndPosition locationEntity = new LocationAndPosition(InsideFieldLocation.None, null);
            m_currentViewState.Add(new KeyValuePair<GameViewState, LocationAndPosition>(GameViewState.IsInFps, locationEntity));
        }

        private void OnBackToPreviousView()
        {
            KeyValuePair<GameViewState, LocationAndPosition> currentState = m_currentViewState[m_currentViewState.Count - 1];
            if (currentState.Key == GameViewState.InAnAsset && currentState.Value.m_location == InsideFieldLocation.WellHeads)
            {
                AssetEvent assetEvt = new AssetEvent(AssetEvent.ON_ACTIVE_WELLHEADS_COLLIDERS);
                assetEvt.arguments["active"] = false;
                EventManager.instance.dispatchEvent(assetEvt);
            }

            m_currentViewState.PopBack();

            if (m_currentViewState.Count > 1)
            {
                KeyValuePair<GameViewState, LocationAndPosition> state = m_currentViewState[m_currentViewState.Count - 1];

                LocationAndPosition locationEntity = state.Value;
                locationEntity.m_isFromBack = true;

                m_uiController.ChangeUIDueToLocationChange(state.Key, locationEntity.m_location);

                switch (state.Key)
                {
                    case GameViewState.InAnAsset:
                        m_locationCameraController.GoToSpecificPoint(locationEntity.m_transformPosition, state.Key, locationEntity);
                        break;
                    case GameViewState.IsInFps:
                        GameValues.isInFPSMode = true;
                        AssetUIEvent assetUIEvent = new AssetUIEvent(AssetUIEvent.ON_ENABLE_COLLIDER);
                        EventManager.instance.dispatchEvent(assetUIEvent);
                        m_locationCameraController.ChangeToFpsView();
                        break;
                }
            }
            else if (m_currentViewState.Count == 1)
            {
                GameValues.isInFPSMode = false;
                GameValues.disableOrbit = false;
                m_locationCameraController.GotoTopView();
                m_uiController.ChangeUIDueToLocationChange(GameViewState.FieldView, InsideFieldLocation.None);
                
                AssetUIEvent assetUIEvent = new AssetUIEvent(AssetUIEvent.ON_ENABLE_COLLIDER);
                EventManager.instance.dispatchEvent(assetUIEvent);
            }
            else
            {
                GameStateManager.Instance.PopState();
            }
        }

        public void TestScene()
        {
            if (Application.isEditor)
            {
                GameValuesConfig.SetAndSaveCloudsAnimation(false);
                m_oilFieldGOsDisplay.ConfigureLocation(EnumWellType.Drilling);
                m_uiController.StartUI(GameViewState.FieldView, InsideFieldLocation.None);

                ConfigureCameraToTopDownView();
                m_locationCameraController.SetTheMainCameraForTest();

                StartCoroutine("WaitNextFrameToActiveCameraRotaion");
            }
        }

        private System.Collections.IEnumerator WaitNextFrameToActiveCameraRotaion()
        {
            yield return new WaitForEndOfFrame();

            CameraEvent evt = new CameraEvent(CameraEvent.ON_SET_CAMERA_ORBIT);
            evt.arguments["target"] = m_orbitPoint;
            EventManager.instance.dispatchEvent(evt);
        }
    }
}