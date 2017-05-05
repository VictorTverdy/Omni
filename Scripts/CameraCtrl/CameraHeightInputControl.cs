using UnityEngine;

using Omni.Manager;
using VolumetricFogAndMist;

public class CameraHeightInputControl : MonoBehaviour {
	
	[SerializeField]private float speedZoom = 5f;
    [SerializeField]private float m_minFov = 12f;
    [SerializeField]private float m_maxFov = 25f;
	[SerializeField]private float m_maxAngle = 60f;
    [SerializeField]private float m_minAngle = -20f;
	[SerializeField]private float speedRotate = 50f;
    [SerializeField]private bool zoomEnabled = true;
	[SerializeField]private Transform pointLook = null;

	private Camera myCamera;
    private Vector3 m_rotation;   
    private bool isAutoRotating; 

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
    }

    private void CameraRotate(eInputEvents _event, float _value)
    {		
		if (_event == eInputEvents.axisX) {
			m_rotation.y -= _value * speedRotate * Time.deltaTime;
		}
		if (_event == eInputEvents.axisY) {
			m_rotation.x += _value * speedRotate * Time.deltaTime;
			m_rotation.x = Mathf.Clamp (m_rotation.x, m_minAngle, m_maxAngle);
		}
		pointLook.eulerAngles = m_rotation;

		if (_event == eInputEvents.cameraZoom) {
			if (zoomEnabled)
				CameraZoom (_value);
		}
    }

    private void CameraZoom(float _value)
    {
        float fov = myCamera.fieldOfView;
        fov += _value * speedZoom;
        fov = Mathf.Clamp(fov, m_minFov, m_maxFov);
        myCamera.fieldOfView = fov;
    }

    void Update() {
        if (GameValues.RotationCameraAnimation)
        {
            if (!isAutoRotating)
            {
                return;
            }
            m_rotation.y -= 0.15f * speedRotate * Time.deltaTime;
            pointLook.eulerAngles = m_rotation;
        }
    }

    public void ListenerStartAutoRotate() {
        isAutoRotating=true;
    }

    public void ListenerEndAutoRotate() {
        isAutoRotating = false;
    }
}
