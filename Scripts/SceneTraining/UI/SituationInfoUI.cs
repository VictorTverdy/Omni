using System;
using UnityEngine;
using UnityEngine.UI;

namespace Omni.Game.SceneTraining
{
    public class SituationInfoUI : MonoBehaviour
    {
        public static SituationInfoUI Instance;

        public GameObject Content, UNGUIDEDMODE, nextStep;
        public Image SituationIcon, LocationIcon;
        public Text SituationName, NextStep, NumStep, TimeText;

        private float timer;
        private bool isTimer;
        private Vector3 startPosTimer;

        void Awake()
        {
             if (Instance == null)
                Instance = this;
            else
                DestroyImmediate(this);  
        }

        void OnEnable()
        {         
            Content.SetActive(false);
            isTimer = false; 
            timer = 0;
            LocationIcon.transform.parent.gameObject.SetActive(false);
        }

        void Update()
        {
            if (isTimer)
            {  
                timer += Time.deltaTime;      
                TimeSpan timeSpan = TimeSpan.FromSeconds(timer);
                TimeText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            }              
        }     

        public void UnPause()
        {
            isTimer = true;
        }

        public void Pause()
        {
            if (isTimer)
                isTimer = false;
            else
                isTimer = true;
        }

        public void SetEndSituation()
        {
            TrainingComplitUI.Instance.SetTime(timer);
            isTimer = false;
            SituationName.text = "END";// _name.ToUpper();
            SetNextStep("", -99);
        }

        public void SetSituationName(string _name, eTrainingState _state, bool trainingMode)
        {
            if (String.IsNullOrEmpty(_name))
            {
                Content.SetActive(false);
            }
            else
            {
                CanvasVRPlayer.Instance.FireBoxInfo.content.SetActive(false);
                // LocationIcon.transform.parent.gameObject.SetActive(true);
                if (_state == eTrainingState.Evacuation)
                {
                    LocationIcon.rectTransform.sizeDelta = new Vector2(42, 22);
                    SituationIcon.rectTransform.sizeDelta = new Vector2(50, 40);
                }
                else
                {
                    LocationIcon.rectTransform.sizeDelta = new Vector2(25, 40);
                    SituationIcon.rectTransform.sizeDelta = new Vector2(30, 40);
                }
                Content.SetActive(true);
                Sprite _sprite = Resources.Load<Sprite>("Sprites/" + _name);
                if(_sprite != null)
                {
                    LocationIcon.sprite = _sprite;
                    SituationIcon.sprite = _sprite;
                    LocationIcon.enabled = true;
                    SituationIcon.enabled = true;
                }
                else
                {
                    LocationIcon.enabled = false;
                    SituationIcon.enabled = false;
                }
               
                isTimer = true;
                timer = 0;
                SituationName.text = _name.ToUpper();
                if (!trainingMode)
                {                   
                    UNGUIDEDMODE.SetActive(false);
                    nextStep.SetActive(true);
                }
                else
                {
                    UNGUIDEDMODE.SetActive(true);
                    nextStep.SetActive(false);
                }                    
            }
        }

        public void SetNextStep(string _step, int _numStep)
        {
            if (_numStep == -99)
            {
                NumStep.text = "";
                NextStep.text = "";
            }
            else
            {
                _numStep++;
                if (!string.IsNullOrEmpty(_step))
                    NextStep.text = _step.ToUpper();
                else
                    NextStep.text = "";
                NumStep.text = _numStep.ToString();
            }
        }   
    }
}
