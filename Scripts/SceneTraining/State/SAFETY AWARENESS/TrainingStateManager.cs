using UnityEngine;
using System.Collections;
using Omni.UI.ScreensControllers;

namespace Omni.Game.SceneTraining
{
    public class TrainingStateManager : MonoBehaviour
    {
        public static TrainingStateManager Instance;

        public bool IsTrainig;
        private TrainingState currentState;
        private eHazardLevel currentLevel;
        private int minInvoke = 15;
        private int maxInvoke = 50;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else 
                DestroyImmediate(this);           
        }             

        public eItems GetNextItem(eItems currentItem)
        {          
            int nextNum =0;
            int lenght = currentState.SequencingItems.Length;
            for (int i= 0;i< lenght; i++)
            {
                if (i == lenght - 1)  
                {                  
                    return eItems.None;
                }
                if (currentState.SequencingItems[i] == currentItem )
                {                  
                    nextNum = i + 1;
                    break;
                }
            }           
            return currentState.SequencingItems[nextNum];
        }      

        public void SetEndSituation()
        { 
            ProgressInfo.Instance.SetEndTrainingSituation(currentState.currentState);            
            SituationInfoUI.Instance.SetEndSituation();
        }      

        private void RestartState()
        {           
            ManagerInteractItems.Instance.DeactivateAllItems();
            HazardItemsManager.Instance.DeactivateAllItems();
        }

        public void SetNewState(eTrainingState _state, eHazardLevel _level)
        {
            currentLevel = _level;
			UIController.Instance.FadeEffectController.ScreenTransition(0);
            RestartState();
            HazardItemsManager.Instance.ActivateAllItems(currentLevel);
            StopCoroutine("CreateNewState");
            switch (_state) 
            {
                case eTrainingState.Normal:
                    StartCoroutine("CreateNewState", new NormalTrainingState());
                    break;
                case eTrainingState.Fire:
                    StartCoroutine("CreateNewState", new FireTrainingState());
                    break;
                case eTrainingState.OilSpill:
                    StartCoroutine("CreateNewState", new OilSpillTrainingState());
                    break;
                case eTrainingState.Evacuation:
                    StartCoroutine("CreateNewState", new EvacuateTrainingState());
                    break;
            }
        }

        public void SetNewState(eTrainingState _state)
        {
            RestartState();
            StopCoroutine("CreateNewState");            
            switch (_state)
            {
                case eTrainingState.Normal:
                    StartCoroutine("CreateNewState", new NormalTrainingState());
                    break;
                case eTrainingState.Fire:
                    StartCoroutine("CreateNewState", new FireTrainingState());
                    break;
                case eTrainingState.OilSpill:
                    StartCoroutine("CreateNewState", new OilSpillTrainingState());
                    break;
                case eTrainingState.Evacuation:
                    StartCoroutine("CreateNewState", new EvacuateTrainingState());
                    break;
            }
        }

        private IEnumerator CreateNewState(TrainingState _state)
        {           
            SituationInfoUI.Instance.Content.SetActive(false);           
            currentState = _state;
            int _random = Random.Range(minInvoke, maxInvoke);
            yield return new WaitForSeconds(_random);           
            currentState.Start();
            SituationInfoUI.Instance.SetSituationName(currentState.SituationName, currentState.currentState, IsTrainig);                   
        } 

        public eItems[] GetCurrentStateItems()
        {
            return currentState.SequencingItems;
        }

        public int GetNumItemsInCurrentState(eItems _item)
        {
            eItems[] items = GetCurrentStateItems();
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == _item)
                {
                    return  i;
                }
            }
            return -1;
        }

        public eTrainingState GetCurrentState()
        {   
            return currentState.currentState;
        }

        public eHazardLevel GetCurrentLevel()
        {
            return currentLevel;
        }    
    }
}
