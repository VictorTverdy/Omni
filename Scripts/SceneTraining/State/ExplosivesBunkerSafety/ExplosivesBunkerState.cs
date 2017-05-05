using UnityEngine;
using Omni.Game.SceneTraining.Box;

namespace Omni.Game.SceneTraining
{
    public class ExplosivesBunkerState : MonoBehaviour
    {
        public SequenceActions SequenceActions;

        // Use this for initialization
        void Start()
        {

        }

        void OnEnable()
        {
            Invoke("StartSituation",1);
        }

        void OnDisable()
        {
            CancelInvoke("StartSituation");
        }

        private void StartSituation()
        {
            SequenceActions.ActivateStart();
        }
    }
}
