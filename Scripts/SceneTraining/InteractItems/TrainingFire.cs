using UnityEngine;
namespace Omni.Game.SceneTraining
{
    public class TrainingFire : MonoBehaviour
    {
        public ParticleSystem[] Fire; 
        public ParticleSystem WhiteSmoke;
        public GameObject LightFire;
        public bool deactivate;

        private void OnEnable()
        {
            for (int i = 0; i < Fire.Length; i++)
            {
                 Fire[i].gameObject.SetActive(true);              
            }
            WhiteSmoke.gameObject.SetActive(false);
            if (LightFire != null)
                LightFire.SetActive(true);
            deactivate = false;
        }

        public void DeactivationFire()
        {
            if (deactivate)
                return;

            deactivate = true;
            WhiteSmoke.gameObject.SetActive(true);
            if (LightFire != null)
                LightFire.SetActive(false);
            for (int i = 0; i < Fire.Length; i++)
            {
                Fire[i].gameObject.SetActive(false);
            }
        }
    }
}
