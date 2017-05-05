using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPanelDisplay : MonoBehaviour {

	void Awake() {
        gameObject.SetActive(false);
    }
	public void ListenerSwapActive() {
        gameObject.SetActive(!isActiveAndEnabled);
    }
}
