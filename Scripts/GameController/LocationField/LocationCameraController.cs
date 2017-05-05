using UnityEngine;

using Omni.Events;
using Omni.Entities;
using Omni.Utilities;
using VolumetricFogAndMist;
using Omni.UI.ScreensControllers;
using Omni.Utilities.EventHandlers;

namespace Omni.Game.LocationField
{
    public class LocationCameraController : MonoBehaviour
    {

        public GameObject m_fpsControllerObj;
        public GameObject m_normalViemCameraObj;

        [SerializeField] private GameObject m_truckCamera = null;
        [SerializeField] private GameObject m_wellHeadsCamera = null;
        [SerializeField] private GameObject m_specialWellHeadsCamera = null;
        private Animator m_translateCam_fromTopToTowerAnim = null;

        private Transform m_topDownViewPoint;
        private Transform m_transformViewPoint;

        private Camera m_currentCamera;
        private GameViewState m_viewState;
        private InsideFieldLocation m_locationField;        

        public void Awake()
        {
            m_truckCamera.SetActive(false);
            m_wellHeadsCamera.SetActive(false);
            m_fpsControllerObj.SetActive(false);
            m_normalViemCameraObj.SetActive(false);
            m_specialWellHeadsCamera.SetActive(false);
            m_translateCam_fromTopToTowerAnim = m_normalViemCameraObj.GetComponent<Animator>();
            if(m_translateCam_fromTopToTowerAnim == null)Debug.LogError("It needs to have an Animator attached");
            m_translateCam_fromTopToTowerAnim.enabled = false;
        }

        public void Start()
        {
            EventManager.instance.addEventListener(FadeEffectEvent.ON_FADE_IN_ANIMATION_FINISHED, this.gameObject, "OnFadeInEffectFinished");
        }


        public void SetTopDownViewPoint(Transform trans)
        {
            m_topDownViewPoint = trans;

            switch (GameValues.CurrentWellType)
            {
                case EnumWellType.Default:
                    m_specialWellHeadsCamera.GetComponent<VolumetricFog>().enabled = true;
                    m_specialWellHeadsCamera.gameObject.SetActive(true);
                    break;
                case EnumWellType.Drilling:
                    m_normalViemCameraObj.SetActive(true);
                    break;
                case EnumWellType.WorkOver:
                    m_normalViemCameraObj.SetActive(true);
                    break;
                case EnumWellType.Abandoned:
                    m_specialWellHeadsCamera.GetComponent<VolumetricFog>().enabled = true;
                    m_specialWellHeadsCamera.gameObject.SetActive(true);
                    break;
            }
        }

        public void GotoTopView()
        {   
            GoToSpecificPoint(m_topDownViewPoint, GameViewState.FieldView, new LocationAndPosition(InsideFieldLocation.None));
        }

        public void GoToSpecificPoint(Transform pointView, GameViewState viewState, LocationAndPosition locationAndPosition)
        {
            bool preiousViewStateWasFPS = m_viewState == GameViewState.IsInFps;

            m_viewState = viewState;
            m_locationField = locationAndPosition.m_location;
            
            m_transformViewPoint = pointView;

            if (GameValuesConfig.FadeAnimation || preiousViewStateWasFPS)
            {
                UIController.Instance.FadeEffectController.ScreenTransition(0);
            }
            else
            {
                if (m_locationField != InsideFieldLocation.None)
                {
                    if (m_locationField == InsideFieldLocation.SelectedInsideAsset || locationAndPosition.m_isFromBack)
                    {
                        UIController.Instance.FadeEffectController.ScreenTransition(0);
                    }
                    else
                    {
                        switch (m_locationField)
                        {
                            case InsideFieldLocation.WellHeads:
                                m_translateCam_fromTopToTowerAnim.enabled = true;
                                m_translateCam_fromTopToTowerAnim.Play("cam_TopToWellHeads_intro");
                                break;
                            case InsideFieldLocation.Wireline:
                                m_translateCam_fromTopToTowerAnim.enabled = true;
                                m_translateCam_fromTopToTowerAnim.Play("cam_TopToWireLine_intro");
                                break;
                            case InsideFieldLocation.Hse:
                                m_translateCam_fromTopToTowerAnim.enabled = true;
                                m_translateCam_fromTopToTowerAnim.Play("cam_TopToHSE_intro");
                                break;
                            case InsideFieldLocation.DrillingTower:
                                m_translateCam_fromTopToTowerAnim.enabled = true;
                                m_translateCam_fromTopToTowerAnim.Play("cam_TopToTowerView_intro");
                                break;
                        }
                    }
                }
                else
                {
                    UIController.Instance.FadeEffectController.ScreenTransition(0);
                }
            }
        }

