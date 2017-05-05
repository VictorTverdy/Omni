using UnityEngine;
namespace Omni.Game.SceneTraining
{
    public class Wind : MonoBehaviour
    {

        public Transform target;
        private Transform player;

        // Use this for initialization
        void Start()
        {
            player = FindObjectOfType<VRMovement>().transform;
        }
        private void Update()
        {
            Look();
        }

        private void Look()
        {
            transform.position = player.transform.position;
            transform.LookAt(target);
        }
    }
}
