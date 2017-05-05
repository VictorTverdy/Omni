using UnityEngine;
namespace Omni.Game.SceneTraining.Box
{
    public class BoxPlaced : Situation, IToDo
    {
        public GameObject Bunker;
        public GameObject SavePlace;
        public Animator forkLift;

        void OnEnable()
        {
            nameStep = GlobalTrainingString.positionBunker;
            situation = eSituations.placedBox;
        }

        public void Action()
        {
            SavePlace.SetActive(false);
            forkLift.Play("GroundBunker");
            Invoke("GroundBunker",6);
        }

        private void GroundBunker()
        {            
             Bunker.transform.SetParent(transform);
            sequenceActions.ActivateNextItem();
        }
    }
}
