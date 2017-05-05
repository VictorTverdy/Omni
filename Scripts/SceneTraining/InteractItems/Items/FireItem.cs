using UnityEngine;
namespace Omni.Game.SceneTraining
{
    public class FireItem : InteractItem
    {
        private TrainingFire Fires;
        private ExtinguisherItem _Extinguishe;
        private bool endSituation;    
        private GameObject spriteAlert;     
        private GameObject mapIdent;

        public override void Awake()
        {
            base.Awake();
            nameStep = GlobalTrainingString.emergencyButton;
            CurrentItem = eItems.Fire;
            Fires = transform.GetComponentInChildren<TrainingFire>();
            spriteAlert = Fires.transform.Find("SpriteAlert").gameObject;
            mapIdent = Fires.transform.Find("MapIdentificator").gameObject;             
        }

        private void OnEnable()
        {
            endSituation = false;
            if (spriteAlert != null)
                spriteAlert.SetActive(true);
            if (mapIdent != null)
                mapIdent.SetActive(true);
        }

        public override void Action()
        {
            if (!endSituation)
            {
                if (_Extinguishe == null)
                    _Extinguishe = ManagerInteractItems.Instance.GetItem(eItems.Extinguisher).GetComponent<ExtinguisherItem>();
                if (!_Extinguishe.GetCanInteract())
                {
                    _Extinguishe.PlayExtinguisherParticle();
                    if (!Fires.deactivate)
                    {
                        Fires.DeactivationFire();
                        base.Action();
                        spriteAlert.SetActive(false);
                        mapIdent.SetActive(false);
                        endSituation = true;
                    }
                }
            }
        }      
    }
}
