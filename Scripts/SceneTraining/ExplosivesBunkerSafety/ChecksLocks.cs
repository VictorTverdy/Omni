using System.Collections;
using UnityEngine;

namespace Omni.Game.SceneTraining.Box
{
    public class ChecksLocks : Situation, IToDo
    {
        public Animator OpenDoor;
        public MeshRenderer[] MyMeshRenderer;
        private BoxCollider colider;
        private bool canInteract;
        void OnEnable()
        {
            nameStep = GlobalTrainingString.checksLocks;
            situation = eSituations.checksLocks;
        }

        public override void Init()
        {
            canInteract = true;
            colider = transform.Find("colider").GetComponent<BoxCollider>();
            colider.enabled = true;
            StartCoroutine("FlashingObject");
            base.Init();
        }

        public void Action()
        {
            if (canInteract && sequenceActions.GetCurrentItem() == situation)
            {
                canInteract = false;
                Invoke("ActivateNext",1);
                colider.enabled = false;
                StopCoroutine("FlashingObject");               
                OpenDoor.Play("OpenDoor");
                foreach (MeshRenderer myMesh in MyMeshRenderer)
                {
                    myMesh.material.SetColor("_EmissionColor", Color.black);
                }
            }            
        }

        private void ActivateNext()
        {
            sequenceActions.ActivateNextItem();
        }

        private IEnumerator FlashingObject()
        {
            Color _color = Color.black;
            float addStep = 0.01f;
            for (float i = _color.g; _color.g < 0.6f; i += addStep)
            {
                _color.g = i;
                foreach (MeshRenderer myMesh in MyMeshRenderer)
                {
                    myMesh.material.SetColor("_EmissionColor", _color);
                }
                yield return new WaitForSeconds(0.01f);
            }
            for (float i = _color.g; _color.g > 0; i -= addStep)
            {
                _color.g = i;
                foreach (MeshRenderer myMesh in MyMeshRenderer)
                {
                    myMesh.material.SetColor("_EmissionColor", _color);
                }
                yield return new WaitForSeconds(0.01f);
            }
            StartCoroutine("FlashingObject");
        }
    }
}
