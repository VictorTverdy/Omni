using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Omni.Events;
using Omni.Utilities.EventHandlers;
using Omni.Utilities;

[RequireComponent(typeof(Collider))]
public class SelectableDrillingPrefabItem : MonoBehaviour {
    public EnumDrillingMachinePieces machineType;
    [SerializeField]
    //private bool startEnable = true;
   
    public void SetEnable(bool enable)
    {
        enabled = enable;
    }

    void OnMouseDown()
    {
        if (!DrillingAssamblyDisplay.Instance.isShowingAll) return;
        DrillingAssamblyDisplay.Instance.DisplayJustOneGameObject(gameObject);

		AssetEvent evt = new AssetEvent (AssetEvent.ON_SELECTED_ASSET);
		EventManager.instance.dispatchEvent (evt);

    }
}
