using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToFirstPosition : MonoBehaviour {
    private Vector3 initPosition;
    private Vector3 initRotation;
    void Awake() {
        initPosition = transform.position;
        initRotation = transform.eulerAngles;
    }
    public void ListenerBackToPositionAndRotation() {
        transform.position = initPosition;
        transform.eulerAngles = initRotation;
    }
    
    void OnDisable() {
        transform.position = initPosition;
        transform.eulerAngles = initRotation;
    }
}
