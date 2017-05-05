using Omni.GameState;
using UnityEngine;
using UnityEngine.UI;
namespace Omni.Game.SceneTraining
{
    public class MenuUI : MonoBehaviour
    {
        public Button ExitWelcom, ExitHealht;
        public GameObject PanelWelcom;

        // Use this for initialization
        void Start()
        {           
            ExitWelcom.onClick.AddListener(OnExit);
            ExitHealht.onClick.AddListener(OnExit);
        }    

        public void IsTrainingMode(bool value)
        {
            TrainingStateManager.Instance.IsTrainig = value;
        }
        
        void OnEnable()
        {
            PanelWelcom.SetActive(true);
        }

        private void OnExit()
        {
            if (GameStateManager.Instance != null)
				GameStateManager.Instance.SwapToState(new GameInitState());
        }      
       
    }
}
