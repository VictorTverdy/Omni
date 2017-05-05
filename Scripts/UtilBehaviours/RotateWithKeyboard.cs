using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithKeyboard : MonoBehaviour {    
    [SerializeField]private bool alwaysRotate = true;

    [SerializeField]private float horizontalSpeed = 2.0F;
    [SerializeField]private float verticalSpeed = 2.0F;

    private bool canRotate;

    public void ListenerEnableRotation() {
        canRotate = true;
    }

    public void ListenerDisableRotation() {
        canRotate = false;
    }

    void Awake() {
        if(alwaysRotate)canRotate = true;
    }
    void FixedUpdate() {
        if(!canRotate)return;

        float h = horizontalSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        float v = verticalSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        //double Rotate to have a better user experience (para que el usuario no se maree)
        transform.Rotate(-v, 0, 0,Space.World);
        transform.Rotate(0, h, 0);
    }
}
