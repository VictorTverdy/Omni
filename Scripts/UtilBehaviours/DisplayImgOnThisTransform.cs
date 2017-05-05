using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (BoxCollider))]
public class DisplayImgOnThisTransform : MonoBehaviour {
    
    [SerializeField]private RectTransform canvasRT;
    [SerializeField]private RectTransform rtToShowInFieldView;
    [SerializeField]private RectTransform rtToShowInFPSMode;
    [SpaceAttribute(10)]
    [SerializeField]private UnityEvent onClick;

    [Header("Raycast Settings")]
    public LayerMask RaycastLayer;

    private bool isShowing = false;
    private bool isInRaycast;

    void Awake() {
        rtToShowInFieldView.gameObject.SetActive(false);
        if(rtToShowInFPSMode)rtToShowInFPSMode.gameObject.SetActive(false);
    }

    #region previous methods
    void OnMouseEnter() { 
        /*
        currentRtToShow().gameObject.SetActive(true);  
        isShowing = true; */
    }    
    void OnMouseExit() {
       /*
        isShowing = false; 
        currentRtToShow().gameObject.SetActive(false);  */
    } 
    void OnMouseDown() {     
        /*  
        isShowing = false;
        rtToShowInFPSMode.gameObject.SetActive(false); */
    }
    #endregion

    void OnRaycastEnter() {
        isInRaycast = true;
        isShowing = true;
        currentRtToShow().gameObject.SetActive(true); 
    }
    void OnRaycastExit() {
        isInRaycast = false;
        isShowing = false;
        currentRtToShow().gameObject.SetActive(false); 
    }

    void Update() {

        //if (InputUtils.IsPointerOverUiObject()) return;
        RaycastHit hit;
		if (Camera.main != null) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			bool isIn = false;
			//here we need to prevent to enter Raycasy when something is above, like the left panel...
			if (Physics.Raycast (ray, out hit, raycastLenght (), RaycastLayer)) {
				if (hit.transform.gameObject == this.gameObject) {
					isIn = true;
					//never get click here because the gameobject dissapears before arrive here
					if (Input.GetMouseButtonDown (0)) {                   
					}
				}
			}
			if (!isIn) {
				if (!isInRaycast)
					return;
				OnRaycastExit ();
			} else {                        
				if (isInRaycast)
					return;
				OnRaycastEnter ();
			}
		}
    }

    private float raycastLenght() {
        if(GameValues.isInFPSMode) {
            return GameValues.RaycastLenghtFPS;
        }else {
            return GameValues.RaycastLenghtTopView;
        }
    }

    void LateUpdate() {   
        if(GameValues.isInFPSMode)return; 
        if(isShowing) {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
            rtToShowInFieldView.anchoredPosition = screenPoint - canvasRT.sizeDelta / 2f;
        }    
    }

    public void Hide() {
        isShowing = false;
        isInRaycast = false;
        currentRtToShow().gameObject.SetActive(false);
    }

    private RectTransform currentRtToShow() {
        RectTransform rt;
        if(GameValues.isInFPSMode) {
            rt = rtToShowInFPSMode;
        }else {
            rt = rtToShowInFieldView;
        }
        return rt;
    }
}
