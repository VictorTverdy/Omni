using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Omni.CameraCtrl;
using Omni.Events;
using Omni.Utilities.EventHandlers;
using Omni.GameState;

namespace Omni.UI.Gameplay.HUD
{
	public class DrillToolHUDController : MonoBehaviour 
	{
		public CanvasRenderer BackPanel;
		[SerializeField]private GameObject goButtons;
		[SerializeField]private GameObject imgPanel;
		[SerializeField]private GameObject imgMachine0;
		[SerializeField]private GameObject imgMachine1;
		[SerializeField]private GameObject imgMachine2;

        public void Awake()
		{
			EventManager.instance.addEventListener (AssetEvent.ON_SELECTED_ASSET, this.gameObject, "OnShowBackButton");

			BackPanel.gameObject.SetActive (false);
            goButtons.gameObject.SetActive (false);
            imgPanel.gameObject.SetActive (false);
        }

		public void BackToDrillTower()
		{
			GameStateManager.Instance.PopState ();
		}

		public void BackButtonClick()
		{
			BackPanel.gameObject.SetActive (false);
            goButtons.SetActive(false);

            AssetEvent evt = new AssetEvent (AssetEvent.ON_BACK_TO_ALL_VIEW);
			EventManager.instance.dispatchEvent (evt);
		}

		private void OnShowBackButton()
		{
			BackPanel.gameObject.SetActive (true);
            goButtons.SetActive(true);
        }

        public void ListenerOpenImg()
        {
            imgMachine0.SetActive(false);
            imgMachine1.SetActive(false);
            imgMachine2.SetActive(false);
            switch (DrillingAssamblyDisplay.Instance.currentMachineSelected)
            {
                case Utilities.EnumDrillingMachinePieces.None:
                    break;
                case Utilities.EnumDrillingMachinePieces.DrillingJar:
                    imgMachine0.SetActive(true);
                    break;
                case Utilities.EnumDrillingMachinePieces.MudMotor:
                    imgMachine1.SetActive(true);
                    break;
                case Utilities.EnumDrillingMachinePieces.DrillingHead:
                    imgMachine2.SetActive(true);
                    break;
                default:
                    break;
            }
            imgPanel.SetActive(true);

        }

        public void ListenerOpenUrl()
        {
            switch (DrillingAssamblyDisplay.Instance.currentMachineSelected)
            {
                case Utilities.EnumDrillingMachinePieces.None:
                    break;
                case Utilities.EnumDrillingMachinePieces.DrillingJar:
                    Application.OpenURL("http://www.odfjellwellservices.com/globalassets/rental-equipment/jars/6-griffith-double-acting-hydraulic-mechanical-drilling-jar---series-431-428-440-441-480-411-437---operating-manual.pdf");
                    break;
                case Utilities.EnumDrillingMachinePieces.MudMotor:
                    Application.OpenURL("http://www.cougards.com/wp-content/uploads/2014/03/Motor-Operations-Handbook-2012.pdf");
                    break;
                case Utilities.EnumDrillingMachinePieces.DrillingHead:
                    Application.OpenURL("http://www.infinitytoolmfg.com/pdc-bits-digital-brochure/");
                    break;
                default:
                    break;
            }
        }
        public void ListenerBtnClose()
        {
            imgPanel.SetActive(false);
            Debug.Log("***" + "closing");

        }
    }
}
