using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour {
    
	[SerializeField]private float speedAutoRotate = 50f;
    private bool isRotating = false;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameValuesConfig.RotationCameraAnimation)
        {
            if (!isRotating)
            {
                return;
            }

            Vector3 m_rotation = transform.localEulerAngles;
            m_rotation.y += 0.15f * speedAutoRotate * Time.deltaTime;
            transform.localEulerAngles = m_rotation;
        }
	}

    public void ListenerStartAutoRotate() {
        isRotating=true;
    }

    public void ListenerEndAutoRotate() {
        isRotating = false;
    }
}