        public void ChangeToFpsView()
        {
            m_viewState = GameViewState.IsInFps;
            m_locationField = InsideFieldLocation.None;

            if (!GameValues.isInFPSMode)
            {
                m_viewState = GameViewState.FieldView;
            }
            UIController.Instance.FadeEffectController.ScreenTransition(0);
        }

        //this is called by the animation event at animation end
        public void ListenerOnAnimationEndFinished() {
            FadeEffectEvent evt = new FadeEffectEvent (FadeEffectEvent.ON_FADE_IN_ANIMATION_FINISHED);
			EventManager.instance.dispatchEvent (evt);
        }

        private void OnFadeInEffectFinished(FadeEffectEvent evt)
        {
            bool isFromChangeState = false;
            if (evt.arguments.ContainsKey("isCallFromTransitionState"))
            {
                isFromChangeState = (bool)evt.arguments["isCallFromTransitionState"];
            }

            if (!isFromChangeState)
            {
                if (m_viewState == GameViewState.InAnAsset)
                {

                    switch (m_locationField)
                    {
                        case InsideFieldLocation.DrillingTower:
                        case InsideFieldLocation.Hse:
                            SetCamera(TypeCamera.NormalView);
                            MoveAndSetCamera(m_transformViewPoint);
                            break;
                        case InsideFieldLocation.Wireline:
                            SetCamera(TypeCamera.TruckView);
                            MoveAndSetCamera(m_transformViewPoint);
                            break;
                        case InsideFieldLocation.SelectedInsideAsset:
                            MoveAndSetCamera(m_transformViewPoint);
                            break;
                        case InsideFieldLocation.WellHeads:
                            AssetEvent assetEvt = new AssetEvent(AssetEvent.ON_ACTIVE_WELLHEADS_COLLIDERS);
                            assetEvt.arguments["active"] = true;
                            EventManager.instance.dispatchEvent(assetEvt);
                            SetCamera(TypeCamera.WellHeads);
                            break;
                        default:
                            SetCamera(TypeCamera.SpecialWellHeads);
                            break;
                    }
                }
                else if (m_viewState == GameViewState.IsInFps)
                {
                    SetCamera(TypeCamera.FpsView);
                }
                else
                {
                    SetCamera(TypeCamera.NormalView);
                    MoveAndSetCamera(m_topDownViewPoint);
                }
            }
        }

        private void MoveAndSetCamera(Transform moveTransform)
        {
            m_currentCamera.transform.position = moveTransform.position;
            m_currentCamera.transform.rotation = moveTransform.rotation;
        }

        private void SetCamera(TypeCamera typeCamera)
        {
            Camera actualCamera = null;

            m_truckCamera.SetActive(false);
            m_wellHeadsCamera.SetActive(false);
            m_fpsControllerObj.SetActive(false);
            m_normalViemCameraObj.SetActive(false);
            m_specialWellHeadsCamera.SetActive(false);
           m_translateCam_fromTopToTowerAnim.enabled = false;

            switch (typeCamera)
            {
                case TypeCamera.TruckView:
                    m_truckCamera.SetActive(true);
                    actualCamera = m_truckCamera.GetComponent<Camera>();
                    break;
                case TypeCamera.WellHeads:
                    m_wellHeadsCamera.SetActive(true);
                    actualCamera = m_wellHeadsCamera.GetComponent<Camera>();
                    break;
                case TypeCamera.FpsView:
                    m_fpsControllerObj.SetActive(true);
                    actualCamera = m_fpsControllerObj.GetComponent<Camera>();
                    break;
                case TypeCamera.NormalView:
                    m_normalViemCameraObj.SetActive(true);
                    actualCamera = m_normalViemCameraObj.GetComponent<Camera>();
                    break;
                case TypeCamera.SpecialWellHeads:
                    m_specialWellHeadsCamera.SetActive(true);
                    actualCamera = m_specialWellHeadsCamera.GetComponent<Camera>();
                    break;
            }
            m_currentCamera = actualCamera;
        }

        #region TEST

        public void SetTheMainCameraForTest()
        {
            if (Application.isEditor)
            {
                m_viewState = GameViewState.FieldView;
                m_locationField = InsideFieldLocation.None;

                SetCamera(TypeCamera.NormalView);
                MoveAndSetCamera(m_topDownViewPoint);
            }
        }

        #endregion
    }
}
