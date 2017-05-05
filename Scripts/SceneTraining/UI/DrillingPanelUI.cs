using Omni.Game.SceneTraining.State;
using UnityEngine;

using UnityEngine.UI;

namespace Omni.Game.SceneTraining
{
    public class DrillingPanelUI : MonoBehaviour
    {
        public Button next;
        // Use this for initialization
        void Start()
        {
            next.onClick.AddListener(OnNext);
        }

        private void OnNext()
        {
            GlobalSceneState.Instance.SetNewDrillingState();
            gameObject.SetActive(false);
        }

        private void OnBack()
        {

        }
    }
}
