using Omni.Game.SceneTraining.State;
using UnityEngine;
using UnityEngine.UI;

namespace Omni.Game.SceneTraining
{
    public class ComplexityLevel : MonoBehaviour
    {
        public GameObject SelectedOtline;
        public Button Next;
        public Button easy,normal,hard;

        private eHazardLevel currentLevel;       

        // Use this for initialization
        void Start()
        {            
            Next.onClick.AddListener(OnNext);
            easy.onClick.AddListener(SelectedEasy);
            normal.onClick.AddListener(SelectedNormal);
            hard.onClick.AddListener(SelectedHard); 
        }

        void OnEnable()
        {
            SelectedNormal();
        }

        void OnNext()
        {            
            GlobalSceneState.Instance.SetNewTraininigState(currentLevel);
            gameObject.SetActive(false);
        }

        void SelectedEasy()
        {
            SelectedOtline.transform.position = easy.transform.position;
            currentLevel = eHazardLevel.easy; 
        }

        void SelectedNormal()
        {
            SelectedOtline.transform.position = normal.transform.position;
            currentLevel = eHazardLevel.medium;
        }

        void SelectedHard()
        {
            SelectedOtline.transform.position = hard.transform.position;
            currentLevel = eHazardLevel.hard;
        }
    }
}
