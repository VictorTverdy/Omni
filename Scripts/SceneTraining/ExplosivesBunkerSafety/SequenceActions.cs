using UnityEngine;
using System.Linq;
namespace Omni.Game.SceneTraining.Box
{
    public class SequenceActions : MonoBehaviour
    {
        public Transform PointMove;

        private Situation[] situations;
        private int currentItem = -1;        

        void Awake() {
        }
        void OnEnable()
        {
            if (situations == null)
                situations = transform.GetComponentsInChildren<Situation>();

            for (int i=0;i< situations.Length;i++) 
            {
                situations[i].gameObject.SetActive(false);
            }
        }        

        public void ActivateStart()
        {
            currentItem = -1; 
            ActivateNextItem();
        }

        public void ActivateNextItem()
        {
            currentItem++;
            if (currentItem < situations.Length)
                ActivateItem(currentItem);
        }

        public Situation GetItem(eSituations situation)
        {
            return situations.First(x => x.situation == situation);
        }

        public eSituations GetCurrentItem()
        {
            if (currentItem > situations.Length)
            {
                return eSituations.none;

            }               
            else
            {
                Situation item = situations.First(x => x.situation == situations[currentItem].situation);
                return item.situation;
            }           
        }

        public void PointMoveAction()
        {
            if(currentItem == 0)
            {
                ActivateNextItem();
            }
            if (currentItem == 2)
            {
                ActivateNextItem();
            }
        }

        public void Finish()
        {
            PointMove.gameObject.SetActive(false);
            currentItem = -1;
            TrainingComplitUI.Instance.SetTime(0);
            CanvasVRPlayer.Instance.FireBoxInfo.content.SetActive(false);
        }

        private void ActivateItem(int num)
        {
            currentItem = num;
            situations[currentItem].gameObject.SetActive(true);
            situations[currentItem].Init();
            if (situations[currentItem].pointMove != null)
            {
                PointMove.gameObject.SetActive(true);
                PointMove.transform.position = situations[currentItem].pointMove.position;
            }
            else
            {
                PointMove.gameObject.SetActive(false);
            }
            CanvasVRPlayer.Instance.FireBoxInfo.ActivateInfo(situations[currentItem].nameStep, currentItem);
        }
    }
}
