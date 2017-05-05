using UnityEngine;
namespace Omni.Game.SceneTraining.Box
{
    public class MoveSaveLOcation : Situation, IToDo
    {
        public Animator ForkLift;

        void OnEnable()
        {
            nameStep = GlobalTrainingString.bunkerSafeArea;
            situation = eSituations.moveLocation;          
        }

        public override void Init()
        {
            base.Init();
            ForkLift.gameObject.SetActive(false);
            CanvasVRPlayer.Instance.SetActivateFade();
            Invoke("DeactivateCar", 2);
            Invoke("ActivateForkLift",5); 
        }

        public void Action()
        {
        }

        private void DeactivateCar()
        {
            ForkLift.gameObject.SetActive(true);
            sequenceActions.GetItem(eSituations.arrivedBox).gameObject.SetActive(false);
            sequenceActions.GetItem(eSituations.checkPaper).gameObject.SetActive(false);
        }

        private void ActivateForkLift()
        {
           
            ForkLift.Play("ForkLift");
        }
    }
}
