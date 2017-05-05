using UnityEngine;
using UnityEngine.UI;
using Omni.Game.SceneTraining.State;
namespace Omni.Game.SceneTraining
{
    public class SituationPanelUI : MonoBehaviour
    {
        public TrainigChaptersUI _TrainigChaptersUI;
        public Button Next, Previous;
        public SituationItemUI StartButton;        
        public SituationItemUI[] ItemsState;       

        // Use this for initialization
        void Start()
        {           
            Next.onClick.AddListener(NextItem);
            Previous.onClick.AddListener(PreviousItem);
            StartButton.GetComponent<Button>().onClick.AddListener(OnStartButton);   
            StartButton.Deactivate();
            gameObject.SetActive(false);
        }

        void OnEnable()
        {
            CheckProgress();
            if (GlobalSceneState.Instance != null)
                GlobalSceneState.Instance.CurrentEmergency = eTrainingState.Normal;
            for (int i = 0; i < ItemsState.Length; i++)
            {
                ItemsState[i].Deactivate();
            }
        }

        public void ActivateItem(int _id)
        {
            GlobalSceneState.Instance.CurrentEmergency = (eTrainingState)_id;
            _id--;
            StartButton.Activate();
            int lenght = ItemsState.Length;
            for (int i=0;i< lenght; i++)
            {
                if(i != _id)
                {
                    ItemsState[i].Deactivate();
                }else
                {
                    ItemsState[i].Activate();
                }
            }
            Next.image.color = Color.white;
            Previous.image.color = Color.white;
        }

        void CheckProgress()
        {
            if (ProgressInfo.Instance == null)
            {
                
            }              
            else
            {
                int lenght = ItemsState.Length;
                bool[] _progress = ProgressInfo.Instance.Progress.TrainingSituations;
                if (_progress == null)
                    return;
                for (int i = 0; i < lenght; i++)
                {                   
                    if (i < _progress.Length &&  _progress[i])
                        ItemsState[i].ActivateCompleted();
                }
            }  
        }

        void NextItem()
        {
            if (GlobalSceneState.Instance.CurrentEmergency == eTrainingState.Normal)
            {
                ActivateItem(1);
            }
            else
            {
                int _current = (int)GlobalSceneState.Instance.CurrentEmergency;
                _current++;
                if (_current <= ItemsState.Length)
                {                  
                    ActivateItem(_current);
                }                  
            }
        }

        void PreviousItem()
        {           
            if(GlobalSceneState.Instance.CurrentEmergency == eTrainingState.Normal)
            {
                ActivateItem(1);
            }else
            {
                int _current = (int)GlobalSceneState.Instance.CurrentEmergency;
                _current--;
                if (_current > 0)
                    ActivateItem(_current);
            }
        }

        void OnStartButton()
        {
            _TrainigChaptersUI.NextAfterSutuation();
            gameObject.SetActive(false);
            /*
            if (currentState != eTrainingState.None)
            {
                GlobalSceneState.Instance.SetNewTraininigState(currentState,eHazardLevel.hard);
               
            } 
            */
        }
    }
}
