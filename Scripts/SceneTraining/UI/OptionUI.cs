using Omni.GameState;
using UnityEngine;
using UnityEngine.UI;
using Omni.Game.SceneTraining.State;
using Omni.Events;
using Omni.Utilities.EventHandlers;

namespace Omni.Game.SceneTraining
{
    public class OptionUI : MonoBehaviour
    {
        public GameObject OptionPanel, HelpPanel, TestModePanel;
        public Button Restart, Exit,Pause,Help,Chapters,TestMode, TutorialMode;
        public TrainigChaptersUI ChaptersPanel;
        public Text TextLabbel;
        public bool isTestMode;

        // Use this for initialization
        void Start()
        {
            Exit.onClick.AddListener(OnExit);
            Restart.onClick.AddListener(OnRestart);
            Pause.onClick.AddListener(OnPause);
            Chapters.onClick.AddListener(OnChapters);
            TestMode.onClick.AddListener(OnTestMode);
            TutorialMode.onClick.AddListener(OnTutorialMode);
            Help.onClick.AddListener(OnHelp);
        }

        public void OpenOption(bool _enable) 
        {
            OptionPanel.SetActive(_enable);
            if(_enable == true)
            {  
                if (isTestMode)
                {
                    TutorialMode.gameObject.SetActive(true);
                    TestMode.gameObject.SetActive(false);
                    TextLabbel.text = "TEST MODE";                   
                } 
                else
                {
                    TestMode.gameObject.SetActive(true);
                    TutorialMode.gameObject.SetActive(false);
                    TextLabbel.text = "TUTORIAL MODE";                    
                }
            }
        }

        public void SetTestMode(bool isTest)
        {
            isTestMode = isTest;
        }

        private void OnChapters()
        {
            ChaptersPanel.gameObject.SetActive(true);
            ChaptersPanel.OpenInGame();
            OptionPanel.SetActive(false);
        }

        private void OnTestMode()
        { 
          //  OptionPanel.SetActive(false);
         //   TestModePanel.SetActive(true);
        }

        private void OnTutorialMode()
        {
            //  OptionPanel.SetActive(false);
            //   TestModePanel.SetActive(true);
        }

        private void OnHelp()
        {
            OptionPanel.SetActive(false);
            HelpPanel.SetActive(true);
        }

        private void OnPause()
        {
            SituationInfoUI.Instance.Pause(); 
        }
         
        private void OnExit()
        {
            if (GameStateManager.Instance != null)
                GameStateManager.Instance.SwapToState(new GameInitState());
        }

        private void OnRestart()
        {           
            OptionPanel.SetActive(false);
            GlobalSceneState.Instance.DeactivateCurrentChapter();
            TrainingEvent evt = new TrainingEvent(TrainingEvent.restart);
            EventManager.instance.dispatchEvent(evt);
        }
    }
}
