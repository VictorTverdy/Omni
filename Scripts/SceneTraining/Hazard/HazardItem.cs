using UnityEngine;

namespace Omni.Game.SceneTraining
{
    public class HazardItem : MonoBehaviour, IToDo
    {

        public eHazardLevel Level;
        private GameObject Outline;
        private bool isFind;
        private GameObject iconFind;

        // Use this for initialization
        void Start()
        {
            GameObject _clone = Instantiate(Resources.Load<GameObject>("Prefabs/GamePlay/TrainingItems/FindHazard"));
            _clone.transform.SetParent(transform);
            _clone.transform.localPosition = Vector3.zero;
            iconFind = _clone;
            iconFind.SetActive(false);
            Outline = transform.Find("Outline").gameObject;
         //   Outline.SetActive(false);
        }

        public void SetCanFind()
        {
            if (Outline != null)
                Outline.SetActive(true);
            if (iconFind != null)
                iconFind.SetActive(false);
            isFind = false;
        }     

        public void Action()
        {
            if (!isFind)
            {
                iconFind.SetActive(true);
                HazardItemsManager.Instance.FoundItem(transform);               
                Outline.SetActive(false);
                isFind = true;
            }         
        }
    }
}
