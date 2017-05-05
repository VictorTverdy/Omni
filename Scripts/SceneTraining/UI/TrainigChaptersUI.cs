using Omni.Game.SceneTraining.State;
using UnityEngine;
using UnityEngine.UI;
namespace Omni.Game.SceneTraining
{
    public enum eSelectedChapters
    {
        SafetyAwareness,
        Drilling,
        ValveOperation,
        ExplosivesBunkerSafety
    }

public class TrainigChaptersUI : MonoBehaviour
    {

        public ComplexityLevel LevelPanel;
        public GameObject WelcomePanel,Option,SafetyPanel, DrillingPanel, ValveOperationPanel;
        public Button Next,Back;
        public Button NextChapter, PreviousChapter;
        public ChaptersItemUI[] ChaptersItem;

        private bool isGame;     
        private eSelectedChapters currentChapters;

        // Use this for initialization
        void Start()
        {
            Back.onClick.AddListener(OnBack);
            Next.onClick.AddListener(OnNext);
            NextChapter.onClick.AddListener(OnNextChapter);
            PreviousChapter.onClick.AddListener(OnPreviousChapter);  
            gameObject.SetActive(false);
        }

        void OnEnable()
        {
            CheckProgress();
            ActivateChapter(eSelectedChapters.SafetyAwareness);
        }

        public void OpenInMenu()
        {           
            isGame = false;
        }

        public void OpenInGame()
        {
            isGame = true;
        }

        public void ActivateChapter(eSelectedChapters chapter)
        {
            currentChapters = chapter;
            int lenght = ChaptersItem.Length; 
            for (int i = 0; i < lenght; i++)
            {
                ChaptersItem[i].ActivateOtline(false);
            }
            ChaptersItem[(int)chapter].ActivateOtline(true);
        }

        public void NextAfterSutuation()
        {
            switch (currentChapters)
            {
                case eSelectedChapters.SafetyAwareness:
                    LevelPanel.gameObject.SetActive(true);
                    break;
                case eSelectedChapters.Drilling:
                    DrillingPanel.SetActive(true);
                    break;
                case eSelectedChapters.ValveOperation:
                    ValveOperationPanel.SetActive(true);
                    break;
                case eSelectedChapters.ExplosivesBunkerSafety:
                    GlobalSceneState.Instance.SetNewExplosivesBunkerSafety();
                    break;
            }
        }

        void CheckProgress()
        {
            if (ProgressInfo.Instance == null)
            {
               
            }
            else
            {
                int lenght = ChaptersItem.Length;
                bool[] _progress = ProgressInfo.Instance.Progress.Chapters;
                for (int i = 0; i < lenght; i++)
                {
                    if (_progress[i]) 
                        ChaptersItem[i].ActivateCompleted();
                }
            }
        }

        void OnNextChapter()
        {
            int _current = (int)currentChapters;
            _current++; 
            if (_current <= ChaptersItem.Length -1)
            {
                ActivateChapter((eSelectedChapters)_current);
            }
        }

        void OnPreviousChapter()
        {
            int _current = (int)currentChapters;
            _current--;
            if (_current >= 0)
            {
                ActivateChapter((eSelectedChapters)_current);
            }              
        }

        void OnBack()
        {
            if (!isGame)
            { 
                WelcomePanel.SetActive(true);
            }else
            {
                Option.SetActive(true);           
            }
            gameObject.SetActive(false);
        }

        void OnNext()
        {             
            if(currentChapters != eSelectedChapters.ExplosivesBunkerSafety)
            {
                SafetyPanel.SetActive(true);
            }
            else
            {
                GlobalSceneState.Instance.SetNewExplosivesBunkerSafety();
            }            
            gameObject.SetActive(false);
        }        
    }
}
