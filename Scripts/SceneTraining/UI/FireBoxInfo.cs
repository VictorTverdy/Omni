using UnityEngine;
using UnityEngine.UI;
namespace Omni.Game.SceneTraining
{
    public class FireBoxInfo : MonoBehaviour
    {
        public GameObject content;
        public Text nameStep, numStep;       
       
        void Start()
        {
            content.SetActive(false);

        }

        public void ActivateInfo(string _step,int num)
        {
            num++;
            content.SetActive(true);
            nameStep.text = _step;
            numStep.text = num.ToString();
        }

    }
}
