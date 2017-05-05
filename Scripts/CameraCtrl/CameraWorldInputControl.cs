using UnityEngine;
using System.Collections;

using Omni.Manager;
using Omni.Events;
using Omni.Utilities.EventHandlers;
using WPM;
using System;

namespace Omni.CameraCtrl
{
    [RequireComponent(typeof(Animator))]
    public class CameraWorldInputControl : MonoBehaviour
    {
		[SerializeField]private GameObject mapUAE;
		[SerializeField]private Transform world;
        [SerializeField]private WorldMapGlobe worldMapGlobe;
               
		[SerializeField]private GameObject countryEmiratos;

        [SerializeField]private RotateWithKeyboard rotateWithKeyboard;
        [SerializeField]private CameraZoomInOutMouseWheel cameraZoom;
        [SerializeField]private InactiveDetection inactiveDetection;
                
        [Tooltip("Rotation to country time")]
        [SerializeField]private float lerpTime = 0.75f;
        private float currentLerpTime = 0;
        private Quaternion startQuat;
        private Quaternion endQuat;
        private Animator animator;
        
        
        private bool canZoom = true;
        private bool canRotate = true;
        private bool canInteract;
        private Country countryHighlight;


        private enum EnumStateWorld { None, Waiting, RotatingToFaceCountry, ZoomingToCountry, ZoomingOutToSpace, WaitingToClickWell};
        private EnumStateWorld state;

        public bool CanZoom {
            get {
                return canZoom;
            }
        }

        public bool CanRotate {
            get {
                return canRotate;
            }
        }

        private void OnEnable()
        {
            Invoke("CheckCloud",0.2f);
            countryHighlight = worldMapGlobe.countryHighlighted;
            canRotate = true;
            rotateWithKeyboard.ListenerEnableRotation();
            state = EnumStateWorld.Waiting;
        }

        private void OnDisable()
        {
        }

        void Start()
        {
            countryHighlight = worldMapGlobe.countryHighlighted;
            mapUAE.SetActive(false);   
            inactiveDetection.canDetect = true;         
        }
        void Awake() {
           animator = GetComponent<Animator>();
        }

        public void CameraMoveToPoint()
        {
            countryHighlight = worldMapGlobe.countryHighlighted;
            if (!CanZoom)return;
            if (countryHighlight != null && countryHighlight.name == "United Arab Emirates")
            {
                setStateToRotatingToFaceCountry(); 
            }
        }

        internal bool GetCanInteract() {
            return canInteract;
        }

        private void setStateToRotatingToFaceCountry() {     
            StartUIAnimation();
            canZoom = false;
            canRotate = false;
            rotateWithKeyboard.ListenerDisableRotation();
            if(Camera.main.fieldOfView<15)
            cameraZoom.Reset();
            cameraZoom.SetEnable(false);
            mapUAE.SetActive(true);
            inactiveDetection.canDetect = false;
            //countryHighlight.hidden = true;
            worldMapGlobe.showCursor = false;
            worldMapGlobe.enableCountryHighlight = false;  
            
            startQuat = world.transform.localRotation;          
            endQuat = countryEmiratos.transform.localRotation;          
            state = EnumStateWorld.RotatingToFaceCountry;
        }

        private void setStateToZoomingToCountry() {
            currentLerpTime = 0;
            animator.Play("WorldZoom_intro");
            mapUAE.SetActive(true);
            //GetComponent<ProLerpPosition>().ListenerStartLerp(transform.position, mapUAE.transform.position,
             //   ProLerpPosition.EnumLerpType.InvertedExponentialMovement);
            state = EnumStateWorld.ZoomingToCountry;
        }

        public void SetStateToZoomingOutToSpace() {
            animator.Play("WorldZoom_out");
            mapUAE.SetActive(false);
            state = EnumStateWorld.ZoomingOutToSpace;
        }

        private void setStateToWaiting() { 
            canZoom = true;
            canRotate = true;
            rotateWithKeyboard.ListenerEnableRotation();
            cameraZoom.SetEnable(true);
            inactiveDetection.canDetect = true;
            //if(countryHighlight != null)countryHighlight.hidden = false;
            worldMapGlobe.showCursor = true;
            worldMapGlobe.enableCountryHighlight = true;
            state = EnumStateWorld.Waiting;
        }
      
        void Update()
        {
            switch(state) {
                case EnumStateWorld.None:
                break;
                case EnumStateWorld.Waiting:
                    if (Input.GetMouseButton(0)) {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit)) {
                            if (hit.transform.CompareTag("Earth")){                      
                                CameraMoveToPoint();
                            }
                        }
                    }
                break;
                case EnumStateWorld.RotatingToFaceCountry:     
                    currentLerpTime+= Time.deltaTime;
                    if(currentLerpTime > lerpTime) {
                        currentLerpTime = lerpTime;
                    }
                    float t = currentLerpTime/lerpTime;
                    //this lerp and tweens are from https://chicounity3d.wordpress.com/2014/05/23/how-to-lerp-like-a-pro/
                    t = Mathf.Sin(t * Mathf.PI * 0.5f); //ease out                   
                    //t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f); //ease in  
                    //t = t*t; //exponial movement     
                    //t = t*t * (3f - 2f*t);//smoothstep  
                    //t = t*t*t * (t * (6f*t - 15f) + 10f);//smootherstep        
                    world.transform.localRotation = Quaternion.Lerp(startQuat, endQuat, t);
                    if(t == 1) {
                        setStateToZoomingToCountry();
                    }
                   

                break;
                case EnumStateWorld.ZoomingToCountry:
                break;
                default:
                break;
            }			
        }         

        public void ListenerAnimationArriveToZoom() {
            switch(state) {
                case EnumStateWorld.None:
                return;
                case EnumStateWorld.Waiting:
                return;
                case EnumStateWorld.RotatingToFaceCountry:
                return;
                case EnumStateWorld.ZoomingToCountry:
                    canInteract = true;
                    state = EnumStateWorld.WaitingToClickWell;
                return;
                case EnumStateWorld.ZoomingOutToSpace:
                    //this is called when you start zooming out, it should be called when it ends to zoomOut
                    setStateToWaiting();
                return;
                case EnumStateWorld.WaitingToClickWell:
                return;
                default:
                break;
            }
           
        }
        private void StartUIAnimation(){
            WorldLevelEvent evt = new WorldLevelEvent(WorldLevelEvent.ON_CAMERA_TO_POINT);
            EventManager.instance.dispatchEvent(evt);
        }

        private void CheckCloud()
        {
            if (worldMapGlobe == null)
                worldMapGlobe = FindObjectOfType<WorldMapGlobe>();
            if (!CanZoom)
            {
                worldMapGlobe.showCursor = false;
                worldMapGlobe.enableCountryHighlight = false;
            }             
        }      
       
    }
}
