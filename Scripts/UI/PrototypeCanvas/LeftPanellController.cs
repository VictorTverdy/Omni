using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPanellController : MonoBehaviour {

    [SerializeField] private List<GameObject> LeftPopUp;

    public void OpenLeftPopUp()
    {
        for (int i = 0; i < LeftPopUp.Count; i++)
        {
            LeftPopUp[i].SetActive(!LeftPopUp[i].activeSelf);
        }
    }


}
