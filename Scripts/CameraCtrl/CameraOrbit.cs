using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using Omni.Asset;
using Omni.GameState;
using Omni.Events;
using Omni.Utilities.EventHandlers;

namespace Omni.CameraCtrl
{
    public class CameraOrbit : MonoBehaviour
    {

        public float m_orbitSpeed = 10f;

        private bool isRotating;
        private Transform m_lookAtPoint;
        private Vector3 m_lookAtPosition;

        public void Start()
        {
            EventManager.instance.addEventListener(CameraEvent.ON_SET_CAMERA_ORBIT, this.gameObject, "OnSetCameraOrbit");            
        }

        public void Update()
        {

            if (m_lookAtPoint != null)
            {
                float forward = 0;
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    forward = -1;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    forward = 1;
                }

				//AutoRotate
				if (GameValuesConfig.RotationCameraAnimation && !GameValues.disableOrbit && isRotating)
				{
					forward = 1;
				}

                if (forward != 0 && !GameValues.disableOrbit)
                {
                    transform.RotateAround(m_lookAtPosition, new Vector3(0.0f, 1.0f, 0.0f), Time.deltaTime * m_orbitSpeed * forward);
                }
            }
        }

        public void ListenerStartAutoRotate()
        {
            isRotating = true;
        }

        public void ListenerEndAutoRotate()
        {
            isRotating = false;
        }

        private void OnSetCameraOrbit(CameraEvent evt)
        {
            Transform lookAtPoint = (Transform)evt.arguments["target"];
            m_lookAtPosition = lookAtPoint.position;
            m_lookAtPoint = lookAtPoint;
        }
    }
}
