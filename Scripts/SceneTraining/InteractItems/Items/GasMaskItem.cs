using UnityEngine;
namespace Omni.Game.SceneTraining
{
    public class GasMaskItem : InteractItem
    {
        public GameObject MaskModel;
       
        // Use this for initialization
        public override void Awake()
        {
            base.Awake();
            nameStep = GlobalTrainingString.takeMask;
            CurrentItem = eItems.GasMask;
        }

        void OnEnable()
        {
            MaskModel.SetActive(true);
        }

        public override void Action()
        { 
            MaskModel.SetActive(false);
            ManagerInteractItems.Instance.DeactivateWorkers();
            InteractItem _item = ManagerInteractItems.Instance.GetItem(eItems.EmergencyEvacuation);
            PointMoveItem _pointMove = _item.GetComponent<PointMoveItem>();
            _pointMove.ActivateSituaion();
            VRMovement vrMovement = FindObjectOfType<VRMovement>(); 
            vrMovement.ActivateGasMask();
            base.Action();
        }
       
    }
}
