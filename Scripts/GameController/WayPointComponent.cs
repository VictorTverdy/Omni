using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class WayPointComponent : MonoBehaviour {

	public List<GameObject> m_relevat3dPositions;

	void Start(){
		//GameObject[] relevat3dPositions = this.GetComponentsInChildren<GameObject>();

		//Debug.Log ("NUMERO DE ELEMENTOS " + relevat3dPositions.Length);
		//m_relevat3dPositions = relevat3dPositions.ToList<GameObject> ();
	}

	public List<GameObject> GetRelevantPositions()
	{
		return m_relevat3dPositions;
	}

	public GameObject GetRelevantPositionByIndex(int positionIndex)
	{
		return m_relevat3dPositions[positionIndex];
	}
}
