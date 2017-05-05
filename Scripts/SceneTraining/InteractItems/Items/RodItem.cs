using UnityEngine;
namespace Omni.Game.SceneTraining
{
    public class RodItem : InteractItem
    {
        public Animator rotateRood;
        private ParticleSystem ParticleOil;
        private GameObject OilSpill;
        private GameObject spriteAlert;
        private GameObject mapIdent;
        private float emmisionOilSpill = 100;
        private bool canHandRotate;
        private Vector3 oldPosHand;

        public override void Awake()
        {
            base.Awake();
            nameStep = GlobalTrainingString.closeValve;
            CurrentItem = eItems.Rod;        
            OilSpill = transform.Find("OilSpill").gameObject;
            spriteAlert = OilSpill.transform.Find("SpriteAlert").gameObject;
            mapIdent = OilSpill.transform.Find("MapIdentificator").gameObject;
            OilSpill.SetActive(false);
            ParticleOil = transform.GetComponentInChildren<ParticleSystem>();
            ParticleOil.gameObject.SetActive(false);
         //   rightHand = FindObjectOfType<VRMovement>().transform.Find("Controller (right)");
        }      

        private void OnEnable()
        {
            canHandRotate = false;  
       //     rotateRood.Play("RoodStart");
            if (CanvasText!= null)
            CanvasText.SetActive(true);
            if(spriteAlert != null) 
            spriteAlert.SetActive(true);
            if(mapIdent != null)
            mapIdent.SetActive(true);
            if(ParticleOil != null)
            {
                var emission = ParticleOil.emission;
                var rate = emission.rateOverTime;
                rate.constant = emmisionOilSpill;
                emission.rateOverTime = rate;
            }           
        }

        private void Update()
        {
        //    RotateHand();
        }

        public override void Action()
        {
           var _fireAlert = ManagerInteractItems.Instance.GetItem(eItems.FireAlert).GetComponent<InteractItem>();
            if (!_fireAlert.GetCanInteract())
            {
                RotateRoodCamera();
                rotateRood.transform.localEulerAngles = new Vector3(0, 0, -90);
            }             
         //   canHandRotate = true;          
        }

        public void ActivateSituation()
        {
            OilSpill.gameObject.SetActive(true);
            ParticleOil.gameObject.SetActive(true); 
        }     

        public void DeactivateParticleOil()
        {
            var emission = ParticleOil.emission;
            var rate = emission.rateOverTime;
            rate.constant = 0;
            emission.rateOverTime = rate;
            DeactivateSituation();
        }

        private void RotateHand()
        {
            if(canHandRotate && CanInteract)
            {
               
             //   if (oldPosHand.y > rightHand.transform.localPosition.y)                
                    rotateRood.transform.Rotate(0, -10, 0);                              
            //    oldPosHand = rightHand.transform.localPosition;
                if (rotateRood.transform.localEulerAngles.x >= 90)
                {
                    rotateRood.Play("RodRotate");
                    DeactivateParticleOil();
                }
            }
        }

        private void RotateRoodCamera()
        {
            if (CanInteract)
            {
                CanInteract = false;
                rotateRood.Play("RodRotate");
                Invoke("DeactivateParticleOil", 2);
            }
        }
         
        private void DeactivateSituation()
        {
            canHandRotate = false; 
            CanInteract = false;
            base.Action();
            spriteAlert.SetActive(false);
            mapIdent.SetActive(false);
        }
    }
}