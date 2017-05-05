using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class CollidersEnable : MonoBehaviour {
    [SerializeField] private BoxCollider[] aClickColliders;
    [SerializeField] private bool startEnableClickCollider = true;
    [Space(10)]
    [SerializeField] private BoxCollider[] aWellHeadsTranslateColliders;
    [SerializeField] private bool startEnableWellHeadsTranslateColliders = true;

    // Use this for initialization
    void Start () {
		if(!startEnableClickCollider) {
            ListenerDisableClickColliders();
        }
		if(!startEnableWellHeadsTranslateColliders) {
            ListenerDisableWellHeadsTranslateColliders();
        }
	}

    private void OnEnable() {        
        WellheadsSpecialCameraAnimationEvents.OnSpecialWellHeadSet +=   OnEnterSpecialWellHeadsEvent;  
    }

    private void OnDisable() {        
        WellheadsSpecialCameraAnimationEvents.OnSpecialWellHeadSet -=   OnEnterSpecialWellHeadsEvent;  
    }
    

    private void OnEnterSpecialWellHeadsEvent() {       
        ListenerEnableWellHeadsTranslateColliders();    
        aWellHeadsTranslateColliders[1].enabled = false;     
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ListenerDisableClickColliders() {
        for(int i = 0; i < aClickColliders.Length; i++) {
            aClickColliders[i].enabled = false;
        }
    }

    public void ListenerEnableClickColliders() {
        for(int i = 0; i < aClickColliders.Length; i++) {
            aClickColliders[i].enabled = true;
        }
    }

    public void ListenerDisableWellHeadsTranslateColliders() {
        for(int i = 0; i < aWellHeadsTranslateColliders.Length; i++) {
            aWellHeadsTranslateColliders[i].enabled = false;
        }
    }

    public void ListenerEnableWellHeadsTranslateColliders() {
        for(int i = 0; i < aWellHeadsTranslateColliders.Length; i++) {
            aWellHeadsTranslateColliders[i].enabled = true;
        }
        Debug.Log("por aquiii");
    }
}
