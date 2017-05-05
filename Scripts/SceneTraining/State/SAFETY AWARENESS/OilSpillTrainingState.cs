namespace Omni.Game.SceneTraining
{
    public class OilSpillTrainingState : TrainingState
    {

        // Use this for initialization
        public override void Start()
        {
            SituationName = GlobalTrainingString.oilSpill;    
            currentState = eTrainingState.OilSpill;
            SequencingItems = new eItems[] { eItems.FireAlert, eItems.Rod, eItems.MusterPoint };
            ManagerInteractItems.Instance.ActivateNextObject(SequencingItems[0]);

            InteractItem _item = ManagerInteractItems.Instance.GetItem(eItems.Rod);        
            _item.GetComponent<RodItem>().ActivateSituation();
        }       
    }
}
