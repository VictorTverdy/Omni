using UnityEngine;
using System.Collections;
namespace Omni.Game.SceneTraining
{
    public class LookAtPlayer : MonoBehaviour
    {

        private Transform player;

        void OnEnable()
        {
            if (player != null)
                Look();
            else
            {
                player = FindObjectOfType<VRMovement>().transform;
                Look();
            }
        }

        // Use this for initialization
        void Start()
        {          
            InvokeRepeating("Look",0.1f,0.1f);         
        }    

        public void Look()
        {
            transform.LookAt(player);
        }

    }
}
