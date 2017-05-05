using UnityEngine;

namespace Omni.CameraCtrl
{
	public class CameraMouseRotationController : MonoBehaviour {

	    public float minY = -45.0f;
	    public float maxY = 45.0f;

	    public float sensX = 100.0f;
	    public float sensY = 100.0f;

	    float rotationY = 0.0f;
	    float rotationX = 90.0f;

	    void Update()
	    {
	        if (Input.GetMouseButton(1))
	        {
	            rotationX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
	            rotationY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;
	            rotationY = Mathf.Clamp(rotationY, minY, maxY);
	            transform.localEulerAngles += new Vector3(-rotationY, rotationX, 0);
	        }
	    }
	}
}
