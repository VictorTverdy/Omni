using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointController : MonoBehaviour {

	private GameObject m_mainCameraObject;
	private WayPointComponent m_currentWayPointComponent;



	public WayPointComponent CurrentWayPointComponent
	{
		get { return m_currentWayPointComponent; }
		set { m_currentWayPointComponent = value; }
	}
}
