using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace Omni.Game.SceneTraining
{
    public enum eItems
    {
        None,
        FireAlert,
        Rod,
        Extinguisher,
        GasMask,
        Fire,
        EmergencyEvacuation,
        MusterPoint
    }

    public abstract class InteractItem : MonoBehaviour, IToDo
    {            
        public MeshRenderer MyMeshRenderer;

        protected string nameStep;
        protected bool CanInteract = true;
        protected eItems CurrentItem; 
        protected GameObject CanvasText;
        private Transform MyPointMove;
        private Sprite imageCanvas;

        public virtual void Awake()
        {
            MyPointMove = transform.Find("PointMove");
            Transform _canvas = transform.Find("Canvas");
            if(_canvas != null)
            {
                CanvasText = _canvas.gameObject;
                CanvasText.SetActive(false);
                Image _imageCompleted = CanvasText.transform.GetComponentInChildren<Image>();
                if (_imageCompleted != null)
                {
                    imageCanvas = CanvasText.transform.GetComponentInChildren<Image>().sprite;
                }
            }               
        }

        public virtual void Action()
        {
            DeactivateObject();
        }

        public void SetActiveObject()
        { 
            MyMeshRenderer.gameObject.SetActive(true);
        }

        public void ActivateObject()
        {
            if (MyMeshRenderer != null)
                StartCoroutine("FlashingObject");
            if (CanvasText != null)            
                CanvasText.SetActive(true); 
            if (MyPointMove != null)
            {
                int num = TrainingStateManager.Instance.GetNumItemsInCurrentState(CurrentItem);
                SituationInfoUI.Instance.SetNextStep(nameStep, num);
                ManagerInteractItems.Instance.ShowPointMove(MyPointMove.position, num);              
            }
        }

        public void DeactivateObject()
        {
        //    if (TrainingStateManager.Instance.GetCurrentStateItems() == null)
         //     return; 

            eItems next = TrainingStateManager.Instance.GetNextItem(CurrentItem);
            ManagerInteractItems.Instance.ActivateNextObject(next);          
            CanInteract = false;
            if (MyMeshRenderer != null)
                StopCoroutine("FlashingObject");
            if (CanvasText != null)
            {
                CanvasText.SetActive(true);
                Image _imageCompleted = CanvasText.transform.GetComponentInChildren<Image>();
                if(_imageCompleted != null)
                {
                    _imageCompleted.sprite = Resources.Load<Sprite>("Sprites/completed");                  
                }
                Invoke("DeactivateImageCompleted", 2);
            }                        
            if (MyMeshRenderer != null)
                MyMeshRenderer.material.SetColor("_EmissionColor", Color.black);
        }

        public string GetNameStep()
        {
            return nameStep;
        }

        public eItems GetCurrentItem()
        {
            return CurrentItem;
        }

        public void SetCanInteract()
        {
            if (CanvasText != null)
            {
                Image _imageCompleted = CanvasText.transform.GetComponentInChildren<Image>();
                if (_imageCompleted != null)
                {
                    CanvasText.transform.GetComponentInChildren<Image>().sprite = imageCanvas;
                }
            }       
            CanInteract = true;
        }

        public bool GetCanInteract()
        {
            return CanInteract;
        }

        private void DeactivateImageCompleted()
        {
            CanvasText.SetActive(false);
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

        private void CanvasLookAtPlayer()
        {
            CanvasText.transform.LookAt(Camera.main.transform);
        }
    }
}
