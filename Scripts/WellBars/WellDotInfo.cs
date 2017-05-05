using Omni.CameraCtrl;
using Omni.GameState;
using UnityEngine;
using System;
using Omni.Utilities;
using System.Collections.Generic;

public class WellDotInfo : MonoBehaviour {

	public WellCategory m_wellCategory;
  
	public void ApplyFilter(List<WellCategory> filterCategory) 
	{
		if (filterCategory.Count == 0) {
			this.gameObject.SetActive (false);
		} else {
			bool activeGo = filterCategory.Contains (m_wellCategory);
			this.gameObject.SetActive (activeGo);
		}
	}
}
