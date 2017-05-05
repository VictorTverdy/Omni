using UnityEngine;

namespace Omni.Game.SceneTraining
{
    public class PointMoveSafety : MonoBehaviour, IToDo
    {
        public void Action()
        {
            if (TrainingStateManager.Instance != null && TrainingStateManager.Instance.GetCurrentState() == eTrainingState.Evacuation)
            {
                if (name == "WrongWay")
                {
                    ManagerInteractItems.Instance.GetItem(eItems.EmergencyEvacuation).GetComponent<PointMoveItem>().ActivateWrongWayExplanations();
                }
                else
                {
                    var _gasMask = ManagerInteractItems.Instance.GetItem(eItems.GasMask).GetComponent<InteractItem>();
                    if (!_gasMask.GetCanInteract())
                    {
                       ManagerInteractItems.Instance.GetItem(eItems.EmergencyEvacuation).GetComponent<PointMoveItem>().Action();
                    }                        
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        } 
    }
}
