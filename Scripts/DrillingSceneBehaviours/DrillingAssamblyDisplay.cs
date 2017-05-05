using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Omni.Utilities.EventHandlers;
using Omni.Events;
using Omni.Manager;
using Omni.Utilities;

public class DrillingAssamblyDisplay : MonoBehaviourSingleton<DrillingAssamblyDisplay> {

	public float m_speed = 120f;
    [SerializeField]   private GameObject[] vGo;
    [SerializeField]   private Transform[] vTransforms;
    private Vector3[] vInitialPositions;
    private Vector3[] vInitialScale;
    private Quaternion[] vInitialQuat;
    private Quaternion initialQuat;
    public bool isShowingAll = true;

    public EnumDrillingMachinePieces currentMachineSelected;

    private ObjectRotation oRotation;

    public void Awake()
	{
        initialQuat = transform.rotation;
        oRotation = GetComponent<ObjectRotation>();

        EventManager.instance.addEventListener (AssetEvent.ON_BACK_TO_ALL_VIEW, this.gameObject, "OnShowAllAssembly");
        vInitialPositions = new Vector3[vGo.Length];
        vInitialScale = new Vector3[vGo.Length];
        vInitialQuat = new Quaternion[vGo.Length];
        for (int i = 0; i < vGo.Length; i++)
        {
            vInitialPositions[i] = vGo[i].transform.position;
            vInitialScale[i] = vGo[i].transform.localScale;
            vInitialQuat[i] = vGo[i].transform.localRotation;
        }
	}

	public void Update()
	{
        
    }

    public void DisplayJustOneGameObject(GameObject go)
    {
        oRotation.SetEnable(false);
        resetRotation();
        CameraZoomInOutMouseWheel.Instance.Reset();

        for (int i = 0; i < vGo.Length; i++)
        {
            bool disable = true;
            if(vGo[i].name == go.name)
            {
                disable = false;
                go.transform.position = vTransforms[i].position;
                go.transform.localScale = vTransforms[i].localScale;
                vGo[i].GetComponent<ObjectRotation>().SetEnable(true);
                vGo[i].GetComponent<OutlineDisplay>().SetDisable();
                currentMachineSelected = vGo[i].GetComponent<SelectableDrillingPrefabItem>().machineType;
            }
            if (disable) vGo[i].SetActive(false);
               
        }
        isShowingAll = false;
    }

	private void OnShowAllAssembly()
    {
        oRotation.SetEnable(true);
        resetRotation();
        CameraZoomInOutMouseWheel.Instance.Reset();
        isShowingAll = true;
		for (int i = 0; i < vGo.Length; i++)
		{
            vGo[i].GetComponent<OutlineDisplay>().SetEnable();
            vGo[i].GetComponent<ObjectRotation>().SetEnable(false);
            vGo[i].transform.position = vInitialPositions[i];
            vGo[i].transform.localScale = vInitialScale[i];
            vGo[i].transform.localRotation = vInitialQuat[i];

            vGo[i].SetActive(true);
		}
	}

    private void resetRotation()
    {
        transform.rotation = initialQuat;
    }
}
