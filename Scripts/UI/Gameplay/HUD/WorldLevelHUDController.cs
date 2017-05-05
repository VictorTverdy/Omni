using UnityEngine;
using UnityEngine.UI;

using Omni.CameraCtrl;
using Omni.Events;
using Omni.Utilities.EventHandlers;
using Omni.GameState;

public class WorldLevelHUDController : MonoBehaviour
{   
    public Button closePanel;
    public Animator GeozoneAnimator;
    private CameraWorldInputControl cameraInputControl;

	[SerializeField] private GameObject goFilter = null;
	[SerializeField] private Animator animLegend = null;
	[SerializeField] private Animator animBtnHelp = null;    
	[SerializeField] private GameObject goWelcomePanel = null;
    
	private bool m_isInsideTheFiel;

    void OnEnable() {

        if(GameValues.lastScene != Omni.Utilities.EnumScenes.Intro) {
            OnBackToWorldLevel();
            GeozoneAnimator.Play("GeozoneUp");
        }else {
            //not working o_o, why???
            //goWelcomePanel.SetActive(true);
        }
    }

    void Start()
    {
		m_isInsideTheFiel = false;
        closePanel.onClick.AddListener(CloseWellInfoPopup);      
        cameraInputControl = FindObjectOfType<CameraWorldInputControl>();
        EventManager.instance.addEventListener(WorldLevelEvent.ON_CAMERA_TO_POINT, this.gameObject, "OnCameraToPoint");
        EventManager.instance.addEventListener(WorldLevelEvent.ON_BACK_TO_WORLD, this.gameObject, "OnBackToWorldLevel");
        goFilter.SetActive(true);
    }  

    void OnBackToWorldLevel() {
        goFilter.SetActive(false);
        animBtnHelp.gameObject.SetActive(false);
        animLegend.gameObject.SetActive(false);
        goWelcomePanel.gameObject.SetActive(false);
    }   

    private void CloseWellInfoPopup()
    {
        //camera zoom out
		if (m_isInsideTheFiel) {
			goWelcomePanel.gameObject.SetActive(false);
			if (cameraInputControl.GetCanInteract ()) {
				m_isInsideTheFiel = false;
				GeozoneAnimator.Play ("GeozoneDown");
                cameraInputControl.SetStateToZoomingOutToSpace();
                animBtnHelp.gameObject.SetActive(true);
                animLegend.gameObject.SetActive(true);
			}
		} else {
        //back to mainMenu
			GameStateManager.Instance.SwapToState(new GameInitState());
		}
    }
            
    //camera zoom in
    private void OnCameraToPoint()
    {
		m_isInsideTheFiel = true;
        GeozoneAnimator.Play("GeozoneUp");
        
        goFilter.SetActive(false);
        animBtnHelp.Play("an_HelpButton_out");
        animLegend.Play("an_Tittle_out");
    }
}
