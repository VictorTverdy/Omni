using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameSettingsSet : MonoBehaviour {
    [SerializeField]private ImgColorSwap imgColorSwap;
    void Awake() {
        if(GameValues.CloudsAnimation) {
            imgColorSwap.setTrue();
        }else {            
            imgColorSwap.setFalse();
        }
    }

    public void ListenerEnableClouds() {
        GameValuesConfig.SetAndSaveCloudsAnimation(true, gameObject);
    }
    public void ListenerDisableClouds() {
        GameValuesConfig.SetAndSaveCloudsAnimation(false, gameObject);
    }

}
