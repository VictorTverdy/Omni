namespace Omni.Game.SceneTraining
{
    public class EvacuateTrainingState : TrainingState
    {

        // Use this for initialization
        public override void Start()
        {
            SituationName = GlobalTrainingString.evacuation; 
            currentState = eTrainingState.Evacuation;
            SequencingItems = new eItems[] { eItems.GasMask, eItems.EmergencyEvacuation};
            ManagerInteractItems.Instance.ActivateNextObject(SequencingItems[0]);

            InteractItem _item = ManagerInteractItems.Instance.GetItem(eItems.FireAlert);
            FireAlertItem _alert = _item.GetComponent<FireAlertItem>();
            _alert.LightAlert.SetActive(true);   
            
                    
        }      
    }
}

