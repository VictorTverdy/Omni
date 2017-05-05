using UnityEngine;
namespace Omni.Game.SceneTraining.Box
{
    public class CheckPaper : Situation, IToDo
    {
        public GameObject completed, document;     

        // Use this for initialization
        void OnEnable()
        {
            nameStep = GlobalTrainingString.checkDocumentation;
            completed.SetActive(false);
            situation = eSituations.checkPaper;
        }

        public void Action()
        {
            completed.SetActive(true);
            sequenceActions.ActivateNextItem();
        }

    }
}
