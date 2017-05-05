using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour {
    public float m_speed = 120f;
    [SerializeField]    private bool startEnable = false;

    void Awake()
    {
        enabled = startEnable;
    }
    public void SetEnable(bool enable)
    {
        enabled = enable;
    }

    void Update () {
        RotateObject();
    }

    void RotateObject()
    {
        if (Input.GetMouseButton(1))
        {
            float rotationX = Input.GetAxis("Mouse X") * (-m_speed) * Time.deltaTime;
            float rotationY = Input.GetAxis("Mouse Y") * m_speed * Time.deltaTime;
            
            transform.Rotate(rotationY, rotationX, 0, Space.World);


        }
    }
}
