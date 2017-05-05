using UnityEngine;
using UnityEngine.UI;

namespace Omni.Game.SceneTraining
{
    public class HazardInfo : MonoBehaviour
    {
        public static HazardInfo Instance;

        public GameObject Content;
        public Text TextProggres;
        public Text LimitRadar;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                DestroyImmediate(this);
        }
        // Use this for initialization
        void OnEnable()
        {
            Content.SetActive(false);
            TextProggres.text = "";
        }
       
    }
}
