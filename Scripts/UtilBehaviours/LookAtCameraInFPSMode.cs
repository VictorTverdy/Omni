using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCameraInFPSMode : MonoBehaviour {

	public Camera m_Camera;
 
    void LateUpdate()
    {
        if(GameValues.isInFPSMode) {
            transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
                            m_Camera.transform.rotation * Vector3.up);
        }
    }
}
