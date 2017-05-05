using UnityEngine;

namespace Omni.Game.SceneTraining.Box
{
    public enum eSituations
    {
        none = -1,
        arrivedBox,
        checkPaper,
        moveLocation,
        placedBox,
        groundingBar,
        checksLocks,
        checksContentsBunker
    }

    public class Situation : MonoBehaviour
    {
        public eSituations situation;
        public Transform pointMove; 
        public string nameStep;
        protected SequenceActions sequenceActions;

        // Use this for initialization
        public void Awake()
        {
            sequenceActions = FindObjectOfType<SequenceActions>();
            Transform _pm = transform.Find("PointMove");
            if (_pm != null)
                pointMove = transform.Find("PointMove").transform;
        }     

        public virtual void Init()
        {

        }
       
    }
}
