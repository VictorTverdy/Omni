using UnityEngine;
namespace Omni.Game.SceneTraining
{
    public class Flashing : MonoBehaviour
    {
        public SpriteRenderer image;
        // Use this for initialization
        void Start()
        {
            InvokeRepeating("FlashingAction",1,1);
        }

        private void FlashingAction()
        {
            if (image.enabled)
                image.enabled = false;
            else
                image.enabled = true;
        }
    }
}
