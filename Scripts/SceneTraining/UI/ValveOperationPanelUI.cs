using UnityEngine;
using UnityEngine.UI;

namespace Omni.Game.SceneTraining
{
    public class ValveOperationPanelUI : MonoBehaviour
    {
        public Button next;
        // Use this for initialization
        void Start()
        {
            next.onClick.AddListener(OnNext);
        }

       private void OnNext()
        {
         //   gameObject.SetActive(false);
        }

        private void OnBack()
        {

        }
    }
}
