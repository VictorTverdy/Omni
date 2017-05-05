using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace Omni.Game.SceneTraining {
    public class ManagerInteractItems : MonoBehaviour
    {

        public static ManagerInteractItems Instance;

       
        public Transform PointMove;
        public Text TextNumPoint;
        public InteractItem[] Items;

        private GameObject WorkerGroupDeactivate;
        private Transform player;
        // Use this for initialization
        void Awake()
        {

            if (Instance != null)
                DestroyImmediate(this);
            else
                Instance = this;          
        }

        void Start()
        {          
            Items = transform.GetComponentsInChildren<InteractItem>();
            player = FindObjectOfType<VRMovement>().transform;
            DeactivateAllItems();
        }

        public void DeactivateWorkers()
        {
            WorkerGroupDeactivate.SetActive(false);
        }

        public void ActivateNextObject(eItems numNext)
        {
            if (numNext == eItems.None)           
                return;            
               
            int lenght = Items.Length;           
            for (int i = 0; i < lenght; i++) 
            {
                if (Items[i].GetCurrentItem() == numNext && Items[i].GetCanInteract())
                {
                    Items[i].gameObject.SetActive(true); 
                    Items[i].ActivateObject();                   
                    break; 
                }
            }            
        }

        public InteractItem GetItem(eItems _item)
        {
            if (_item == eItems.None)
                return null;
            
            InteractItem _interactItem = Items.First(x => x.GetCurrentItem() == _item);
            _interactItem.gameObject.SetActive(true);
            return _interactItem;
        }

        public bool GetCanExtinguisherFire()
        {
            if (!Items.First(x => x.GetCurrentItem() == eItems.Rod).GetCanInteract())
                return true;
            else
                return false;
        }

        public void DeactivateAllItems()
        {
            if (WorkerGroupDeactivate == null)
                WorkerGroupDeactivate = GameObject.Find("Workers");
            WorkerGroupDeactivate.SetActive(true);
            PointMove.gameObject.SetActive(false);
            int lenght = Items.Length;
            for (int i = 0; i < lenght; i++)
            {
                Items[i].SetCanInteract();
                Items[i].gameObject.SetActive(false);
            }
        }

        public void ShowPointMove(Vector3 _pos, int num)
        {     
            PointMove.position = _pos;
            PointMove.LookAt(player);
            num++;
            TextNumPoint.text = num.ToString();
            PointMove.gameObject.SetActive(true);           
        }
    }
}
