using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Omni.Game;
using Omni.Utilities;
using Omni.CameraCtrl;

public class WellPathManager : MonoBehaviour {
	private static WellPathManager _instance = null;
	public static WellPathManager Instance {get{ return _instance;}}

	public GameObject WellPathRoot;
	public GameObject WellObject;
	private GameObject WellPathScene;
	public GameObject VRCamera;

	void Awake(){
		_instance = this;
	}

	// Use this for initialization
	void Start () {/*
		WellPathRoot = GameObject.Find ("WellPathRoot") as GameObject;
		WellObject = GameObject.Find ("WellObject") as GameObject;*/
	}
	
	// Update is called once per frame
	void Update () {
        /*
		if(WellPathRoot == null)
			WellPathRoot = GameObject.Find ("WellPathRoot") as GameObject;
		if(WellObject == null)
			WellObject = GameObject.Find ("WellObject") as GameObject;
		if (VRCamera == null)
			VRCamera = GameObject.Find ("Camera (eye)") as GameObject;
		
		if (Input.GetKey (KeyCode.LeftArrow)) {
			RotateWellPath (true);
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			RotateWellPath (false);
		} else if (Input.GetKey (KeyCode.UpArrow)) {
			MoveWellPath (false);
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			MoveWellPath (true);
		} else if (Input.GetKey (KeyCode.Escape)) {
			UnloadWellPathScene ();
		}*/
	}

	public void RotateWellPath(bool bLeft){
		if(WellObject == null)
			WellObject = GameObject.Find ("WellObject") as GameObject;
		if (bLeft) {
			WellObject.transform.Rotate (Vector3.up, 5.0f);
		} else {
			WellObject.transform.Rotate (Vector3.up, -5.0f);
		}
	}

	public void MoveWellPath(bool bIn){		
		if(WellPathRoot == null)
			WellPathRoot = GameObject.Find ("WellPathRoot") as GameObject;
		if (VRCamera == null)
			VRCamera = GameObject.Find ("Camera (eye)") as GameObject;
		Vector3 direction = VRCamera.transform.position - WellPathRoot.transform.position;
		direction.y = 0;
		direction = direction.normalized;

		int sign = bIn == true ? 1 : -1;
		WellPathRoot.transform.position += direction * sign * 0.1f;
	}

	public void LoadWellPathScene(){
		string strPrefabPath = "Prefabs/GamePlay/WellPath/WellPathScene";
		Object prefab = Resources.Load(strPrefabPath);

		if(null != prefab && WellPathScene == null){

			WellPathScene = (GameObject)Object.Instantiate(prefab, Vector3.zero, Quaternion.Euler(0, 0, 0));
		}
		else{
			UnloadWellPathScene ();
			//Debug.LogError("Couldn't load prefab: " + strPrefabPath);
		}
	}

	public void UnloadWellPathScene(){
		Destroy (WellPathScene);
	}
}
