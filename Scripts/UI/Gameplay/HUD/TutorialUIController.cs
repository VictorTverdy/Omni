using Omni.Events;
using Omni.Game.SceneTraining;
using Omni.Utilities.EventHandlers;
using UnityEngine;
namespace Omni.UI.Gameplay.HUD
{
    public class TutorialUIController : MonoBehaviour
    {
        public OptionUI OptionPanel;
        public SituationInfoUI SituationPanel;
        public MenuUI  MenuPanel;
        public VRMovement VRMovementPlayer;

        private Transform vRMovement;
        private float offset = 1.7f;
        void OnEnable()
        {
            CloseMenu();
        }
        // Use this for initialization
        private void Start()
        { 
            vRMovement = Camera.main.transform;          
            MoveToPlayer();           
            EventManager.instance.addEventListener(TrainingEvent.open_menu, this.gameObject, "OpenMenu");
            EventManager.instance.addEventListener(TrainingEvent.close_menu, this.gameObject, "CloseMenu");
            EventManager.instance.addEventListener(TrainingEvent.auto_menu, this.gameObject, "AutoMenu");
            EventManager.instance.addEventListener(TrainingEvent.restart, this.gameObject, "Restart");
        } 
        
        public void MoveToPlayer()
        {
            if (vRMovement != null)
            {
                VRMovementPlayer.SetCanMove(false);
                transform.eulerAngles = vRMovement.eulerAngles;
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                transform.position = vRMovement.transform.position + (transform.forward * 0.8f); 
                transform.localPosition = new Vector3(transform.localPosition.x, VRMovementPlayer.transform.position.y + offset, transform.localPosition.z);
            } 
        }

        private void Restart()
        {
            OptionPanel.OpenOption(false);
            MenuPanel.PanelWelcom.SetActive(true);
            MoveToPlayer();
        }

        private void AutoMenu()
        {          
            if (OptionPanel.OptionPanel.activeInHierarchy)
            {
                MoveToPlayer();
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }          
        }

        private void OpenMenu()
        {
            VRMovementPlayer.SetCanMove(false);
            MoveToPlayer();
            OptionPanel.OpenOption(true);
        }

        private void CloseMenu()
        {
            if (SituationPanel == null)
                SituationPanel = FindObjectOfType<SituationInfoUI>();
            if(VRMovementPlayer == null)
                VRMovementPlayer = FindObjectOfType<VRMovement>();
            VRMovementPlayer.SetCanMove(true);
            SituationPanel.UnPause();
            OptionPanel.OpenOption(false);
        }
    }
}
