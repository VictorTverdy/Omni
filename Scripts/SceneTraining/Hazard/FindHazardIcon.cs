using UnityEngine;
using System.Collections;
namespace Omni.Game.SceneTraining
{
    public class FindHazardIcon : MonoBehaviour
    {
        private Transform player;

        void OnEnable()
        {
            transform.localScale =  Vector3.one;
            transform.localPosition = Vector3.zero;
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
            StartCoroutine("Smallest");
            InvokeRepeating("Look", 0.1f, 0.1f);
        }

        IEnumerator Smallest()
        {
            yield return new WaitForSeconds(3);
            for (float f = 1; f > 0.4f; f -= 0.01f)
            {
                transform.position += new Vector3(0,0.02f,0);
                transform.localScale = new Vector3(f, f, 1);
                yield return new WaitForSeconds(0.05f);
            }
        }

        public void Look()
        {
            transform.LookAt(player);
        }
    }
}
