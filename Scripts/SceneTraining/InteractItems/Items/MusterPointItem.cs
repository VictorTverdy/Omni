using UnityEngine;
namespace Omni.Game.SceneTraining
{
    public class MusterPointItem : InteractItem
    {

        // Use this for initialization
        public override void Awake()
        {
            base.Awake();
            CurrentItem = eItems.MusterPoint;
            nameStep = GlobalTrainingString.musterPoint;
        }

        public override void Action()
        {
            if (TrainingStateManager.Instance.GetCurrentState() != eTrainingState.Evacuation)
            {
                gameObject.SetActive(false);
                TrainingStateManager.Instance.SetEndSituation();
            }
            
        }
    }
}
