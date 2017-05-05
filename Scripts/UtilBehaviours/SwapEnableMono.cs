using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapEnableMono : MonoBehaviour {
    [SerializeField] private MonoBehaviour mono;
    void Awake() {

    }
    public void ListenerSetEnable() {
        swap();
       // mono.enabled = true;
    }

    private void swap() {
        Debug.Log("***swap should work when playing in reverse too");
        mono.enabled = !mono.enabled;
    }

    public void ListenerSetDisable() {
        swap();
        //mono.enabled = false;
    }
}
