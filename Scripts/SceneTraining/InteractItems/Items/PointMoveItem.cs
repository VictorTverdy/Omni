using UnityEngine;
namespace Omni.Game.SceneTraining
{
    public class PointMoveItem : InteractItem
    {
        public Transform[] points;
        public GameObject WrongWay;
        public GameObject CanvasWrongExplanations;
        public GameObject WindMiniMap;
        private int currentStep = -1; 

        public override void Awake()
        {
            base.Awake();
            CanInteract = false;
            nameStep = GlobalTrainingString.moveToPoint;
            CurrentItem = eItems.EmergencyEvacuation;
            WrongWay.SetActive(false);
            CanvasWrongExplanations.SetActive(false);
            WindMiniMap.SetActive(false);
        }

        public override void Action()
        {
            if (!CanInteract)
                return;

            if (currentStep < points.Length - 1)
            {
                Invoke("ActivateCanInteract", 1);
                CanInteract = false;
                currentStep++;
                Vector3 pos = points[currentStep].position;
                SituationInfoUI.Instance.SetNextStep(nameStep, currentStep);
                ManagerInteractItems.Instance.ShowPointMove(pos, currentStep);
                CanvasText.SetActive(true);
                CanvasText.transform.position = pos;
                CanvasText.transform.LookAt(Camera.main.transform);
            }
            else
            {
                CanInteract = false;
                ManagerInteractItems.Instance.PointMove.gameObject.SetActive(false);
                CanvasText.SetActive(false);
                TrainingStateManager.Instance.SetEndSituation();
            }
        }

        public void ActivateWrongWayExplanations()
        {
            WrongWay.SetActive(false);
            CanvasWrongExplanations.SetActive(false);
        }

        public void ActivateSituaion()
        {
            CanInteract = true;
            currentStep = -1;
            WindMiniMap.SetActive(true);
            WrongWay.SetActive(true);
            Action();
        }

        private void ActivateCanInteract()
        {
            CanInteract = true;
        }
    }
}

