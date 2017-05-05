using UnityEngine;
using Omni.Utilities;

public class OilFieldGOsDisplay : MonoBehaviour
{
    [SerializeField] private GameObject[] aDefaultWellGOHidens;
    [SerializeField] private GameObject[] aDrillingWellGOHidens;
    [SerializeField] private GameObject[] aWorkoverWellGOHidens;
    [SerializeField] private GameObject[] aAbandonedWellGOHidens;

    public void ConfigureLocation(EnumWellType currentWellType)
    {
        Debug.Log("***GameValues.CurrentWellType" + GameValues.CurrentWellType);

        switch (currentWellType)
        {
            case EnumWellType.Default:
                DisableChilds(aDefaultWellGOHidens);                
                break;
            case EnumWellType.Drilling:
                DisableChilds(aDrillingWellGOHidens);                
                break;
            case EnumWellType.WorkOver:
                DisableChilds(aWorkoverWellGOHidens);                
                break;
            case EnumWellType.Abandoned:
                DisableChilds(aAbandonedWellGOHidens);                
                break;
        }
    }

    private void DisableChilds(GameObject[] gameObjectArray)
    {
        for (int i = 0; i < gameObjectArray.Length; i++)
        {
            gameObjectArray[i].gameObject.SetActive(false);
        }
    }

}
