using UnityEngine;
using System.Collections;

using Omni.Manager;
using Omni.Events;
using Omni.Utilities.EventHandlers;

namespace Omni.CameraCtrl
{
    public class CameraInputControl : MonoBehaviour
    {
        public Transform pointLook;
        private float speedRotate = 80f;
        private float speedZoom = 5f;
        private float m_minFov = 12f;
        private float m_maxFov = 20f;
        private float zoomFoV =5;
        private float currentFoV;
        private float cofSpeed;
        private const float cof =0.01f;
        private Vector3 m_rotation;
        private Quaternion startRotation;
        private Camera myCamera;

        private bool canZoom = true; 

        private void OnEnable()
        {
            InputManager.OnClicked += CameraRotate;
        }

        private void OnDisable()
        {
            InputManager.OnClicked -= CameraRotate;
        }

        private void Awake()
        {     
            myCamera = GetComponent<Camera>();
            startRotation = transform.localRotation;
            currentFoV = myCamera.fieldOfView;
        }

        public void StartMoveToWeel(Vector3 pos)
        {
            if (canZoom)
            {
                WellInfoPanel.Instanse.InitInfo(0);
                HeightLevelEvent evt = new HeightLevelEvent(HeightLevelEvent.ON_SHOW_WELL_INFO);
                EventManager.instance.dispatchEvent(evt);
                currentFoV = myCamera.fieldOfView;
                canZoom = false;
                StartCoroutine("MoveToWhell", pos);
            }
        }

        public void StartMoveStartPos()
        {
            StartCoroutine("MoveToStartPos");          
        }

        private void CameraRotate(eInputEvents _event,float _value)
        {
            if (!canZoom)
                return;
            if(_event == eInputEvents.axisX)
            {              
                m_rotation.y -= _value * speedRotate * Time.deltaTime;                       
            }
            if(_event == eInputEvents.axisY)
            {
                m_rotation.x += _value * speedRotate * Time.deltaTime;
             //   m_rotation.x = Mathf.Clamp(m_rotation.x, -20, 60);
            }
            pointLook.eulerAngles = m_rotation;

            if (_event == eInputEvents.cameraZoom )
            {
                CameraZoom(_value);
            }            
        }

        private void CameraZoom(float _value)
        {
            float fov = myCamera.fieldOfView;
            fov += _value * speedZoom;
            fov = Mathf.Clamp(fov, m_minFov, m_maxFov);
            myCamera.fieldOfView = fov;
        }

        private IEnumerator MoveToWhell(Vector3 pos)
        {
            transform.LookAt(pos);
            Quaternion newRotation = transform.localRotation;
            transform.localRotation = startRotation;
            cofSpeed = myCamera.fieldOfView  * cof;           
            for (float i= myCamera.fieldOfView; i > zoomFoV; i-= cofSpeed)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, newRotation, cofSpeed);
                myCamera.fieldOfView = i;
                yield return new WaitForSeconds(0.01f);
            }
            transform.localRotation = newRotation;
        }

        private IEnumerator MoveToStartPos()
        {          
            for (float i = myCamera.fieldOfView; i < currentFoV; i += cofSpeed)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, startRotation, cofSpeed);
                myCamera.fieldOfView = i;
                yield return new WaitForSeconds(0.01f);
            }
            transform.localRotation = startRotation;
            canZoom = true;
        }

    }
}
// 