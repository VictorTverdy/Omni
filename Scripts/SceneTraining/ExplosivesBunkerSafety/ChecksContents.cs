using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Omni.Game.SceneTraining.Box
{
    public class ChecksContents : Situation, IToDo
    {      
        public MeshRenderer[] MyMeshRenderer;
        public Animator Door;

        void OnEnable()
        {
            nameStep = GlobalTrainingString.checksContentsBunker;
            situation = eSituations.checksContentsBunker;
        }

        public override void Init()
        {           
            StartCoroutine("FlashingObject");
            base.Init();
        }

        public void Action()
        {
            if (sequenceActions.GetCurrentItem() == situation)
            {
                StopCoroutine("FlashingObject");
                foreach (MeshRenderer myMesh in MyMeshRenderer)
                {
                    myMesh.material.SetColor("_EmissionColor", Color.black);
                }
                Door.Play("ClosedDoor");
                sequenceActions.Finish();               
            }
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
