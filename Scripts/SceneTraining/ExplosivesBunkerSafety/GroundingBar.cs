using System.Collections;
using UnityEngine;

namespace Omni.Game.SceneTraining.Box
{
    public class GroundingBar : Situation, IToDo
    {
        public MeshRenderer DeactivateObj;
        public MeshRenderer MyMeshRenderer;
        private BoxCollider colider;

        void OnEnable()
        {
            nameStep = GlobalTrainingString.groundingBar;
            situation = eSituations.groundingBar;
        }

        public override void Init()
        {
            colider = transform.Find("colider").GetComponent<BoxCollider>();
            colider.enabled = true;
            StartCoroutine("FlashingObject");
            base.Init(); 
        }

        public void Action()
        {
            if (sequenceActions.GetCurrentItem() == situation)
            {
                DeactivateObj.enabled = false;
                colider.enabled = false;
                StopCoroutine("FlashingObject"); 
                sequenceActions.ActivateNextItem();
                MyMeshRenderer.material.SetColor("_EmissionColor", Color.black); 
                MyMeshRenderer.GetComponent<Animator>().Play("GroundingBar");
            }         
        }

        private IEnumerator FlashingObject()
        {
            Color _color = Color.black;
            float addStep = 0.01f;
            for (float i = _color.g; _color.g < 0.6f; i += addStep)
            {
                _color.g = i;
                MyMeshRenderer.material.SetColor("_EmissionColor", _color);
                yield return new WaitForSeconds(0.01f);
            }
            for (float i = _color.g; _color.g > 0; i -= addStep)
            {
                _color.g = i;
                MyMeshRenderer.material.SetColor("_EmissionColor", _color);
                yield return new WaitForSeconds(0.01f);
            }
            StartCoroutine("FlashingObject");
        }
    }
}
