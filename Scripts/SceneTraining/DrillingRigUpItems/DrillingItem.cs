using UnityEngine;
using System.Collections;
namespace Omni.Game.SceneTraining
{
    public class DrillingItem : MonoBehaviour, IToDo
    {
        public eDrillingSequence sequence;
        private Transform startPoint;
        private Transform pointMove;
        private Transform interactObject;
        private bool canInteract;

        // Use this for initialization
        void Awake()
        {
            startPoint = transform.Find("StartPoint");
            interactObject = transform.Find("InteractObject");
            pointMove = transform.Find("PointMove");
        }     

        public void StartPosition()
        {
            StopCoroutine("CorMove");
            canInteract = true;
            interactObject.SetParent(startPoint);
            interactObject.localPosition = Vector3.zero;
            interactObject.localEulerAngles = Vector3.zero;
        }

        public void SetParentFinish(Transform _finish)
        {
            StopCoroutine("CorMove");
            interactObject.SetParent(_finish);
        }
         
        public void MoveEndPoint()
        {           
            canInteract = false;
            interactObject.SetParent(pointMove);
            StartCoroutine("CorMove");          
        }

        private IEnumerator CorMove()
        {
            float speedMove = 0.3f;
            for (;;)
            {
                interactObject.localPosition = Vector3.MoveTowards(interactObject.localPosition,Vector3.zero, speedMove);
                if (interactObject.localPosition == Vector3.zero)
                {
                    interactObject.localEulerAngles = Vector3.zero;
                    break;
                }                  
                yield return new WaitForSeconds(0.01f);
            }          
        }

        public void Action()
        {
            if (canInteract)
            {
                ManagerDrillingItems.Instance.ActivateSequence(sequence);
            }
        }
    }
}
