namespace Omni.Game.SceneTraining.Box
{
    public class BoxArrives : Situation, IToDo
    {
        // Use this for initialization
        void OnEnable()
        {
            nameStep = GlobalTrainingString.bunkerArrived;
            situation = eSituations.arrivedBox;
        }

        public void Action()
        {
           
        }
    }
}
