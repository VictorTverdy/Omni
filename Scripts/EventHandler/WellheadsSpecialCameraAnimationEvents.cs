using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WellheadsSpecialCameraAnimationEvents : MonoBehaviour {
    [Space(10)]
    [SerializeField] private UnityEvent onArrive;
    [SerializeField] private UnityEvent onArriveFirstPause;
    
    public delegate void EnableHUDSpecialWellHeadEvent();
    public static event EnableHUDSpecialWellHeadEvent OnSpecialWellHeadSet;

	public void ListenerOnArrive() {
        
        if(OnSpecialWellHeadSet!=null) {
            OnSpecialWellHeadSet();            
        }
        onArrive.Invoke();
    }
	public void ListenerOnArriveFirstPause() {
        onArriveFirstPause.Invoke();
    }
}
