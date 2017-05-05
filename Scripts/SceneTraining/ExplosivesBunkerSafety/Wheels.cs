using UnityEngine;
namespace Omni.Game.SceneTraining
{
    public class Wheels : MonoBehaviour
    {

        public Transform[] whell;
        public bool canRotate;

        // Update is called once per frame
        void Update()
        {
            if (canRotate)
            {
                for (int i=0;i< whell.Length;i++)
                {
                    whell[i].Rotate(6,0,0);
                }
            }
        }

        public void SetCanRotateWhells()
        {
            canRotate = true;
        }

        public void SetStop()
        {
            canRotate = false;
        }

    }
}
