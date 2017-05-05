using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//under construction, do not use yet!
public class ProLerpPosition : MonoBehaviour {
    public enum EnumLerpType {None, Default, EaseOut,EaseIn, InvertedExponentialMovement, ExponentialMovement,SmoothStep,Smootherstep };
    [SerializeField] private EnumLerpType lerpType;
    [SerializeField]private float lerpTime = 1f;

    private Vector3 startPos;
    private Vector3 endPos;
    private bool isLerping;
    private float currentLerpTime = 0;
    [Space(10)]
    [SerializeField]private UnityEvent OnEndLerp;
    private Action actionOnEndLerp;

	void Start () {
		startPos = transform.position;
	}
	
    public void ListenerStartLerp(Vector3 _initPos, Vector3 _endPos) {
        startPos = _initPos;          
        endPos = _endPos;          
        isLerping=true;
    }
    public void ListenerStartLerp(Vector3 _initPos, Vector3 _endPos, EnumLerpType _lerpType) {
        startPos = _initPos;          
        endPos = _endPos;          
        lerpType = _lerpType;          
        isLerping=true;
    }
    public void ListenerStartLerp(Vector3 _initPos, Vector3 _endPos, EnumLerpType _lerpType, Action action) {
        startPos = _initPos;          
        endPos = _endPos;          
        lerpType = _lerpType;    
        actionOnEndLerp = action;      
        isLerping=true;
    }

	void Update () {
		if(!isLerping)return;

        currentLerpTime+= Time.deltaTime;
        if(currentLerpTime > lerpTime) {
            currentLerpTime = lerpTime;
        }
        float t = currentLerpTime/lerpTime;
        switch(lerpType) {
            case EnumLerpType.None:
            break;
            case EnumLerpType.Default:
            break;
            case EnumLerpType.EaseOut:
                t = Mathf.Sin(t * Mathf.PI * 0.5f);
            break;
            case EnumLerpType.EaseIn:
                t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
            break;
            case EnumLerpType.ExponentialMovement:
                t = t*t;
            break;
            case EnumLerpType.InvertedExponentialMovement:
                t = t*t;
            break;
            case EnumLerpType.SmoothStep:
                t = t*t * (3f - 2f*t);
            break;
            case EnumLerpType.Smootherstep:
                t = t*t*t * (t * (6f*t - 15f) + 10f);
            break;
            default:
            break;
        }
        transform.localPosition = Vector3.Lerp(startPos, endPos, t);
        if(t == 1) {
            currentLerpTime = 0;
            isLerping = false;
            OnEndLerp.Invoke();
            if(actionOnEndLerp!= null) {
                actionOnEndLerp.Invoke();
            }
        }

    }
}
