
namespace Omni.Game.SceneTraining
{
    public class EndEmergencyState : TrainingState
    {

        // Use this for initialization
        public override void Start()
        {
            SituationName = "end situation";
          //  currentState = eTrainingState.EndSituation;
            ManagerInteractItems.Instance.DeactivateAllItems();          
        }
        
    }
}
