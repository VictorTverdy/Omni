using Omni.Events;
using Omni.Utilities.EventHandlers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent (typeof (BoxCollider))]
public class ClickAndTransport : MonoBehaviour {

    [SerializeField] private Transform newPosition;
    [SerializeField] private Transform objectToTranslate;
    [SerializeField] private UnityEvent onClick;
    
    void Start() {
        EventManager.instance.addEventListener(AssetEvent.ON_ACTIVE_WELLHEADS_COLLIDERS, this.gameObject,"ListenerEnableCollider");
    }

    void ListenerEnableCollider(AssetEvent evt) {
        bool active = (bool)evt.arguments["active"];
        GetComponent<BoxCollider>().enabled = active;
    }

    void OnMouseDown() {
        onClick.Invoke();
        objectToTranslate.transform.rotation = newPosition.rotation;

        objectToTranslate.transform.position = newPosition.position;

        GetComponent<BoxCollider>().enabled = false;
        
    }
}
