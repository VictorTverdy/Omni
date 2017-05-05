namespace Omni.Game.SceneTraining
{
    public class FireTrainingState : TrainingState
    {

        // Use this for initialization
        public override void Start()
        {
            SituationName = GlobalTrainingString.fire;           
            currentState = eTrainingState.Fire;
            SequencingItems = new eItems[] { eItems.FireAlert, eItems.Extinguisher, eItems.Fire,eItems.MusterPoint };
            ManagerInteractItems.Instance.ActivateNextObject(SequencingItems[0]);

            InteractItem _item = ManagerInteractItems.Instance.GetItem(eItems.Fire);
            FireItem _fire = _item.GetComponent<FireItem>();
            _fire.gameObject.SetActive(true);
        }
      
    }
}
