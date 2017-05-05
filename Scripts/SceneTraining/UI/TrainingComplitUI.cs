using Omni.UI.Gameplay.HUD;
using Omni.Game.SceneTraining.State;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Omni.Game.SceneTraining
{
    public class TrainingComplitUI : MonoBehaviour
    {
        public static TrainingComplitUI Instance;

        public TutorialUIController TutorialUIController;
        public Button Repeate, Next;
        public Text textTime;
        public GameObject Content;

        // Use this for initialization
        void Start()
        {
            Instance = this;
            Next.onClick.AddListener(NextTraining);
            Repeate.onClick.AddListener(RepeateTraining);
        }

        public void SetTime(float _time)
        {
            TutorialUIController.MoveToPlayer(); 
            Content.SetActive(true);  
            TimeSpan timeSpan = TimeSpan.FromSeconds(_time);
            textTime.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }

        public void NextTraining()
        { 
            Content.SetActive(false);
            GlobalSceneState.Instance.NextScene();    
        }

        public void RepeateTraining()
        {
            Content.SetActive(false);
            GlobalSceneState.Instance.RepeateScene();                            
        }
    }
}
