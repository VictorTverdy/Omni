using UnityEngine;
using System.Collections.Generic;
namespace Omni.Game.SceneTraining
{
    public class HazardItemsManager : MonoBehaviour
    {
        public static HazardItemsManager Instance;

        private LookAtPlayer ImageFind;
        private HazardItem[] Items;
        private int maxItems;
        private int currentFoundItem;
        private float timer;
        private bool isTimer;

        // Use this for initialization
        void Awake()
        {            
            if (Instance != null)
                DestroyImmediate(this);
            else
                Instance = this;
            
            Items = transform.GetComponentsInChildren<HazardItem>();
            DeactivateAllItems();
        }

        private void Update()
        {
            if(isTimer)
            timer += Time.deltaTime;
        } 

        public void FoundItem(Transform _pos)
        {
            if (currentFoundItem < maxItems)
            {              
                currentFoundItem++; 
                //HazardInfo.Instance.TextProggres.text = currentFoundItem.ToString() + " / " + maxItems.ToString();
                HazardInfo.Instance.TextProggres.text = (maxItems - currentFoundItem).ToString();
                if(currentFoundItem == maxItems)
                {
                    TrainingComplitUI.Instance.SetTime(timer);
                } 
            }  
        }

        public void ActivateAllItems(eHazardLevel _level)
        {
            currentFoundItem = 0;
            isTimer = true;
            timer = 0;
            int lenght = Items.Length;
            switch (_level)
            {
                case eHazardLevel.easy:
                    maxItems = lenght / 3;
                    break;
                case eHazardLevel.medium:
                    maxItems = lenght / 2;
                    break;
                case eHazardLevel.hard:
                    maxItems = lenght;
                    break;
            }
            HazardInfo.Instance.Content.SetActive(true);
          //  HazardInfo.Instance.TextProggres.text = "0 / " + maxItems.ToString();
         //   HazardInfo.Instance.TextProggres.text = currentFoundItem.ToString() + " / " + maxItems.ToString();
            HazardInfo.Instance.TextProggres.text = (maxItems-currentFoundItem).ToString();

            List<int> numbers = new List<int>();

            for (int i = 0; i < maxItems; i++)
            { 
                int _random = Random.Range(0, lenght);
                if (!numbers.Contains(_random))
                {
                    numbers.Add(_random);
                    Items[_random].gameObject.SetActive(true);
                }
                else
                {
                    i--;
                }
            }
        }    

        public void DeactivateAllItems()
        {
            isTimer = false;
            timer = 0;
            int lenght = Items.Length; 
            for (int i = 0; i < lenght; i++)
            {
                Items[i].SetCanFind();
                Items[i].gameObject.SetActive(false);
            }
        }  
    }
}
