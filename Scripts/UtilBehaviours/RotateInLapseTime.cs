using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInLapseTime : MonoBehaviour {
    [SerializeField] private float time;
    [SerializeField] private Transform targetRotation;

	void Start () {	
	}

    public void ListenerStartRotation() {
        StartCoroutine(RotateMe(targetRotation.eulerAngles - transform.eulerAngles, time));
    }
	
     IEnumerator RotateMe(Vector3 byAngles, float inTime) {	
           var fromAngle = transform.rotation;
           var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
           for(var t = 0f; t < 1; t += Time.deltaTime/inTime) {
                transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
                yield return null;
           }
      }
    
}
