using Omni.Events;
using Omni.Utilities.EventHandlers;
using UnityEngine;

namespace Omni.Game.SceneTraining.State
{
    public class GlobalSceneState : MonoBehaviour
    {
        public static GlobalSceneState Instance;
        
        public  eTrainingState CurrentEmergency;

        private GameObject trainingChapter;
        private GameObject drillingChapter;
        private GameObject valveChapter;
        private GameObject ExplosivesBunkerSafety;
        private GameObject EmergencySituation;
        private VRMovement vRMovement;
        FireBoxInfo fireBoxInfo;
        SituationInfoUI situationInfoUI;
        HazardInfo hazardInfo;

        private void Awake()
        {
            if (Instance != null)
                DestroyImmediate(this);
            else
                Instance = this;

            Init();
        }

        private void Init()
        {
            Transform _independent = transform.Find("Independent");
            if (!_independent.gameObject.activeInHierarchy)
            {
                GameObject _player = Instantiate(Resources.Load<GameObject>("Prefabs/GamePlay/TrainingItems/Player"));
                SetNormalPosition(_player);
                GameObject _hud = Instantiate(Resources.Load<GameObject>("Prefabs/GamePlay/TrainingItems/TutorialHUD"));
                SetNormalPosition(_hud);

                GameObject _clone = Instantiate(Resources.Load<GameObject>("Prefabs/GamePlay/TrainingItems/SAFETY_AWARENESS"));
                SetNormalPosition(_clone);
                trainingChapter = _clone;
                _clone = Instantiate(Resources.Load<GameObject>("Prefabs/GamePlay/TrainingItems/ValveOperations"));
                SetNormalPosition(_clone);
                valveChapter = _clone;
                _clone = Instantiate(Resources.Load<GameObject>("Prefabs/GamePlay/TrainingItems/DRILLING_RIG_UP"));
                SetNormalPosition(_clone);
                drillingChapter = _clone;
                _clone = Instantiate(Resources.Load<GameObject>("Prefabs/GamePlay/TrainingItems/EmergencySituation"));
                SetNormalPosition(_clone);
                EmergencySituation = _clone;
            }else
            {
                trainingChapter = _independent.Find("SAFETY_AWARENESS").gameObject;
                valveChapter = _independent.Find("ValveOperations").gameObject;
                drillingChapter = _independent.Find("DRILLING_RIG_UP").gameObject;
                EmergencySituation = _independent.Find("EmergencySituation").gameObject;
            }
            fireBoxInfo = FindObjectOfType<FireBoxInfo>();
            situationInfoUI = FindObjectOfType<SituationInfoUI>();
            hazardInfo = FindObjectOfType<HazardInfo>();
            vRMovement = FindObjectOfType<VRMovement>();
            DeactivateCurrentChapter();
        }

        public void SetNewTraininigState(eHazardLevel _level)
        {
            DeactivateCurrentChapter();
            trainingChapter.SetActive(true);
            EmergencySituation.SetActive(true);
            TrainingStateManager.Instance.SetNewState(CurrentEmergency, _level);
        }

        public void ActivateEmergency()
        {
            EmergencySituation.SetActive(true);
            TrainingStateManager.Instance.SetNewState(CurrentEmergency);
        }

        public void SetNewExplosivesBunkerSafety()
        {           
             DeactivateCurrentChapter();
            if (ExplosivesBunkerSafety != null) 
                Destroy(ExplosivesBunkerSafety);
            GameObject _clone = Instantiate(Resources.Load<GameObject>("Prefabs/GamePlay/TrainingItems/ExplosivesBunkerSafety"));
            SetNormalPosition(_clone);
            ExplosivesBunkerSafety = _clone; 
        }

        public void SetNewValveState()
        {
            DeactivateCurrentChapter();
            valveChapter.SetActive(true);
            ActivateEmergency();
        }

        public void SetNewDrillingState()
        {
            DeactivateCurrentChapter();
            drillingChapter.SetActive(true);
            ActivateEmergency();
        }         

        public void NextScene()
        {
            DeactivateCurrentChapter();
            TrainingEvent evt = new TrainingEvent(TrainingEvent.restart);
            EventManager.instance.dispatchEvent(evt);
        }

        public void RepeateScene()
        {
            if (trainingChapter.activeInHierarchy)
            {
                eHazardLevel _currentSLevel = TrainingStateManager.Instance.GetCurrentLevel();
               SetNewTraininigState(_currentSLevel);
            }
            else if (ExplosivesBunkerSafety.activeInHierarchy)
            {
               SetNewExplosivesBunkerSafety();
            }
            else
            {
                NextScene();               
            }
        }

        public void DeactivateCurrentChapter()
        {
            EmergencySituation.SetActive(false);
            fireBoxInfo.content.SetActive(false);
            situationInfoUI.Content.SetActive(false);
            hazardInfo.Content.SetActive(false);            
            vRMovement.MoveToStartPos();
            if (ExplosivesBunkerSafety != null)
                ExplosivesBunkerSafety.SetActive(false);
            trainingChapter.SetActive(false);
            valveChapter.SetActive(false);
            drillingChapter.SetActive(false);
        }

        private void SetNormalPosition(GameObject _clone)
        {
            _clone.transform.position = Vector3.zero;
            _clone.transform.localScale = Vector3.one;
            _clone.transform.SetParent(transform.root);
        }
    }
}

