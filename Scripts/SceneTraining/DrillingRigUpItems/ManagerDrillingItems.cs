using UnityEngine;
using System.Linq;

namespace Omni.Game.SceneTraining
{
    public enum eDrillingSequence
    {
        Drillbit = 0,
        MudMotor = 1,
        SilckMonel = 2
    }

    public class ManagerDrillingItems : MonoBehaviour
    {
        public static ManagerDrillingItems Instance;       
        public GameObject wrongText;
        public int currentSequence;
        public DrillingItem[] items;
        public Animator FinishCorredor;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                DestroyImmediate(this);         
            items = transform.GetComponentsInChildren<DrillingItem>();
        } 

        public void ItemsStartPosition(bool showWrongText)
        {
            currentSequence = -1;
            for (int i = 0; i < items.Length; i++)
            {
                items[i].StartPosition();
            }
            wrongText.SetActive(showWrongText);
        } 

        private void ItemsStartPosition()
        {
            currentSequence = -1;
            for (int i = 0; i < items.Length; i++)
            {
                items[i].StartPosition();
            }
            wrongText.SetActive(false);
        }

        public void ActivateSequence(eDrillingSequence _item)
        {
            if ((int)_item == currentSequence + 1)
            {
                items.First(x => x.sequence == _item).MoveEndPoint();                
                currentSequence++; 
                if(currentSequence == items.Length - 1)
                {
                    Invoke("SetFinishPosition",1);
                }
            }
            else
            {
                ItemsStartPosition(true);
            }
        }
       
        public void SetFinishPosition()
        {
            if(FinishCorredor == null)
            {
                FinishCorredor = GameObject.Find("corredor central").GetComponent<Animator>(); 
            }
            for (int i = 0; i < items.Length; i++)
            {
                items[i].SetParentFinish(FinishCorredor.transform);
            }
            FinishCorredor.Play("Corredor");
            Invoke("ItemsStartPosition",4);
        }
    }
}
