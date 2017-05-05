using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomInOutMouseWheel : MonoBehaviourSingleton<CameraZoomInOutMouseWheel> {
    [SerializeField] private float initialFov = 60f;
    [SerializeField] private float minFov = 15f;
    [SerializeField] private float maxFov = 90f;
    [SerializeField] private float sensitivity = 10f;
    [SerializeField] private bool startEnable = true;
    

    void Awake()
    {
        Camera.main.fieldOfView = initialFov;
        enabled = startEnable;
    }

    void OnEnable()
    {
        Camera.main.fieldOfView = initialFov;
    }

    public void Reset()
    {
        Camera.main.fieldOfView = initialFov;
    }

    public void SetEnable(bool enable)
    {
        enabled = enable;
    }
    void Start () {
		
	}
	
	void Update () {
        float fov = Camera.main.fieldOfView;
        if(fov > maxFov)
        {
            fov = maxFov;
        }
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }
}
