using UnityEngine;

namespace Omni.Game.SceneTraining.Box
{
    public class PointMoveBox : MonoBehaviour,IToDo
    {

        private SequenceActions sequenceActions;

        private void Awake()
        {
            sequenceActions = transform.parent.GetComponent<SequenceActions>();
        }

        public void Action()
        {
            gameObject.SetActive(false);
            sequenceActions.PointMoveAction();           
        }    

       
    }
}
