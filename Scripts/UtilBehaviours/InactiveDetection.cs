using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InactiveDetection : MonoBehaviour {

	private bool InactiveTriggered;
    private float InactiveTimeElapsed;
    [SerializeField]private float InactiveTimeSecs = 25;
    [SpaceAttribute(10)]
    [SerializeField]private UnityEvent onInactive;
    [SerializeField]private UnityEvent onActive;
    [HideInInspector]public bool canDetect = true;

	void Update () {
		HandleInactiveTime();
	}

    private void HandleInactiveTime()
    {
        InactiveTimeElapsed += Time.deltaTime;
        if(Input.anyKey) { 
            if (InactiveTriggered) onActive.Invoke();
            InactiveTimeElapsed = 0;
            InactiveTriggered = false;
        }

        if(!canDetect)return;
        if (InactiveTimeElapsed > InactiveTimeSecs && !InactiveTriggered)
        {
            InactiveTriggered = true;
            onInactive.Invoke();
        }
    }

    internal void setActive() {
        onActive.Invoke();
        InactiveTimeElapsed = 0;
        InactiveTriggered = false;
    }
}
