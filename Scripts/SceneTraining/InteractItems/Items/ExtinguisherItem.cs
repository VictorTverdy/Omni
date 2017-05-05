using UnityEngine;

namespace Omni.Game.SceneTraining
{
    public class ExtinguisherItem : InteractItem
    {
        public ParticleSystem ExtinguisherParticle;
        public GameObject modelExtinguisher;
        public GameObject WarningIconFire;

        public override void Awake()
        {
            base.Awake();
            nameStep = GlobalTrainingString.takeExtinguisher;
            ExtinguisherParticle = FindObjectOfType<VRMovement>().Extinguisher.GetComponentInChildren<ParticleSystem>();
            CurrentItem = eItems.Extinguisher;
        }    

        void OnEnable()
        {
            modelExtinguisher.SetActive(true);
        }

        public void PlayExtinguisherParticle()
        {
            ExtinguisherParticle.Play();
        }

        public override void Action()
        {          
            if (CanInteract)
            {
                modelExtinguisher.SetActive(false);
                WarningIconFire.SetActive(false);
                VRMovement vrMovement = FindObjectOfType<VRMovement>();              
                vrMovement.SetParentCameraExtinguisher();
                base.Action();
            }
        }


    }
}

