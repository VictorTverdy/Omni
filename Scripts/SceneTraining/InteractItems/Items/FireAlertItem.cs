using UnityEngine;
namespace Omni.Game.SceneTraining
{
    public class FireAlertItem : InteractItem
    {
        public Animator myAnimator;
        public GameObject LightAlert;


        private void OnEnable()
        {
            LightAlert.SetActive(false);
        }

        public override void Awake()
        {
            base.Awake();
            nameStep = GlobalTrainingString.emergencyButton; 
            CurrentItem = eItems.FireAlert;          
        }

        public override void Action()
        {          
            myAnimator.Play("FireAlert");          
            if (CanInteract)
            {
                LightAlert.SetActive(true);
                base.Action();               
            }             
        }    
    }
}
